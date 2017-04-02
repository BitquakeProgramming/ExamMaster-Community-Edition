using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamMaster.Frontend
{
    public partial class RoundProgress : UserControl
    {

        private float time = 0;
        private float easing = 0;
        private float reachedPercentPoints = 100f;

        public RoundProgress()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void RoundProgress_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            int width = Width / 8;
            int innerWidth = width * 2;

            g.FillEllipse(Brushes.LightGray, 0, 0, Width, Height);
            Color lerped;
            if (easing > 0.75f)
            {
                lerped = Lerp(Color.Yellow, Color.Green, (easing - 0.75f) * 4f);
            }
            else
            {
                lerped = Lerp(Color.Red, Color.Yellow, easing * 4f);
            }
            g.FillPie(new SolidBrush(lerped), 0, 0, Width, Height, -90, easing * (ReachedPercentPoints / 100f * 360f));
            g.FillEllipse(Brushes.MintCream, width, width, Width - innerWidth, Height - innerWidth);

            string reachedString = "" + (int)Math.Round((easing * ReachedPercentPoints));
            SizeF size = g.MeasureString(reachedString, Font);
            g.DrawString(reachedString,Font, Brushes.Green, Width / 2 - size.Width / 2, Height / 2 - size.Height / 2);

        }

        private Color Lerp(Color a, Color b, float factor)
        {
            float f = Math.Max(Math.Min(factor, 1), 0);
            int A = (int)((b.A - a.A) * f + a.A);
            int R = (int)((b.R - a.R) * f + a.R);
            int G = (int)((b.G - a.G) * f + a.G);
            int B = (int)((b.B - a.B) * f + a.B);
            return Color.FromArgb(A, R, G, B);
        }

        private float EaseInOut(float t, float b, float c, float d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t * t + b;
            t -= 2;
            return -c / 2 * (t * t * t * t - 2) + b;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time += timer1.Interval * 0.001f * 0.1f; // interval * (1/1000) * (1/duration);
            if (time > 1) time = 0;
            easing = EaseInOut(time, 0, 1f, 1);
            Refresh();
        }

        public float ReachedPercentPoints
        {
            get
            {
                return reachedPercentPoints;
            }

            set
            {
                reachedPercentPoints = value;
            }
        }
    }
}
