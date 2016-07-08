
// -------------------------------------------------
// Developed By : Ragheed Al-Tayeb
// e-Mail       : ragheedemail@gmail.com
// Date         : April 2012
// -------------------------------------------------
namespace rtaNetworking.Streaming
{


    /// <summary>
    /// Provides a streaming server that can be used to stream any images source
    /// to any client.
    /// </summary>
    public class ImageStreamingServer : System.IDisposable
    {

        private System.Collections.Generic.List<System.Net.Sockets.Socket> _Clients;
        private System.Threading.Thread _Thread;


        public ImageStreamingServer()
        //: this(Screen.Snapshots(600, 450, true))
            : this(Screen.Snapshots(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height, true))
        {

        }


        public ImageStreamingServer(System.Collections.Generic.IEnumerable<System.Drawing.Image> imagesSource)
        {

            _Clients = new System.Collections.Generic.List<System.Net.Sockets.Socket>();
            _Thread = null;

            this.ImagesSource = imagesSource;
            this.Interval = 50;

        }


        /// <summary>
        /// Gets or sets the source of images that will be streamed to the 
        /// any connected client.
        /// </summary>
        public System.Collections.Generic.IEnumerable<System.Drawing.Image> ImagesSource { get; set; }

        /// <summary>
        /// Gets or sets the interval in milliseconds (or the delay time) between 
        /// the each image and the other of the stream (the default is . 
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Gets a collection of client sockets.
        /// </summary>
        public System.Collections.Generic.IEnumerable<System.Net.Sockets.Socket> Clients { get { return _Clients; } }

        /// <summary>
        /// Returns the status of the server. True means the server is currently 
        /// running and ready to serve any client requests.
        /// </summary>
        public bool IsRunning { get { return (_Thread != null && _Thread.IsAlive); } }


        /// <summary>
        /// Starts the server to accepts any new connections on the specified port.
        /// </summary>
        /// <param name="port"></param>
        public void Start(int port)
        {

            lock (this)
            {
                _Thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ServerThread));
                _Thread.IsBackground = true;
                _Thread.Start(port);
            }

        }


        /// <summary>
        /// Starts the server to accepts any new connections on the default port (8080).
        /// </summary>
        public void Start()
        {
            this.Start(8080);
        }


        public void Stop()
        {

            if (this.IsRunning)
            {
                try
                {
                    _Thread.Join();
                    _Thread.Abort();
                }
                finally
                {

                    lock (_Clients)
                    {

                        foreach (var s in _Clients)
                        {
                            try
                            {
                                s.Close();
                            }
                            catch { }
                        }
                        _Clients.Clear();

                    }

                    _Thread = null;
                }
            }
        }


        /// <summary>
        /// This the main thread of the server that serves all the new 
        /// connections from clients.
        /// </summary>
        /// <param name="state"></param>
        private void ServerThread(object state)
        {
            try
            {
                System.Net.Sockets.Socket Server = new System.Net.Sockets.Socket(
                    System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

                Server.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, (int)state));
                Server.Listen(10);

                System.Diagnostics.Debug.WriteLine(string.Format("Server started on port {0}.", state));

                foreach (System.Net.Sockets.Socket client in Server.IncommingConnectoins())
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(ClientThread), client);
                } // Next client 

            }
            catch { }

            this.Stop();
        }


        /// <summary>
        /// Each client connection will be served by this thread.
        /// </summary>
        /// <param name="client"></param>
        private void ClientThread(object client)
        {
            System.Net.Sockets.Socket socket = (System.Net.Sockets.Socket)client;
            System.Diagnostics.Debug.WriteLine(string.Format("New client from {0}", socket.RemoteEndPoint.ToString()));


            lock (_Clients)
                _Clients.Add(socket);

            try
            {
                using (MjpegWriter wr = new MjpegWriter(new System.Net.Sockets.NetworkStream(socket, true)))
                {

                    // Writes the response header to the client.
                    wr.WriteHeader();

                    // Streams the images from the source to the client.
                    foreach (var imgStream in Screen.Streams(this.ImagesSource))
                    {
                        if (this.Interval > 0)
                            System.Threading.Thread.Sleep(this.Interval);

                        wr.Write(imgStream);
                    }

                }
            }
            catch { }
            finally
            {
                lock (_Clients)
                    _Clients.Remove(socket);
            }
        }


        public void Dispose()
        {
            this.Stop();
        }


    }


    static class SocketExtensions
    {

        public static System.Collections.Generic.IEnumerable<System.Net.Sockets.Socket> IncommingConnectoins(this System.Net.Sockets.Socket server)
        {
            while (true)
                yield return server.Accept();
        }

    }


    static class Screen
    {


        public static System.Collections.Generic.IEnumerable<System.Drawing.Image> Snapshots()
        {
            return Screen.Snapshots(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, true);
        }


        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public System.Int32 cbSize;
            public System.Int32 flags;
            public System.IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        struct POINTAPI
        {
            public int x;
            public int y;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool DrawIcon(System.IntPtr hDC, int X, int Y, System.IntPtr hIcon);

        const System.Int32 CURSOR_SHOWING = 0x00000001;

        public static System.Drawing.Bitmap CaptureScreen(System.Windows.Forms.Screen thisScreen, bool CaptureMouse)
        {
            System.Drawing.Bitmap result = new System.Drawing.Bitmap(thisScreen.Bounds.Width
                , System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
                , System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            try
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(result))
                {
                    g.CopyFromScreen(0, 0, 0, 0, thisScreen.Bounds.Size, System.Drawing.CopyPixelOperation.SourceCopy);

                    if (CaptureMouse)
                    {
                        CURSORINFO pci;
                        pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

                        if (GetCursorInfo(out pci))
                        {
                            if (pci.flags == CURSOR_SHOWING)
                            {
                                DrawIcon(g.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
                                g.ReleaseHdc();
                            }
                        }
                    }
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }



        /// <summary>
        /// Returns a 
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<System.Drawing.Image> Snapshots(int width, int height, bool showCursor)
        {
            System.Drawing.Size size = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
                , System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

            System.Drawing.Bitmap srcImage = new System.Drawing.Bitmap(size.Width, size.Height);
            System.Drawing.Graphics srcGraphics = System.Drawing.Graphics.FromImage(srcImage);

            bool scaled = (width != size.Width || height != size.Height);

            System.Drawing.Bitmap dstImage = srcImage;
            System.Drawing.Graphics dstGraphics = srcGraphics;

            if (scaled)
            {
                dstImage = new System.Drawing.Bitmap(width, height);
                dstGraphics = System.Drawing.Graphics.FromImage(dstImage);
            }

            System.Drawing.Rectangle src = new System.Drawing.Rectangle(0, 0, size.Width, size.Height);
            System.Drawing.Rectangle dst = new System.Drawing.Rectangle(0, 0, width, height);
            System.Drawing.Size curSize = new System.Drawing.Size(32, 32);

            while (true)
            {


                //srcGraphics.CopyFromScreen(0, 0, 0, 0, size);
                srcGraphics.CopyFromScreen(System.Windows.Forms.Screen.AllScreens[1].Bounds.X
                    , System.Windows.Forms.Screen.AllScreens[1].Bounds.Y
                    , 0, 0, size
                );

                /*
                // This results in the wrong cursor...
                if (showCursor)
                    // System.Windows.Forms.Cursors.Default.Draw(srcGraphics,
                    System.Windows.Forms.Cursor.Current.Draw(srcGraphics,
                        new System.Drawing.Rectangle(System.Windows.Forms.Cursor.Position, curSize)
                );
                */


                // https://stackoverflow.com/questions/6750056/how-to-capture-the-screen-and-mouse-pointer-using-windows-apis
                if (showCursor)
                {
                    CURSORINFO pci;
                    pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

                    if (GetCursorInfo(out pci))
                    {
                        if (pci.flags == CURSOR_SHOWING)
                        {
                            DrawIcon(srcGraphics.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
                            srcGraphics.ReleaseHdc();
                        }
                    }
                }

                if (scaled)
                    dstGraphics.DrawImage(srcImage, dst, src, System.Drawing.GraphicsUnit.Pixel);

                yield return dstImage;

            }

            srcGraphics.Dispose();
            dstGraphics.Dispose();

            srcImage.Dispose();
            dstImage.Dispose();

            yield break;
        }


        internal static System.Collections.Generic.IEnumerable<System.IO.MemoryStream> Streams(this System.Collections.Generic.IEnumerable<System.Drawing.Image> source)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            foreach (var img in source)
            {
                ms.SetLength(0);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                yield return ms;
            }

            ms.Close();
            ms = null;

            yield break;
        }


    }


}
