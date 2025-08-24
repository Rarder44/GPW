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
    public enum LedState
    {
        Off,
        On
    }

    public partial class Led : UserControl
    {
        private LedState state = LedState.Off;

        [Browsable(true)]
        [Category("Comportamento")]
        [Description("Stato del LED (On/Off)")]
        public LedState State
        {
            get => state;
            set
            {
                if (state != value)
                {
                    state = value;
                    Invalidate();
                }
            }
        }

        public Led()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Calcola il rettangolo per il cerchio

            Rectangle rect = new Rectangle(
                0,
                0,
                this.Width-1,
                this.Height-1);

            // Colori
            Color ledOn = Color.FromArgb(25, 255, 0);   // Verde chiaro
            Color ledOff = Color.FromArgb(0, 100, 0);      // Verde scuro
            Color borderColor = Color.DarkGreen;

            using (Brush brush = new SolidBrush(state == LedState.On ? ledOn : ledOff))
            using (Pen pen = new Pen(borderColor, 1))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(brush, rect);
                e.Graphics.DrawEllipse(pen, rect);
            }
        }
    }
}

