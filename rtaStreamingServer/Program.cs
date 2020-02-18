
using Gdk;
using Gtk;
using GLib;
using Pango;
using PangoSharp;
using GLibSharp;
using GtkSharp;
using libRtaNetworkStreaming;



namespace rtaStreamingServer
{

    using rtaNetworking.Linux;

    public class Program
    {
        
        
        public static void TestGDK()
        {
            // https://github.com/GtkSharp/GtkSharp
            Gdk.Window window = Gdk.Global.DefaultRootWindow;
            if (window != null)
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
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                for (int i = 0; i < 100; ++i)
                {
                    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                    stopWatch.Start();

                    using (System.Drawing.Bitmap bmp = rtaNetworking.Linux.LinScreen.CopyFromScreenX11())
                    {
                        // System.Console.WriteLine(System.DateTime.Now.ToString("HH:mm:ss.fff"));
                        // bmp.Save("screenshot" + i.ToString() + ".bmp");    
                    } // End Using bmp 

                    stopWatch.Stop();
                    System.Console.WriteLine(stopWatch.ElapsedMilliseconds); // Mean value 370ms...
                } // Next i
            } // End if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux)) 

            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                for (int i = 0; i < 100; ++i)
                {
                    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                    stopWatch.Start();

                    using (System.Drawing.Bitmap bmp = rtaNetworking.Windows.WindowsScreenshotWithCursor.CreateSingleScreenshot())
                    {
                        // System.Console.WriteLine(System.DateTime.Now.ToString("HH:mm:ss.fff"));
                        // bmp.Save("screenshot" + i.ToString() + ".bmp");    
                    } // End Using bmp 

                    stopWatch.Stop();
                    System.Console.WriteLine(stopWatch.ElapsedMilliseconds); // Mean value 37ms
                } // Next i
            } // End if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux)) 

        } // End Sub PerformanceTest 
        


        public static void TestX11_Simple()
        {
            System.IntPtr display = LibX11Functions.XOpenDisplay(System.IntPtr.Zero);
            
            int defaultScreen = LibX11Functions.XDefaultScreen(display);
            System.UIntPtr window = LibX11Functions.XRootWindow(display, defaultScreen);
            
            int screen_width = LibX11Functions.DisplayWidth(display, defaultScreen);
            int screen_height = LibX11Functions.DisplayHeight(display, defaultScreen);
            
            XWindowAttributes xa = new XWindowAttributes();
            LibX11Functions.XGetWindowAttributes(display, window, ref xa);
            System.Console.WriteLine(xa.width);
            System.Console.WriteLine(xa.height);

            int AllPlanes = ~0;
            System.UIntPtr AllPlanes2 = new System.UIntPtr((uint)AllPlanes);
            /*
            XImage* img = LibX11Functions.XGetImage2(display, window, 0, 0, (uint) xa.width, (uint)xa.height
                , AllPlanes2, LinScreen.ZPixmap);
            */
            
            
            // System.IntPtr image = LibX11Functions.XGetImage(display, window, 0, 0, (uint) xa.width, (uint)xa.height
            //     , AllPlanes2, LinScreen.ZPixmap);
            
            tt.Foo(display, window, 0, 0, (uint) xa.width, (uint)xa.height
                , AllPlanes2, LinScreen.ZPixmap);
            
            
            // XImage img = System.Runtime.InteropServices.Marshal.PtrToStructure<XImage>(image);
            
            
            // System.Console.WriteLine(img.bitmap_bit_order);
            // System.Console.WriteLine(img.bits_per_pixel);
            // System.Console.WriteLine(img.width);
            // System.Console.WriteLine(img.height);
            // System.Console.WriteLine(img.data);
            
            // // // // // // 
            
            // System.Console.WriteLine(img->bitmap_bit_order);
            // System.Console.WriteLine(img->width);
            // System.Console.WriteLine(img->height);
            // System.Console.WriteLine(img->data);
            
            // rtaNetworking.Linux.tt.foo();
            
            // LibX11Functions.XDestroyImage2(img);
            
            
            LibX11Functions.XCloseDisplay(display);
        }




        // dotnet publish -f netcoreapp2.1 -c Release -r linux-x64
        // dotnet publish -f netcoreapp3.1 -c Release -r linux-x64
        static void Main(string[] args)
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                Xorg.API.XInitThreads();
            } // End if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < 100; ++i)
            {
                sw.Start ();
                // System.Console.WriteLine(System.DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss.fff"));
                tt.TestX11();
                sw.Stop();
                System.Console.WriteLine(sw.ElapsedMilliseconds);
                sw.Reset();
            }

            
            
            // PerformanceTest();

            // https://www.x.org/releases/X11R7.5/doc/man/man3/XInitThreads.3.html
            // TestScreenshot();
            // System.Drawing.Bitmap dstImage = rtaStreamingServer.LinuxScreenShot.GetScreenshot();

            // RunServer();

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


            System.Timers.Timer tmr = new System.Timers.Timer(200);
            tmr.Elapsed += OnTimer1_Tick;
            tmr.AutoReset = true;
            tmr.Enabled = true;

            BrowserWrapper.OpenBrowser(link);
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
