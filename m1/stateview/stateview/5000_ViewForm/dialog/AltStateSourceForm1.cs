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

namespace stateview._5000_ViewForm.dialog
{
    public partial class AltStateSourceForm1 : Form
    {
        public AltStateSourceForm1()
        {
            InitializeComponent();
        }

        public enum TARGETCONDITION
        {
            none,
            no_distination_all,
            no_distination_nextstate_only,
            no_distination_branch_only,
            no_disiination_gosub_only
        }
        public enum TARGETRANGE
        {
            none,
            states_under_the_group,
            all
        }
        TARGETCONDITION m_targetcondition = TARGETCONDITION.no_distination_all;
        TARGETRANGE     m_targetrange = TARGETRANGE.states_under_the_group;


        public string m_dirpath;
        public DATA   m_result;
        public ViewFormStateControl.BranchInfo m_resultbi; //m_resultをbiに変換

        public string m_dstination;

        private List<DATA> m_data_list;

        public class DATA
        {
            public enum TYPE
            {
                nextstate,
                branch_api,
                branch_if,
                branch_elif,
                branch_else,
                gosub
            }
            public static bool Is_IF(TYPE t) { return t == TYPE.branch_if || t == TYPE.branch_elif || t == TYPE.branch_else; }
            public static bool Is_IF(DATA d) { return Is_IF(d.type); }
            
            public string state;
            public TYPE   type;
            public int    branch_index;
            public string branch_cond_or_api;
            public string branch_comment;

            public override string ToString()
            {
                var s = state + ", " + type.ToString();
                if (type == TYPE.branch_api)
                {
                   s += ", " + branch_cond_or_api + ", " +branch_comment;
                }
                else if (Is_IF(type))
                {
                    if (string.IsNullOrEmpty(branch_comment) ||  branch_comment.Trim()!="?")
                    {
                        s += ",　" + branch_cond_or_api;
                    }
                    else
                    {
                        s += ",　" + branch_comment;
                    }
                }
                return s;
            }
        }

        private void AltStateSourceForm1_Load(object sender, EventArgs e)
        {
            this.Location = PointUtil.Add_XY( Cursor.Position,- this.Width / 3,- this.Height / 3);
            m_result = null;
            set_datagridview();
        }

        DataGridViewCellStyle m_typestyle;
        void set_datagridview()
        {
            m_data_list = null;

            var targetstates = _get_rangestates();
            var candlist = _get_states_w_condition(targetstates);
            candlist.Sort((a,b)=> { return a.state.CompareTo(b.state); } );

            var dgv = this.dataGridView1;
            dgv.Rows.Clear();
            foreach(var d in candlist)
            {
                var typestr = d.type.ToString();
                var cmt = d.branch_comment;
                if (DATA.Is_IF(d) || d.type == DATA.TYPE.branch_api)
                {
                    if (DATA.Is_IF(d)) typestr = typestr.Substring(7); //branch_を消す

                    typestr = d.branch_index.ToString() + ":" + typestr;
                    if (string.IsNullOrEmpty(cmt) || cmt.Trim()=="?")
                    {
                        cmt = d.branch_cond_or_api;
                    }
                }
                dgv.Rows.Add(d.state, typestr, cmt);
            }

            // font
            if (m_typestyle==null) {
                m_typestyle = new DataGridViewCellStyle();
                m_typestyle.Font = new Font("MS Gothic",9);
            }
            for(var r = 0; r < dgv.Rows.Count; r++)
            {
                dgv[1,r].Style = m_typestyle;
            }

            m_data_list = candlist;
        }
        

        List<string> _get_rangestates()
        {
            var states = G.node_get_allstates_by_dirpath(m_dirpath);
            if (states == null) return null;

            if (m_targetrange == TARGETRANGE.states_under_the_group)
            {
                var newlist = new List<string>();
                foreach(var s in states)
                {
                    if (m_dirpath ==  G.node_get_dirpath(s))
                    {
                        newlist.Add(s);
                    }
                }
                return newlist;
            }
            else if (m_targetrange == TARGETRANGE.all)
            {
                return states;
            }
            else
            {
                throw new SystemException("{3A95FC6F-5D6A-4FFA-853F-F9D858C8A26B}");
            }
        }
        List<DATA> _get_states_w_condition(List<string> list)
        {
            /*
                行先が未決定を収集、よって G.state_output_dst_list(決定済みの収集リスト)は使えない。
            */
            var alllist = new List<DATA>();
            foreach(var s in list)
            {
                var statetyp = G.excel_program.GetString(s,G.STATENAME_statetyp);

                if (
                    statetyp == WordStorage.Store.state_typ_end 
                    ||
                    statetyp == WordStorage.Store.state_typ_stop
                    ||
                    statetyp == WordStorage.Store.state_typ_subreturn
                    )
                {
                    continue;
                }

                var nextstate = G.excel_program.GetString(s,G.STATENAME_nextstate);
                var branch = G.excel_program.GetString(s,G.STATENAME_branch);
                if (!string.IsNullOrEmpty(branch))
                {
                    var apilist = BranchUtil.GetApiAndLabelListFromState(s);
                    for(var index = 0; index < apilist.Count; index++)
                    {
                        var a = apilist[index];
                        if (string.IsNullOrEmpty(a.label)|| a.label.Trim()=="?")
                        {
                            if (a.mode == BranchUtil.APIMODE.API)
                            {
                                alllist.Add( new DATA() { state = s, type = DATA.TYPE.branch_api, branch_index = index, branch_cond_or_api = a.api, branch_comment = a.comment }  );
                                continue;
                            }
                            else if (a.mode == BranchUtil.APIMODE.IF)
                            {
                                alllist.Add( new DATA() { state = s, type = DATA.TYPE.branch_if, branch_index = index, branch_cond_or_api = a.condition, branch_comment = a.comment} );
                                continue;
                            }
                            else if (a.mode == BranchUtil.APIMODE.ELSEIF)
                            {
                                alllist.Add( new DATA() { state = s, type = DATA.TYPE.branch_elif, branch_index = index, branch_cond_or_api = a.condition, branch_comment = a.comment} );
                                continue;
                            }
                            else if (a.mode == BranchUtil.APIMODE.ELSE)
                            {
                                alllist.Add( new DATA() { state = s, type = DATA.TYPE.branch_else, branch_index = index, branch_cond_or_api = a.condition, branch_comment = a.comment} );
                                continue;
                            }
                        }
                    }
                }
                else { 
                    if (string.IsNullOrEmpty(nextstate) || nextstate.Trim() == "?")
                    {
                        alllist.Add( new DATA() { state = s, type = DATA.TYPE.nextstate });
                    }
                }
                if (statetyp == WordStorage.Store.state_typ_gosub || statetyp == WordStorage.Store.state_typ_loop)
                {
                    var gosub = G.excel_program.GetString(s,G.STATENAME_gosubstate);
                    if (  string.IsNullOrEmpty(gosub) || gosub.Trim() == "?" )
                    {
                        alllist.Add( new DATA() { state = s, type = DATA.TYPE.gosub });
                    }
                }
            }

            if (m_targetcondition == TARGETCONDITION.no_distination_all)
            {
                return alllist;
            }
            else if (m_targetcondition == TARGETCONDITION.no_distination_nextstate_only)
            {
                var nextstate_only_list = new List<DATA>();
                foreach(var d in alllist)
                {
                    if (d.type == DATA.TYPE.nextstate)
                    nextstate_only_list.Add(d);
                }
                return nextstate_only_list;
            }
            else if (m_targetcondition == TARGETCONDITION.no_distination_branch_only)
            {
                var branch_only_list = new List<DATA>();
                foreach(var d in alllist)
                {
                    if (d.type == DATA.TYPE.branch_api || DATA.Is_IF(d.type))
                    {
                        branch_only_list.Add(d);
                    }
                }
                return branch_only_list;
            }
            else if (m_targetcondition == TARGETCONDITION.no_disiination_gosub_only)
            {
                var gosub_only_list = new List<DATA>();
                foreach(var d in alllist)
                {
                    if (d.type == DATA.TYPE.gosub)
                    {
                        gosub_only_list.Add(d);
                    }
                }
                return gosub_only_list;
            }
            else
            {
                throw new SystemException("{C4E50D3D-6E4A-4CAF-BBB0-2C998409FE1F}");
            }
            //return null;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (m_result!=null)
            {
                if (!AltState.IsAltState( m_dstination))
                {
                    if (m_result.type == DATA.TYPE.nextstate)
                    {
                        G.excel_program.SetString(m_result.state, G.STATENAME_nextstate,m_dstination);
                    }
                    else if (m_result.type == DATA.TYPE.branch_api || DATA.Is_IF(m_result.type))
                    {
                        var s  = G.excel_program.GetStringWithBasestate(m_result.state,/*"branch"*/G.STATENAME_branch);
                        var s2 = DTBranchUtil.SetLebel(s,(int)m_result.branch_index,m_dstination);
                        G.excel_program.SetString(m_result.state,/*"branch"*/G.STATENAME_branch,s2);                        
                    }
                    else if (m_result.type == DATA.TYPE.gosub)
                    {
                        var s  = G.excel_program.GetStringWithBasestate(m_result.state,/*"branch"*/G.STATENAME_gosubstate);
                        if (string.IsNullOrEmpty(m_dstination) && G.excel_program.IsMandatoryGosub(m_result.state))
                        {
                            m_dstination = "?";
                        }
                        G.excel_program.SetString(m_result.state,/*"gosubstate"*/G.STATENAME_gosubstate,m_dstination);
                    }
                }
                else
                {
                    m_resultbi = new ViewFormStateControl.BranchInfo();  //後処理で使うため。
                    m_resultbi.m_branchpoint_state = m_result.state;

                    if      (m_result.type == DATA.TYPE.nextstate) m_resultbi.m_branchpoint_isNextStateOrBranchOrGosub = 1;
                    else if (DATA.Is_IF(m_result.type))            m_resultbi.m_branchpoint_isNextStateOrBranchOrGosub = 2;
                    else if (m_result.type == DATA.TYPE.gosub)     m_resultbi.m_branchpoint_isNextStateOrBranchOrGosub = 3;

                    m_resultbi.m_branchpoint_branch_index = m_result.branch_index;
                }
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AltStateSourceForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            if (row < 0 || row >= dataGridView1.Rows.Count) return;

            m_result = m_data_list[row];
            this.textBox_InputState.Text = m_result.ToString();
        }

        private void radioButton_all_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition = TARGETCONDITION.no_distination_all;
            set_datagridview();
        }

        private void radioButton_nextstate_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition = TARGETCONDITION.no_distination_nextstate_only;
            set_datagridview();
        }

        private void radioButton_branch_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition = TARGETCONDITION.no_distination_branch_only;
            set_datagridview();
        }

        private void radioButton_gosub_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition = TARGETCONDITION.no_disiination_gosub_only;
            set_datagridview();
        }

        private void checkBox_incsub_CheckedChanged(object sender, EventArgs e)
        {
            m_targetrange = (this.checkBox_incsub.Checked) ? TARGETRANGE.all : TARGETRANGE.states_under_the_group ;
            set_datagridview();
        }
    }
}
