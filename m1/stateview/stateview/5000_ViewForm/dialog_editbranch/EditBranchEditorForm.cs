using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G=stateview.Globals;

namespace stateview._5000_ViewForm.dialog_editbranch
{
    public partial class EditBranchEditorForm : Form
    {
        public bool   m_bConditon;
        public bool   m_bOnlyComment; //Else時はテキストエリアはDisableにする。
        public string m_state;
        public int    m_brcond_index;
        public string m_text;
        public string m_comment;

        public EditBranchEditorForm()
        {
            InitializeComponent();
        }

        private void EditBranchEditorForm_Load(object sender, EventArgs e)
        {
            var zoom = RegistryWork.Get_srctabpanel_zoom();
            this.scintillaBox1.Init(this,zoom, false);

            if (!m_bOnlyComment)
            {
                this.scintillaBox1.Text = m_text;
            }
            else
            {
                this.scintillaBox1.Visible = false;
                this.scintillaBox1.Enabled = false;
            }

            this.textBox_comment.Text = m_comment;

            this.label_use_ext_editor.Visible = m_bConditon;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            m_text    =  this.scintillaBox1.Text ;
            m_comment = this.textBox_comment.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label_use_ext_editor_Click(object sender, EventArgs e)
        {
            var sm = new UseExtEditorControl();
            sm.m_state = m_state;
            sm.m_item = "brcond";
            sm.m_item_line_index = m_brcond_index;
            sm.m_val = G.encode_branch_special_newlinechar( this.scintillaBox1.Text );
            sm.m_parent = this;
            sm.Run();

            if (sm.m_bOk)
            {
                scintillaBox1.Text = G.decode_branch_special_newlinechar( sm.m_output);
                //ok_process();
            }
        }
       // private void ok_process()
       //{
       //     this.DialogResult = DialogResult.OK;
       //     m_text = this.scintillaBox1.Text;
       //     m_comment = this.textBoxPageComment.Text;
       //     m_ref = this.textBoxRef.Text;
       //     this.Close();
       // }

    }
}
