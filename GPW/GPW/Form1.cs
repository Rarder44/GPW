using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GPW.Settings;

namespace GPW
{
    public partial class Form1 : Form
    {
        ProcessManager manager = new ProcessManager();
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private bool trayTipShown = false;
        private bool allowshowdisplay = false;

        private readonly  string configFilePath; 


        RadioButtonGroupManager<Settings.NotificationType> notificationTypeButtons
            = new RadioButtonGroupManager<Settings.NotificationType>();


        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);   
        }


        public Form1()
        {
            InitializeComponent();

            notificationTypeButtons.Add(radioButton_Msg, Settings.NotificationType.MessageBox);
            notificationTypeButtons.Add(radioButton_Toast, Settings.NotificationType.Toast);

            notificationTypeButtons.SelectionChanged+= (s, e) =>
            {
                Settings.Notification = e;
            };

            notificationTypeButtons.checkRadio(Settings.Notification);


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


            //creo/leggo il file di configurazione in %appdata%\GPW\processes.txt
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "GPW");
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }
            configFilePath = Path.Combine(appFolder, "processes.txt");



            flowLayoutPanel1.SizeChanged += (s, e) =>
            {
                foreach (Control ctrl in flowLayoutPanel1.Controls)
                    ctrl.Width = flowLayoutPanel1.ClientSize.Width - 10;
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        
            ProcessConfigReader.ReadOrCreate(configFilePath)
                .ForEach(p => AddProcess(p));

            string directory = Path.GetDirectoryName(configFilePath);
            string fileName = Path.GetFileName(configFilePath);
            FileSystemWatcher watcher = new FileSystemWatcher(directory, fileName);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += async (object sender1, FileSystemEventArgs e1) =>
            {
                await Task.Delay(100);
                clearProcesses();
                ProcessConfigReader.ReadOrCreate(configFilePath)
                .ForEach(p => AddProcess(p));
            };
            watcher.EnableRaisingEvents = true;
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
            if (e.CloseReason == CloseReason.UserClosing)   //clicco sulla X del form / rclick -> esci
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                trayIcon.Visible = false;
                base.OnFormClosing(e);
            }
               
        }

        public void clearProcesses()
        {
            this.Invoke((MethodInvoker)delegate
            {
                flowLayoutPanel1.Controls.Clear();
            });

            manager.ClearListeners();
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
                    
                    String str = InsultingNotifier.GetRandomMessage(e.ProcessNameTrigger);

                    if (Settings.Notification == Settings.NotificationType.Toast)
                    {
                        new ToastContentBuilder()
                            .AddArgument("action", "viewConversation")
                            .AddText("Process Not Running")
                            .AddText(str)
                            .Show(); // Show the toast
                    }
                    else if (Settings.Notification == Settings.NotificationType.MessageBox)
                    {
                        MessageBox.Show(str, "Process State Changed", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                    else
                    {
                        throw new NotImplementedException("Tipo di notifica non implementato.");
                    }

                }
            };

            manager.AddListener(listener);

            this.Invoke((MethodInvoker)delegate
            {
                flowLayoutPanel1.Controls.Add(gui);
            });

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = configFilePath,
                UseShellExecute = true
            });
        }

       
    }
}
