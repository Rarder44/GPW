using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPW
{
    public partial class ProcessGUI : UserControl
    {
        public ProcessGUI()
        {
            InitializeComponent();
        }

        public LedState ProcessState
        {
            get => led1.State;
            set => led1.State = value;
        }

        public String ProcessName
        {
            get => label1.Text;
            set => label1.Text = value;
        }
    }
}
