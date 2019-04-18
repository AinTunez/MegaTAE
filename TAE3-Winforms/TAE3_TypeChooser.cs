using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;
using System.Windows.Forms;

namespace MegaTAE
{
    public partial class TAE3_TypeChooser : Form
    {

        public TAE3.Event Event;
        public int GroupIndex;

        public TAE3_TypeChooser()
        {
            InitializeComponent();
            EventTypeBox.DataSource = Enum.GetValues(typeof(TAE3.EventType));
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (EventTypeBox.SelectedItem == null)
            {
                MessageBox.Show("You must select an event type.");
                return;
            }

            Event = TAE3.Event.EventFromType((TAE3.EventType)EventTypeBox.SelectedItem);
            GroupIndex = (int) EventGroupBox.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void EventGroupBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
