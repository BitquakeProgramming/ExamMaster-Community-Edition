using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamMaster.Frontend
{
    public class QuestionListView : ListView
    {
        private Point mouse = new Point(-1,-1);
        
        private bool hasFocus = false;
        public QuestionListView()
        {
            this.OwnerDraw = true;
            DoubleBuffered = true;
            MouseMove += QuestionListView_MouseMove; ;
            
        }

        private void QuestionListView_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X;
            mouse.Y = e.Y;
            hasFocus = false;
            Refresh();
            if (!hasFocus)
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>Löst das <see cref="E:System.Windows.Forms.ListView.DrawItem" />-Ereignis aus.</summary>
        /// <param name="e">Ein <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" />, das die Ereignisdaten enthält. </param>
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);
            Font font = Font;
            if (e.Bounds.Contains(mouse))
            {
                hasFocus = true;
                font = new Font(Font.FontFamily, Font.Size, Font.Style | FontStyle.Underline);
                Cursor = Cursors.Hand;
            }
            Color color = Color.FromArgb(255, 3, 184, 255);
            Brush brush = new SolidBrush(color);
            QuestionItem questionItem = (QuestionItem)e.Item;
            if (questionItem.Selected)
            {
                Color color2 = Color.FromArgb(255, 3, 154, 220);
                Color color3 = Color.FromArgb(255, 3, 134, 180);
                brush = new LinearGradientBrush(new Rectangle(0, 0, 1, 1), color3, color2, LinearGradientMode.ForwardDiagonal);
                ((LinearGradientBrush)brush).WrapMode = WrapMode.TileFlipXY;
            }
            else if (questionItem.State == QuestionState.CHECKED_SOME)
            {
                Color color2 = Color.FromArgb(255, 3, 124, 190);
                Color color3 = Color.FromArgb(255, 3, 104, 150);
                brush = new LinearGradientBrush(new Rectangle(0,0,1,1), color3, color2, LinearGradientMode.ForwardDiagonal);
                ((LinearGradientBrush) brush).WrapMode = WrapMode.TileFlipXY;
            }
            
            SizeF size = e.Graphics.MeasureString("" + (e.ItemIndex + 1), Font);
            e.Graphics.FillRectangle(brush, e.Bounds);
            e.Graphics.DrawString("" + (e.ItemIndex + 1), font, Brushes.White, e.Bounds.X + e.Bounds.Width / 2 - size.Width / 2, e.Bounds.Y + e.Bounds.Height / 2 - size.Height / 2);
        }
    }

    public enum QuestionState
    {
        NONE,
        CHECKED_SOME
    }
}
