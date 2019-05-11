using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace posledniZvoneni
{
    public partial class Form1 : Form
    {
        public Form1(Bitmap pozadi,Bitmap bsod,Byte[] rick)
        {
            Cursor.Hide();
            this.BackgroundImage = pozadi;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Visible = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.Show();
            Thread.Sleep(5000);
            this.BackgroundImage = bsod;
            this.Refresh();
            Thread.Sleep(3000);
            
        }
    }
}
