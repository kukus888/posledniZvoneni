using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Codecs.WAV;
using System.Threading;
using CSCore.Streams;
using System.Diagnostics;

namespace posledniZvoneni
{
    class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var zvuk = new System.Media.SoundPlayer(Properties.Resources._50HzSquare);
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            // Magie, máš screenshot v bmpScreenshot
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Cursor.Hide();
            var t = new Thread(new ThreadStart(() => Frm(bmpScreenshot)));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            zvuk.Play();
            do
            {
                if (Form1.IsKeyLocked(Keys.Scroll))
                {
                    t.Abort();
                    break;
                }
            } while (t.IsAlive);
        }
        public static void Frm(Bitmap bmpScreenshot) {
            try
            {
                Application.Run(new Form1(bmpScreenshot, Properties.Resources.bsod));
            }
            catch (Exception e) { }
        }
    }
}
