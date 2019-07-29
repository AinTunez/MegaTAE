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
    public partial class TAE_TypeChooser : Form
    {
        public Games Game;
        public dynamic Event
        {
           get
           {
                if (Event4 != null) return Event4;
                if (Event3 != null) return Event3;
                return Event2;
           }
        }
        public TAE4.Event Event4;
        public TAE3.Event Event3;
        public TAE2.Event Event2;
        public int GroupIndex;

        public TAE_TypeChooser(Games game)
        {
            InitializeComponent();
            Game = game;
            if (Game == Games.Sekiro) EventTypeBox.DataSource = Enum.GetValues(typeof(TAE4.EventType));
            else if (Game == Games.DS3) EventTypeBox.DataSource = Enum.GetValues(typeof(TAE3.EventType));
            else EventTypeBox.DataSource = Enum.GetValues(typeof(TAE2.EventType));
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
            if (Game == Games.Sekiro) Event4 = TAE4.Event.EventFromType((TAE4.EventType)EventTypeBox.SelectedItem);
            else if (Game == Games.DS3) Event3 = TAE3.Event.EventFromType((TAE3.EventType)EventTypeBox.SelectedItem);
            else Event2 = TAE2.Event.EventFromType((TAE2.EventType)EventTypeBox.SelectedItem);
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
