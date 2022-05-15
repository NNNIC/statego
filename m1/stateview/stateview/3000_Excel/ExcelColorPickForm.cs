using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stateview
{
    public partial class ExcelColorPickForm : Form
    {
        #region 便利
        public static ExcelColorPickForm m_form;
        public static void Start(int cnum, Action<DialogResult> cb, IWin32Window owner=null)
        {
            m_form = new ExcelColorPickForm();
            m_form.m_colornum = cnum;
            m_form.m_cb = cb;
            m_form.Show(owner);
        }
        public static int GetResult() // -1: cancel
        {
            if (m_form != null && m_form.DialogResult == DialogResult.OK)
            {
                return m_form.m_colornum;
            }
            return -1;
        }

        public static int ShowDialog(int cnum,IWin32Window owner=null)
        {
            m_form = new ExcelColorPickForm();
            m_form.m_colornum = cnum;
            m_form.ShowDialog(owner);
            return GetResult();
        }

        #endregion

        public int m_colornum;
        public Action<DialogResult> m_cb;

        public ExcelColorPickForm()
        {
            InitializeComponent();
        }

        private void ExcelColorPickForm_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            if (m_colornum < (int)Libxl.XlColor.COLOR_BLACK || m_colornum > (int)Libxl.XlColor.COLOR_GRAY80)
            {
                m_colornum = (int)Libxl.XlColor.COLOR_BLACK;
            }
            for(var n = (int)Libxl.XlColor.COLOR_BLACK; n <= (int)Libxl.XlColor.COLOR_GRAY80; n++)
            {
                var col = Libxl.GetColor(n);
                var hex = Libxl.GetColorHex(n);

                dataGridView1.Rows.Add(n.ToString(),((Libxl.XlColor)n).ToString(),"",hex);

                dataGridView1[2,dataGridView1.Rows.Count-1].Style.BackColor = Libxl.GetColor(n);
            }

            set_color(m_colornum);

        }

        private void ExcelColorPickForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;

            if (m_cb != null) m_cb(DialogResult);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            var cells = dataGridView1.SelectedCells;
            if (cells!=null && cells.Count>0)
            {
                var row = cells[0].RowIndex;
                var s = dataGridView1[0,row].Value?.ToString();
                var n = ParseUtil.ParseInt(s);
                if (n>0)
                {
                    set_color(n);
                }
                
            }
        }

        private void set_color(int n)
        {
            m_colornum = n;

            textBox_index.Text = m_colornum.ToString();
            textBox_value.Text = ((Libxl.XlColor)m_colornum).ToString();
            textBox_view.BackColor = Libxl.GetColor(m_colornum);
            textBox_hex.Text = Libxl.GetColorHex(m_colornum);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
