
using Gdk;
using Gtk;
using GLib;
using Pango;
using PangoSharp;
using GLibSharp;
using GtkSharp;


namespace rtaStreamingServer
{


    class Program
    {
        
        
        public static void TestGDK()
        {
            // https://github.com/GtkSharp/GtkSharp
            Gdk.Window window = Gdk.Global.DefaultRootWindow;
            if (window!=null)
            {           
                Gdk.Pixbuf pixBuf = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8, 
                    window.Screen.Width, window.Screen.Height);      
                
                // Gdk.CursorType.Arrow
                // Gdk.Display.Default.GetPointer();
                // Gdk.Cursor.GetObject().
                // Gdk.Display.Default.GetMonitor(0).Geometry.Bottom
                // Gdk.Display.Default.GetMonitor(0).IsPrimary
                // Gdk.Display.Default.NMonitors
                // Gdk.Display.Default.GetMonitorAtPoint()
                // Gdk.Display.Default.GetPointer();
                
                
                // pixBuf.dr
                // pixBuf.GetPixelsWithLength()
                // Gdk.Pixbuf buf;
                
                // pixBuf.GetFromDrawable(window, Gdk.Colormap.System, 0, 0, 0, 0, window.Screen.Width, window.Screen.Height);          
                pixBuf.ScaleSimple(400, 300, Gdk.InterpType.Bilinear);
                pixBuf.Save("screenshot0.jpeg", "jpeg");
            } // End if (window!=null)
            
        } // End Sub TestGDK()
        
        
        public static void PerformanceTest()
        {
            for (int i = 0; i < 100; ++i)
            {
                using (System.Drawing.Bitmap bmp = rtaNetworking.Linux.LinScreen.CopyFromScreenX11())
                {
                    System.Console.WriteLine(System.DateTime.Now.ToString("HH:mm:ss.fff"));
                    // bmp.Save("screenshot" + i.ToString() + ".bmp");    
                } // End Using bmp 
                
            } // Next i
            
        } // End Sub PerformanceTest 
        
        
        public static void TestXGetImage()
        {
            // libRtaNetworkStreaming.XImage* foo = libRtaNetworkStreaming.XLib.XGetImage();
            
            
            
            // System.IntPtr pImage;
            // libRtaNetworkStreaming.XImage block1 = (libRtaNetworkStreaming.XImage)System.Runtime.InteropServices.Marshal.PtrToStructure(pImage, typeof(libRtaNetworkStreaming.XImage));
        } // End Sub TestXGetImage
        
        
        
        // dotnet publish -f netcoreapp2.1 -c Release -r linux-x64
        // dotnet publish -f netcoreapp3.1 -c Release -r linux-x64
        static void Main(string[] args)
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                Xorg.API.XInitThreads();
            } // End if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            
            // PerformanceTest();
            
            // https://www.x.org/releases/X11R7.5/doc/man/man3/XInitThreads.3.html
            // TestScreenshot();
            // System.Drawing.Bitmap dstImage = rtaStreamingServer.LinuxScreenShot.GetScreenshot();

            RunServer();

            // https://www.cyotek.com/blog/capturing-screenshots-using-csharp-and-p-invoke


            System.Console.WriteLine(" --- Press any key to continue --- ");
            // System.Console.ReadKey();
            WaitForKeyPress();

            StopServer();
        } // End Sub Main 


        static void WaitForKeyPress()
        {

            while (!System.Console.KeyAvailable)
            {
                System.Threading.Thread.Sleep(100);
            } // Whend 

            System.Console.ReadKey(); // Flush
        } // End Sub WaitForKeyPress 


        static void WaitForEnter()
        {
            System.ConsoleKey cc = default(System.ConsoleKey);

            do
            {
                // THIS IS MADNESS!!!   Madness huh?   THIS IS SPARTA!!!!!!! 
                while (!System.Console.KeyAvailable)
                {
                    System.Threading.Thread.Sleep(100);
                }

                cc = System.Console.ReadKey().Key;

                if (cc == System.ConsoleKey.C)
                    System.Console.Clear();

            } while (cc != System.ConsoleKey.Enter);
        } // End Sub WaitForEnter 


        static void TestScreenshot()
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                TestLinuxScreenshot();
                return;
            }

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                TestWindowsScreenshot();
                return;
            }

            throw new System.NotImplementedException("Screenshot functionality not implemented for your platform...");
        } // End Sub TestScreenshot 


        static void TestLinuxScreenshot()
        {
            System.Drawing.Bitmap bmp = LinuxScreenShot.GetScreenshot();
            System.Console.WriteLine(bmp);
            bmp.Save(@" Screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
        } // End Sub TestLinuxScreenshot 


        static void TestWindowsScreenshot()
        {
            System.Drawing.Bitmap bmp = CaptureEntireDesktop.CaptureDesktop();
            System.Console.WriteLine(bmp);
            bmp.Save(@"Screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
        } // End Sub TestWindowsScreenshot 



        private static rtaNetworking.Streaming.ImageStreamingServer _Server;
        private static System.DateTime time = System.DateTime.MinValue;
        
        
        static void RunServer()
        {
            string link = string.Format("http://{0}:8080", System.Environment.MachineName);

            _Server = new rtaNetworking.Streaming.ImageStreamingServer();
            _Server.Interval = 35;
             
            _Server.Start(8080);


            // System.Timers.Timer tmr = new System.Timers.Timer(200);
            // tmr.Elapsed += OnTimer1_Tick;
            // tmr.AutoReset = true;
            // tmr.Enabled = true;

            // BrowserWrapper.OpenBrowser(link);
        } // End Sub RunServer 


        static void StopServer()
        {
            if (_Server != null)
                _Server.Stop();
        } // End Sub StopServer 


        private static void OnTimer1_Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            System.Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);

            int count = (_Server.Clients != null) ? _Server.Clients.Count : 0;

            System.Console.Write("Clients: ");
            System.Console.WriteLine(count.ToString());
        } // End Sub OnTimer1_Tick 


    } // End Class Program 


} // End Namespace rtaStreamingServer 
