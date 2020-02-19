
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
    public class ImageStreamingServer 
        : System.IDisposable
    {

        private System.Collections.Generic.List<System.Net.Sockets.Socket> _Clients;
        private System.Threading.Thread _Thread;
        
        
        /// <summary>
        /// Gets or sets the source of images that will be streamed to the 
        /// any connected client.
        /// </summary>
        // public System.Collections.Generic.IEnumerable<System.Drawing.Image> ImagesSource { get; set; }


        
        /// <summary>
        /// Gets or sets the interval in milliseconds (or the delay time) between 
        /// the each image and the other of the stream (the default is . 
        /// </summary>
        public int Interval { get; set; }
        
        /// <summary>
        /// Gets a collection of client sockets.
        /// </summary>
        public System.Collections.Generic.List<System.Net.Sockets.Socket> Clients { get { return _Clients; } }
        
        /// <summary>
        /// Returns the status of the server. True means the server is currently 
        /// running and ready to serve any client requests.
        /// </summary>
        public bool IsRunning { get { return (_Thread != null && _Thread.IsAlive); } }



        /// <summary>
        /// Gets or sets the source of images that will be streamed to the 
        /// any connected client.
        /// </summary>
        protected ImageStreamSource m_imageSource { get; set; }


        public ImageStreamingServer()
            : this(null)
        { }
        
        
        public ImageStreamingServer(ImageStreamSource imagesSource)
        {
            _Clients = new System.Collections.Generic.List<System.Net.Sockets.Socket>();
            _Thread = null;
            
            this.Interval = 50;

            if (imagesSource == null)
                this.m_imageSource = ImageStreamSource.Instance;
            else
                this.m_imageSource = imagesSource;
        }



        protected int m_port;

        public int Port { get { return this.m_port; }  }




        /// <summary>
        /// Starts the server to accepts any new connections on the specified port.
        /// </summary>
        /// <param name="port"></param>
        public void Start(int port)
        {
            this.m_port = port;

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

                        foreach (System.Net.Sockets.Socket s in _Clients)
                        {
                            try
                            {
                                s.Close();
                            }
                            catch { }
                        } // Next s 
                        
                        _Clients.Clear();
                    } // End Lock _Clients

                    _Thread = null;
                } // End Finally
                
            } // End if (this.IsRunning)
            
        } // End Sub Stop 


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

                foreach (System.Net.Sockets.Socket client in Server.IncommingConnections())
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(ClientThread), client);
                } // Next client 

            }
            catch { }

            this.Stop();
        } // End Sub ServerThread 


        private static byte[] Compress(byte[] data)
        {
            using (System.IO.MemoryStream compressedStream = new System.IO.MemoryStream())
            using (System.IO.Compression.GZipStream zipStream = new System.IO.Compression.GZipStream(compressedStream, System.IO.Compression.CompressionMode.Compress))
            {
                zipStream.Write(data, 0, data.Length);
                zipStream.Close();
                return compressedStream.ToArray();
            }
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
            {
                _Clients.Add(socket);
            }
            
            try
            {
                using (MjpegWriter wr = new MjpegWriter(new System.Net.Sockets.NetworkStream(socket, true)))
                {
                    // Writes the response header to the client.
                    wr.WriteHeader();
                    
                    // Streams the images from the source to the client.

                    
                    //foreach (System.IO.MemoryStream imgStream in this.m_imageSource.Streams)
                    //{
                    //    if (this.Interval > 0)
                    //        System.Threading.Thread.Sleep(this.Interval);

                    //    wr.Write(imgStream);
                    //} // Next imgStream 
                    

                    foreach (byte[] buffer in this.m_imageSource.Buffers)
                    {
                        if (this.Interval > 0)
                            System.Threading.Thread.Sleep(this.Interval);

                        wr.WriteWithHeader(buffer);

#if false 
                        byte[] compressed = Compress(buffer);
                        wr.WriteWithHeader(compressed);
#endif

                    } // Next buffer 

                } // End Using wr 
            }
            catch(System.Exception ex) 
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                lock (_Clients)
                    _Clients.Remove(socket);
            }
        } // End Sub ClientThread 
        
        
        public void Dispose()
        {
            this.Stop();
        }
        
        
    } // End Class ImageStreamingServer 
    
    
    static class SocketExtensions
    {
        
        public static System.Collections.Generic.IEnumerable<System.Net.Sockets.Socket> IncommingConnections(this System.Net.Sockets.Socket server)
        {
            while (true)
                yield return server.Accept();
        }
        
    } // End Class SocketExtensions 
    
    
} // End Namespace rtaNetworking.Streaming 
