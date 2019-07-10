using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using AxShockwaveFlashObjects;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace posledniZvoneni
{
    public partial class Form1 : Form
    {
        private AxShockwaveFlash swf = new AxShockwaveFlash();
        static byte[] data;
        static Socket socket;
        public Form1(Bitmap pozadi,Bitmap bsod)
        {
            /* //ODKOMENTUJ BLOK PRO POUZITI TCP? TREBA DODELAT!
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345));//zde zmen IP a port
            socket.Listen(1);
            Socket accepteddata = socket.Accept();
            data = new byte[accepteddata.SendBufferSize];
            int j = accepteddata.Receive(data);
            byte[] adata = new byte[j];
            for (int i = 0; i < j; i++)
                adata[i] = data[i];
            string dat = Encoding.Default.GetString(adata);
            */
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
            Thread.Sleep(3000);

            swf.BeginInit();
            swf.Dock = DockStyle.Fill;
            swf.Name = "PosledniZvoneni";
            swf.SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            swf.TabIndex = 0;
            swf.EndInit();
            this.Controls.Add(swf);
            string filePath = Application.StartupPath;
            swf.LoadMovie(0, filePath + @"\movieNever.swf");//!!!!!!!pro definovani videa pozmen nazev v uvozovkach !!!!!!!!!!!
            swf.Play();
        }
    }
}
