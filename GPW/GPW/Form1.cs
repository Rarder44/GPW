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
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private bool trayTipShown = false;

        private bool allowshowdisplay = false;


        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
        }


        public Form1()
        {
            InitializeComponent();

            // Inizializza la tray icon e il menu
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Esci",null, OnExit);
            

            trayIcon = new NotifyIcon
            {
                Text = "GPW",
                Icon = this.Icon,
                ContextMenuStrip = trayMenu,
                Visible = true
            };

            trayIcon.DoubleClick += TrayIcon_DoubleClick;

            ProcessConfigReader.ReadOrCreate("processes.txt")
                .ForEach(p => AddProcess(p));

            flowLayoutPanel1.SizeChanged += (s, e) =>
            {
                foreach (Control ctrl in flowLayoutPanel1.Controls)
                    ctrl.Width = flowLayoutPanel1.ClientSize.Width - 10;
            };

            
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false; // Nasconde l'icona dalla tray
            Application.Exit();
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.allowshowdisplay = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                if (!trayTipShown)
                {
                    trayIcon.ShowBalloonTip(2000, "GPW", "L'applicazione è ancora attiva nella tray bar.", ToolTipIcon.Info);
                    trayTipShown = true;
                }
                return;
            }
            trayIcon.Visible = false;
            base.OnFormClosing(e);
        }


        public void AddProcess(string processPattern)
        {
            var gui = new ProcessGUI
            {
                ProcessName = processPattern,
                ProcessState = LedState.Off,
                Width = flowLayoutPanel1.ClientSize.Width - 10
            };



            var listener = new ProcessListener(processPattern);
            listener.ProcessStateChanged += (s, e) =>
            {
                gui.ProcessState = e.State == ProcessState.Running ? LedState.On : LedState.Off;
                
                if (e.State == ProcessState.NotRunning)
                {
                    String str = InsultingNotifier.GetRandomMessage(e.ProcessNamePattern);
                    MessageBox.Show(str, "Process State Changed", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);


                }
            };

            manager.AddListener(listener);
            flowLayoutPanel1.Controls.Add(gui);
        }
    }
}
