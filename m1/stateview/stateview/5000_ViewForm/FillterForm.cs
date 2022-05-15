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

namespace stateview._5000_ViewForm
{
    public partial class FillterForm:Form
    {
        public FillterForm()
        {
            InitializeComponent();
        }

        private void FillterForm_Load(object sender,EventArgs e)
        {
            DialogResult = DialogResult.None;
            if (G.max_num_of_states != int.MaxValue)
            {
                textBoxMaxStates.Text = G.max_num_of_states.ToString();
            }
            textBoxRegex.Text = G.fillter_regextext;
            Location = Cursor.Position;
        }

        private void buttonCancel_Click(object sender,EventArgs e)
        {
            Close();
        }

        private void buttonReset_Click(object sender,EventArgs e)
        {
            textBoxRegex.Text = string.Empty;
            textBoxMaxStates.Text = string.Empty;
        }

        private void buttonOk_Click(object sender,EventArgs e)
        {
            int nums = -1;

            if (string.IsNullOrWhiteSpace(textBoxMaxStates.Text))
            {
                nums = int.MaxValue;
            }
            else
            {
                nums = ParseUtil.ParseInt(textBoxMaxStates.Text,-1);
                if (nums == -1)
                {
                    G.NoticeToUser_warning( G.Localize("w_numbers_not_digits")/*"Numbers were not digits."*/);
                    return;
                }
                if (nums < 10)
                {
                    G.NoticeToUser_warning( G.Localize("w_theminimumnumberis10")/*"The minimum number is 10"*/);
                    return;
                }
            }

            var regextext = textBoxRegex.Text.Trim();
            G.fillter_regextext = regextext;
            G.max_num_of_states = nums;
            DialogResult = DialogResult.OK;
            Close();
            return;
        }

        private void label_help_win_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","fillter-dlg",Globals.system_lang=="jpn");
        }
    }
}
