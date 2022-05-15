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
/*
    1. Create
        完全削除
        ・状態データクリア
        ・ツリー削除
        生成

    2. Enter/Leave
        カレントになるノード色変更　(対象外は薄い色で、カレントがはっきり見える)
        カレント変更後、カレントのみオープンされて、他はクローズ。

    3. グループ化
        カレントより上のノードのオープン・クローズはそのままで、カレント部分のみ再構築される。
        カレントより下層はクローズ

    4. グループ解除
        カレントより上のノードのオープン・クローズはそのまま。カレント部分のみ再構築される。

    ?5. ダブルクリックしたノード、または、ステートの所属するノードにカレントが移動

*/
using GroupNode=stateview.GroupNodeManager.NodeData;

namespace stateview
{
    public class TabNodeTree
    {
        //readonly string TAG_STATE= "state";
        //readonly string TAG_DIR  = "dir";

        public class TagData
        {
            public enum Type
            {
                STATE,
                DIR
            }
            public Type      typ;
            public GroupNode gn;    //所属グループ
            public GroupNode subgn; //ディレクトリ時のグループ
            public string    name;
            public string    path;

            private TagData() { }
            public static TagData Create(string name,Type typ, GroupNode gn, GroupNode subgn=null)
            {
                var td = new TagData();
                td.typ = typ;
                td.gn = gn;
                td.subgn = subgn;
                td.name  = name;

                if (typ == Type.STATE)
                {
                    td.path = gn.m_pathdir + name;
                }
                else
                {
                    td.path = subgn.m_pathdir;
                }
                return td;
            }
        }

        TreeView m_tree { get { return G.view_form.NodeTreeView; } }
        TreeNode m_treeroot { get {
                if (m_tree.Nodes.Count==1 && m_tree.Nodes[0].Text=="/")
                {
                    return m_tree.Nodes[0];
                }
                return null;
            } }

        GroupNode m_grouproot { get { return G.node_get_rootdata(); } }

        public void CreateAndSetCurrent()
        {
            try {
                create();
                SetCurrent();
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{D75C6834-09B3-4377-A15C-89AE573E2567}" + e.Message);
            }
        }

        private void create()
        {
            m_tree.Nodes.Clear();
            m_tree.ShowNodeToolTips = true;
            var rootstr = "/";
            var treeroot = m_tree.Nodes.Add(rootstr);
            treeroot.Tag = TagData.Create(rootstr,TagData.Type.DIR,null,m_grouproot);

            Action<TreeNode,GroupNode> trv = null;
            trv = (tn,gn)=> {
                if (gn!=null && gn.m_state_list!=null) foreach(var s in gn.m_state_list)
                {
                    var newtn = tn.Nodes.Add(s);
                    newtn.Tag = TagData.Create(s,TagData.Type.STATE,gn);
                    newtn.ToolTipText = G.excel_program.GetString(s, G.STATENAME_statecmt);

                }
                if (gn!=null && gn.m_subdir_list!=null) foreach(var gn2 in gn.m_subdir_list)
                {
                    var newtn = tn.Nodes.Add(gn2.m_dirname);
                    newtn.Tag = TagData.Create(gn2.m_dirname,TagData.Type.DIR,gn,gn2);
                    trv(newtn,gn2);
                }
            };

            trv(treeroot,m_grouproot);
        }

#if x
        public void SetCurrent()
        {
            var cur = G.node_get_curdir();

            Action<TreeNode> trv = null;
            trv = (tn) => {
                if (tn==null) return;
                foreach(var i in tn.Nodes)
                {
                    var tn2 = (TreeNode)i;
                    var td = (TagData)tn2.Tag;

                    var gn = td.gn;

                    tn2.ForeColor = Color.Black;
                    tn2.BackColor = Color.White;

                    if (cur == gn.m_pathdir )
                    {
                        tn2.ForeColor = Color.Red;
                        tn2.BackColor = Color.LightGreen;

                        if (!tn2.IsExpanded)
                        {
                            tn2.Expand();
                        }
                    }
                    else
                    {
                    }

                    if (td.typ == TagData.Type.DIR)
                    {
                        if (gn.m_parent!=null && gn.m_parent.m_pathdir == cur)
                        {
                            tn2.ForeColor = Color.Red;
                            tn2.BackColor = Color.LightGreen;
                        }
                        trv(tn2);
                    }
                }
            };

            trv(m_treeroot);
        }
#endif
        public void SetCurrent()
        {
            string highlight_node;
            List<string> highlight_states;
            List<string> highlight_groups;
            List<string> srcdst_states;

            G.node_get_current_statedrawtreedata(out highlight_node,out highlight_states,out highlight_groups,out srcdst_states);

            Action<TreeNodeCollection> trv = null;
            bool? bTarget = null;
            trv = (nds)=> {
                if (nds == null) return;

                foreach(var i in nds)
                {
                    var tn = (TreeNode)i;
                    var td = (TagData)tn.Tag;

                    tn.ForeColor = Color.Black;
                    tn.BackColor = Color.White;


                    if (td.typ == TagData.Type.STATE)
                    {
                        if (highlight_states.Contains(td.name))
                        {
                            tn.ForeColor = DS.GetColor(SS.STYLE.NORMAL, DS.PARTS.STATETEXT);
                            tn.BackColor = DS.GetColor(SS.STYLE.NORMAL, DS.PARTS.STATEBG);
                            bTarget = false;

                        }
                        else if (srcdst_states.Contains(td.name))
                        {
                            tn.ForeColor = DS.GetColor(SS.STYLE.FORSRC, DS.PARTS.STATETEXT);
                            tn.BackColor = DS.GetColor(SS.STYLE.FORSRC, DS.PARTS.STATEBG);
                        }
                    }
                    else
                    {
                        if(highlight_node == td.path)
                        {
                            tn.ForeColor = Color.Red;
                            tn.BackColor = Color.White;
                            bTarget = true;
                        }
                        else if (highlight_groups.Contains(td.path))
                        {
                            tn.ForeColor = DS.GetColor(SS.STYLE.FORGROUP, DS.PARTS.STATETEXT);
                            tn.BackColor = DS.GetColor(SS.STYLE.FORGROUP, DS.PARTS.STATEBG);
                            bTarget = false;
                        }

                        if (bTarget==null)
                        {
                            tn.Expand();
                        }
                        else if (bTarget==true)
                        {
                            tn.Expand();
                            bTarget = false;
                        }

                        trv(tn.Nodes);
                    }
                }
            };

            trv(m_tree.Nodes);
        }

        public void SelectState(string state)
        {
            bool bSelected = false;
            Action<TreeNodeCollection> trv = null;
            trv = (nds) => {
                if (bSelected) return;
                if (nds == null) return;

                foreach(var i in nds)
                {
                    var tn = (TreeNode)i;
                    var td = (TagData)tn.Tag;
                    if (td.typ == TagData.Type.STATE)
                    {
                        if (tn.Text == state)
                        {
                            m_tree.SelectedNode = tn;
                            bSelected = true;
                            return;
                        }
                    }
                    else if (td.typ == TagData.Type.DIR)
                    {
                        trv(tn.Nodes);
                    }
                }

            };
            trv(m_tree.Nodes);
        }
    }
}
