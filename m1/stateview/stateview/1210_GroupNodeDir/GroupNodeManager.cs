//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

using stateview;

namespace stateview
{
    /*
        グループノードを管理する

        リネームや移動を簡単に分かり易く、かつ、デバッグが楽になるように
    */

    public class GroupNodeManager
    {
        public class NodeData
        {
            public NodeData m_parent;
            public string m_dirname;    //※ルート時は empty
            public string m_comment;
            public Point? m_pos;

            public List<string> m_state_list = new List<string>();
            public List<NodeData> m_subdir_list = new List<NodeData>();

            public string m_pathdir {
                get {
                    var s = string.Empty;
                    Action< NodeData > goback = null;
                    goback = (n)=> {
                        if (n==null) return;
                        s = n.m_dirname + "/" + s;
                        goback(n.m_parent);
                    };

                    goback(this);

                    return s;
                }
            }

            public bool ExistSubdir(string name)
            {
                return FindSubdir(name)!=null;
            }
            public NodeData FindSubdir(string name)
            {
                foreach(var n in m_subdir_list)
                {
                    if (n.m_dirname == name) return n;
                }
                return null;
            }
            public NodeData AddSubDir(string name)
            {
                if (ExistSubdir(name)) return  FindSubdir(name); //既にあった
                var d = new NodeData();
                d.m_parent = this;
                d.m_dirname = name;
                m_subdir_list.Add(d);
                return d;
            }
            public bool AddNodeData(NodeData nd)
            {
                if (m_subdir_list.Contains(nd)) return false;
                nd.m_parent = this;
                m_subdir_list.Add(nd);
                return true;
            }
            public NodeData DelSubDir(string name)
            {
                if (!ExistSubdir(name)) return null; //無い
                var nd = FindSubdir(name);
                m_subdir_list.Remove(nd);
                return nd;
            }
            public bool AddState(string state)
            {
                if (m_state_list.Contains(state)) return false;
                m_state_list.Add(state);
                return true;
            }
            public bool DelState(string state)
            {
                if (!m_state_list.Contains(state)) return false;
                m_state_list.Remove(state);
                return true;
            }
            public bool RemoveSelfFromParent()
            {
                if (m_parent==null) return false;
                if (!m_parent.m_subdir_list.Contains(this)) return false;
                m_parent.m_subdir_list.Remove(this);
                return true;
            }
            public Dictionary<string, List<string>> GetAllChildrenOnSubDir() //サブDIR配下の全ステート収集
            {
                var dic = new Dictionary<string, List<string>>();
                foreach(var sub in m_subdir_list)
                {
                    var list = new List<string>();
                    Action<NodeData> trv = null;
                    trv = (n) => {
                        if (n==null) return;
                        list.AddRange(n.m_state_list);
                        foreach(var n2 in n.m_subdir_list)
                        {
                            trv(n2);
                        }
                    };

                    trv(sub);
                    dic.Add(sub.m_dirname, list);
                }
                return dic;
            }
            public bool GetRelPathdir(string curpath, out string relpath)
            {
                if (m_pathdir.StartsWith(curpath))
                {
                    var rp = m_pathdir.Substring(curpath.Length);
                    relpath = rp;
                    return true;
                }
                relpath = string.Empty;
                return false;
            }
            //---------------------- 以下はstatic -------------------------
            public static NodeData MakeRoot()
            {
                var d = new NodeData();
                d.m_parent = null; //root だから
                d.m_dirname = string.Empty;
                d.m_comment = "The root";
                return d;
            }

            public static bool ExistDirPath(NodeData root,string path)
            {
                return FindNodeData(root,path) != null;
            }
            public static NodeData FindNodeData(NodeData root, string path)
            {
                if (root == null || string.IsNullOrEmpty(path)) return null; //エラー
                if (path == "/") return root;

                NodeData      find = null;
                Action<NodeData> trv    = null;
                trv = (n) => {
                    if (find!=null) return;
                    if (n==null) return;
                    if (n.m_pathdir == path)
                    {
                        find = n;
                        return;
                    }
                    foreach(var n2 in n.m_subdir_list)
                    {
                        trv(n2);
                    }
                };

                trv(root);

                return find;
            }

            public static NodeData Find_or_CreateNodeData(NodeData root, string path)
            {
                if (ExistDirPath(root,path)) return FindNodeData(root,path);
                var dirlist = path.Trim('/').Split('/'); //前後の '/'削除後、
                NodeData nd = root;
                foreach(var i in dirlist)
                {
                    nd = nd.AddSubDir(i);
                }
                return nd;
            }

            public static NodeData FindNodeDataByStateName(NodeData root, string state)
            {
                if (root==null || string.IsNullOrEmpty(state)) return null;
                NodeData find = null;
                Action<NodeData> trv = null;
                trv = (n)=> {
                    if (find != null) return;
                    if (n == null) return;
                    if (n.m_state_list.Contains(state))
                    {
                        find = n;
                        return;
                    }
                    foreach(var n2 in n.m_subdir_list)
                    {
                        trv(n2);
                    }
                };

                trv(root);
                return find;
            }
        } //end of GroupNodeManager.NodeData

        NodeData m_nodedata = null;

        public void ReadAllStates()
        {
            m_nodedata = NodeData.MakeRoot();

            foreach (var s in G.state_working_list)
            {
                string path    ;
                string comment ;
                Point? pos;

                NodeData nd = m_nodedata; //デフォルトでルートとなるようにする。
                var bOk = DirPathExcelUtil.get_diritems(s,out path, out pos, out comment);

                if (bOk)
                {
                    nd = NodeData.Find_or_CreateNodeData(m_nodedata,path);
                    if (string.IsNullOrEmpty(nd.m_comment))//決定済み後は書込みしない
                    {
                        nd.m_comment = comment;
                    }
                    if (nd.m_pos == null)   //決定済み後は書込みしない
                    {
                        nd.m_pos = pos;
                    }
                }
                nd.m_state_list.Add(s);
            }
        }

        public NodeData FindByState(string state)
        {
            return NodeData.FindNodeDataByStateName(m_nodedata,state);
        }
        public NodeData FindByPath(string path)
        {
            return NodeData.FindNodeData(m_nodedata,path);
        }
        public bool RenameSubDir(string path, string from, string to)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to)) return false;

            var nd = NodeData.FindNodeData(m_nodedata,path);
            if (nd == null) return false;

            var nd2 = nd.FindSubdir(from);
            if (nd2 == null) return false;

            nd2.m_dirname = to;


            return true;
        }
        /// <summary>
        ///
        /// 指定ステートが所属するサブディレクトリのノードデータを取得する
        ///
        /// </summary>
        public NodeData FindSubDirByState(string targetpath, string state)
        {
            var nd = FindByState(state);
            if (nd == null) return null;

            string relpath;
            if (!nd.GetRelPathdir(targetpath,out relpath))
            {
                return null;
            }

            if (string.IsNullOrEmpty(relpath)) return null;   //サブディレクトリ内でなければ、エラーとする

            var group = RegexUtil.Get1stMatch(@".+?\/",relpath).TrimEnd('/');

            var path = GroupNodeUtil.pathcombine(targetpath,group);

            return FindByPath(path);
        }

        public bool AddState(string path,string state)
        {
            if (NodeData.FindNodeDataByStateName(m_nodedata,state) != null)
            {
                return false; //既にある
            }

            var nd = NodeData.Find_or_CreateNodeData(m_nodedata,path);
            return nd.AddState(state);
        }

        public bool DelState(string state)
        {
            var nd = NodeData.FindNodeDataByStateName(m_nodedata,state);
            if (nd == null) return false;
            return nd.DelState(state);
        }

        public bool MoveState(string state, string path)
        {
            var nd = NodeData.FindNodeDataByStateName(m_nodedata,state);
            if (nd == null) return false;
            if (!nd.DelState(state))
            {
                return false;
            }

            var nd2 = NodeData.FindNodeData(m_nodedata,path);
            return nd2.AddState(state);
        }

        public bool MoveStateForce(string state, string path)
        {
            var nd = NodeData.FindNodeDataByStateName(m_nodedata,state);
            if (nd == null) return false;
            var tnd = NodeData.Find_or_CreateNodeData(m_nodedata,path);
            if (tnd == null) return false;

            tnd.m_pos = nd.m_pos;
            tnd.m_comment = nd.m_comment;

            if (!nd.DelState(state))  throw new SystemException("unexpected! {79BAFB47-33C0-43A4-AA5E-CB94DD63D254}");
            if (!tnd.AddState(state)) throw new SystemException("unexpected! {33320FB6-5777-4FEC-BB87-C191604A0A09}");

            return true;
        }

        public bool RemoveSubDir(string path, string subdir)
        {
            var nd = NodeData.FindNodeData(m_nodedata,path);
            if (nd == null) return false;
            var nd2 = nd.DelSubDir(subdir);
            return nd2 != null;
        }

        public bool MoveSubDir(string frompath, string topath)
        {
            var nd_from = NodeData.FindNodeData(m_nodedata, frompath);
            if (nd_from==null) return false;

            var nd_to = NodeData.FindNodeData(m_nodedata, topath);
            if (nd_to==null) return false;

            var newdirname = nd_from.m_dirname;
            for(var loop = 0; loop <=100; loop++)
            {
                if (loop >= 100) throw new SystemException("Unexpected! {DFEA27BA-CF2E-4BB2-91D7-B7ADAC380290}");
                if (nd_to.ExistSubdir(newdirname))
                {
                    newdirname = StateUtil.MakeExtraName(newdirname);
                    continue;
                }
                else
                {
                    break;
                }
            }
            nd_from.m_dirname = newdirname;


            if (nd_to.ExistSubdir(nd_from.m_dirname)) //送り先に同名がある
            {
                return false;
            }

            nd_from.RemoveSelfFromParent(); //自身を親から削除
            nd_to.AddNodeData(nd_from);     //追加

            return true;
        }

        public void WriteToExcelCache()
        {
            foreach(var s in G.state_working_list)
            {
                var nd = NodeData.FindNodeDataByStateName(m_nodedata,s);
                if (nd!=null)
                { 
                    Point pos = nd.m_pos==null ? new Point(0,0) : (Point)nd.m_pos;
                    //GroupNodeUtil.set_diritems(s,nd.m_pathdir, pos, nd.m_comment);
                    DirPathExcelUtil.set_diritems(s,nd.m_pathdir, pos, nd.m_comment);
                }
                else if (AltState.IsAltState(s))
                {
                    var s2 = AltState.TrimAltStateName(s);
                    var path = GroupNodeUtil.pathcombine(G.node_get_cur_dirpath(),s2);
                    nd = NodeData.FindNodeData(m_nodedata,path);
                    if (nd!=null) G.nodegroup_pos_set(path,nd.m_pos);
                }
            }
        }

        public NodeData GetRootData()
        {
            return m_nodedata;
        }



    }

}
