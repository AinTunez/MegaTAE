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
    public partial class ArrayEditor : Form
    {
        public byte[] OutArray;
        private bool[] BoolArray;

        public ArrayEditor(byte[] array, string text = "(null)")
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            BoolArray = array.Select(b => b == 1).ToArray();
            ArrayView.SelectedObject = BoolArray;
            Text = text;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            OutArray = BoolArray.Select(b => b ? (byte) 1 : (byte) 0).ToArray();
            Close();
        }
    }
}
