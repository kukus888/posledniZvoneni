using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using AxShockwaveFlashObjects;
using System.IO;
using System.Diagnostics;

namespace posledniZvoneni
{
    public partial class Form1 : Form
    {
        private AxShockwaveFlash swf = new AxShockwaveFlash();
        public Form1(Bitmap pozadi,Bitmap bsod)
        {
            Cursor.Hide();
            this.BackgroundImage = pozadi;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Visible = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer,true);
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.Show();
            Stopwatch sw = new Stopwatch();
            Bitmap mys = Properties.Resources.mys;
            sw.Start();
            while (sw.ElapsedMilliseconds<=5000) {
                for (int x = 0; x <= 11; x++) {
                    for (int y = 0; y <= 20; y++) {
                        try
                        {
                            pozadi.SetPixel(x + System.Windows.Forms.Cursor.Position.X, y + System.Windows.Forms.Cursor.Position.Y, mys.GetPixel(x, y));
                        }
                        catch (Exception e) { }
                    }
                }
                this.Refresh();
            }
            sw.Stop();
            this.BackgroundImage = bsod;//0%
            this.Refresh();
            Thread.Sleep(500);//10%

            Thread.Sleep(500);//20%

            Thread.Sleep(500);//30%

            Thread.Sleep(500);//40%

            Thread.Sleep(500);//50%

            Thread.Sleep(500);//60%

            Thread.Sleep(500);//70%

            Thread.Sleep(500);//80%

            Thread.Sleep(500);//90%
            
            Thread.Sleep(500);//100%
            swf.BeginInit();
            swf.Dock = DockStyle.Fill;
            swf.Name = "PosledniZvoneni";
            swf.SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            swf.TabIndex = 0;
            swf.EndInit();
            this.Controls.Add(swf);
            byte[] data = Properties.Resources.Never_gonna_give_you_up;//Zde zmen video embedovane v resources.resX souboru
            string filePath = Application.StartupPath;
            swf.LoadMovie(0, filePath + @"\movieNever.swf");//!!!!!!!pro definovani videa pozmen nazev v uvozovkach !!!!!!!!!!!
            swf.Play();
        }
    }
}
