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
    public partial class Form1 : Form
    {
        ProcessManager manager = new ProcessManager();
        public Form1()
        {
            InitializeComponent();

            ProcessConfigReader.ReadOrCreate("processes.txt")
                .ForEach(p => AddProcess(p));

                       
        }

        public void AddProcess(string processPattern)
        {

            var gui = new ProcessGUI
            {
                ProcessName = processPattern,
                ProcessState = LedState.Off
            };


            var listener = new ProcessListener(processPattern);
            listener.ProcessStateChanged += (s, e) =>
            {
                gui.ProcessState = e.State == ProcessState.Running ? LedState.On : LedState.Off;
            };

            manager.AddListener(listener);
           
            flowLayoutPanel1.Controls.Add(gui);
        }

     
    }
}
