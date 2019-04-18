using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MegaTAE
{
    public partial class GameChooser : Form
    {
        public bool IsSekiro;

        public GameChooser()
        {
            InitializeComponent();
            DialogResult = DialogResult.Abort;
        }

        private void ds3Btn_Click(object sender, EventArgs e)
        {
            IsSekiro = false;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void sekiroBtn_Click(object sender, EventArgs e)
        {
            IsSekiro = true;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
