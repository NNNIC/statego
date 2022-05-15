using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StateViewer_starter2
{
    public partial class CreateNewDetailTextFrom:Form
    {
        public bool m_bUndefined = false;

        public CreateNewDetailTextFrom()
        {
            InitializeComponent();
        }

        private void CreateNewDetailTextFrom_Load(object sender,EventArgs e)
        {
            WORK.UpdateByInputText(m_bUndefined);
            textBoxStarterKitDescription.Select(0,0);
        }
    }
}
