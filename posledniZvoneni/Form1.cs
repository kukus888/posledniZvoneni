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
            this.BackgroundImage = bsod;
            this.Refresh();
            Thread.Sleep(3000);
            var swf = new AxShockwaveFlashObjects.AxShockwaveFlash();
            swf.BeginInit();
            swf.Dock = DockStyle.Fill;
            swf.Name = "PosledniZvoneni";
            swf.SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            swf.TabIndex = 0;
            swf.EndInit();
            this.Controls.Add(swf);
            byte[] data = Properties.Resources.Never_gonna_give_you_up;//Zde zmen video embedovane v resources.resX souboru
            InitFlashMovie(swf,data);
            /*  MAGIC OF PLAYING SWF FROM RESOURCES.RESX
             *  
             *  Use this link:
             *  https://social.msdn.microsoft.com/Forums/vstudio/en-US/1de60b6b-fc8b-48a5-b41f-1a3d25756700/embedded-flash-object-in-my-c-app?forum=csharpgeneral
             *  search for solution and read it carefully. then jump across stackOverflow sites you find there. voilá, it works!
             */
        }
        private void InitFlashMovie(AxShockwaveFlash flashObj, byte[] swfFile)
        {
            using (MemoryStream stm = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stm))
                {
                    /* Write length of stream for AxHost.State */
                    writer.Write(8 + swfFile.Length);
                    /* Write Flash magic 'fUfU' */
                    writer.Write(0x55665566);
                    /* Length of swf file */
                    writer.Write(swfFile.Length);
                    writer.Write(swfFile);
                    stm.Seek(0, SeekOrigin.Begin);
                    /* 1 == IPeristStreamInit */
                    flashObj.OcxState = new AxHost.State(stm, 1, false, null);
                }
            }
        }
    }
}
