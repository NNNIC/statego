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
    public partial class AltStateDistinationForm1 : Form
    {
        public AltStateDistinationForm1()
        {
            InitializeComponent();
        }

        public enum TARGETCONDITION_TYP
        {
            none,
            all,
            pass_all,
            substart_all
        }
        public enum TARGETCONDTION_NOINP
        {
            none,
            no_input_source
        }
        public enum TARGETRANGE
        {
            none,
            states_under_the_group,
            all
        }

        TARGETCONDITION_TYP m_targetcondition_typ  = TARGETCONDITION_TYP.all;
        TARGETCONDTION_NOINP m_targetcondition_noinp = TARGETCONDTION_NOINP.no_input_source;
         
        TARGETRANGE     m_targetrange = TARGETRANGE.states_under_the_group;

        public ViewFormStateControl      m_parent;
        ViewFormStateControl.BranchInfo  m_save_branchInfo { get { return m_parent.m_save_branchInfo; } }
        
        public string m_dstpath;

        public DATA m_result;


        private void AltStateDistinationForm1_Load(object sender, EventArgs e)
        {
            this.Location = PointUtil.Add_XY(Cursor.Position,- this.Width / 2 , - this.Height / 2);
            set_datagridview();
        }

        private void AltStateDistinationForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        public class DATA
        {
            public string path;
            public string state;
            public string state_typ;
            public string comment;

            public override string ToString()
            {
                var typstr = state_typ != null ? state_typ : "";
                return state + ", " + typstr + ", " + comment;
            }
        }

        List<DATA> m_distlist;

        void set_datagridview()
        {
            m_distlist = null;
            var dgv = this.dataGridView1;
            dgv.Rows.Clear();
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            var targetsetates = _get_rangestates();
            var candlist      = _get_states_w_condition(targetsetates);
            foreach(var l in candlist)
            {
                var typstr = l.state_typ != null ? l.state_typ.ToString() : "";
                dgv.Rows.Add(l.path, l.state, typstr , l.comment);
            }

            m_distlist = candlist;
        }

        List<string> _get_rangestates()
        {
            var states = G.node_get_allstates_by_dirpath(m_dstpath);
            if (states == null) return null;

            if (m_targetrange == TARGETRANGE.states_under_the_group)
            {
                var newlist = new List<string>();
                foreach(var s in states)
                {
                    if (m_dstpath ==  G.node_get_dirpath(s))
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
            var alllist = new List<DATA>();
            foreach(var s in list)
            {
                var statetyp = G.excel_program.GetString(s,G.STATENAME_statetyp);

                if (
                    statetyp == WordStorage.Store.state_typ_start
                    )
                {
                    continue;
                }

                var cmt = G.excel_program.GetString(s,G.STATENAME_statecmt);

                var pathdir = G.node_get_dirpath(s);
                alllist.Add(new DATA() { path = pathdir, state = s, state_typ = statetyp,comment = cmt });
            }

            if (m_targetcondition_noinp == TARGETCONDTION_NOINP.no_input_source)
            {
                var sublist = new List<DATA>();
                foreach(var d in alllist)
                {
                    var state = d.state;
                    if (G.state_input_src_list!=null && !G.state_input_src_list.ContainsKey(state))
                    {
                        sublist.Add(d);
                    }
                }
                alllist = sublist;
            }

            if (m_targetcondition_typ != TARGETCONDITION_TYP.all)
            {
                var target_typ = string.Empty;
                if      (m_targetcondition_typ == TARGETCONDITION_TYP.pass_all)     target_typ = WordStorage.Store.state_typ_pass;
                else if (m_targetcondition_typ == TARGETCONDITION_TYP.substart_all) target_typ = WordStorage.Store.state_typ_start;
                else
                {
                    throw new SystemException("{728DC835-EE2F-4E14-B433-B3F47F997D2D}");
                }

                var sublist = new List<DATA>();
                foreach(var d in alllist)
                {
                    if (d.state_typ == target_typ)
                    {
                        sublist.Add(d);
                    }
                }

                alllist = sublist;
            }


            return alllist;
        }




        private void ok_button_Click(object sender, EventArgs e)
        {
            if (m_result==null)
            {
                return;
            }

            var bi = m_save_branchInfo;
            var new_dest = m_result.state;
            var srcstate = bi.m_branchpoint_state;
            if (bi.m_branchpoint_isNextStateOrBranchOrGosub==1)
            {
                G.excel_program.SetString(srcstate,/*"nextstate"*/G.STATENAME_nextstate,new_dest);
            }
            else if (bi.m_branchpoint_isNextStateOrBranchOrGosub==2/*false*/)
            {
                var s  = G.excel_program.GetStringWithBasestate(srcstate,/*"branch"*/G.STATENAME_branch);
                var s2 = DTBranchUtil.SetLebel(s,(int)bi.m_branchpoint_branch_index,new_dest);
                G.excel_program.SetString(srcstate,/*"branch"*/G.STATENAME_branch,s2);
            }
            else if (bi.m_branchpoint_isNextStateOrBranchOrGosub==3/*gosub*/)
            {
                var s  = G.excel_program.GetStringWithBasestate(bi.m_branchpoint_state,/*"branch"*/G.STATENAME_gosubstate);
                if (string.IsNullOrEmpty(new_dest) &&  G.excel_program.IsMandatoryGosub(srcstate)) new_dest = "?";
                G.excel_program.SetString(srcstate,/*"gosubstate"*/G.STATENAME_gosubstate,new_dest);
            }

            History2.SaveForce_modify_value("Changed an arrow direction");

            G.NoticeToUser(string.Format("Connect from {0} to {1}",srcstate , new_dest));

            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }


        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var row = e.RowIndex;
        //    if (row < 0 || row >= dataGridView1.Rows.Count) return;

        //    m_result = m_distlist[row];
        //    this.textBox1.Text = m_result.ToString();
        //}

        private void checkBox_incsub_CheckedChanged(object sender, EventArgs e)
        {
            m_targetrange = this.checkBox_incsub.Checked ? TARGETRANGE.all : TARGETRANGE.states_under_the_group;
            set_datagridview();
        }

        private void radioButton_alltypes_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition_typ = TARGETCONDITION_TYP.all;
            set_datagridview();
        }

        private void radioButton_pass_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition_typ = TARGETCONDITION_TYP.pass_all;
            set_datagridview();
        }

        private void radioButton_substart_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition_typ = TARGETCONDITION_TYP.substart_all;
            set_datagridview();
        }

        private void checkBox_noinput_CheckedChanged(object sender, EventArgs e)
        {
            m_targetcondition_noinp = this.checkBox_noinput.Checked ? TARGETCONDTION_NOINP.no_input_source : TARGETCONDTION_NOINP.none;
            set_datagridview();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            if (row < 0 || row >= dataGridView1.Rows.Count) return;

            m_result = m_distlist[row];
            this.textBox1.Text = m_result.ToString();
        }
    }
}
