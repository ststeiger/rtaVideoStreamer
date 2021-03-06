﻿
namespace rtaStreamingServer
{

    using libRtaNetworkStreaming;
    using rtaNetworking.Linux;
    
    
    public class Program
    {
        
        
        // Linux is 4.3 times faster ! 
        public static void PerformanceTest()
        {
            // Average (including first): 12.2 ms
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                for (int i = 0; i < 100; ++i)
                {
                    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                    stopWatch.Start();

                    //using (System.Drawing.Bitmap bmp = rtaNetworking.Linux.LinScreen.CopyFromScreenX11())
                    //{
                    //    // System.Console.WriteLine(System.DateTime.Now.ToString("HH:mm:ss.fff"));
                    //    // bmp.Save("screenshot" + i.ToString() + ".bmp");    
                    //} // End Using bmp 

                    byte[] ba = rtaNetworking.Linux.SafeX11.X11Screenshot(true);
                    // System.IO.File.WriteAllBytes("/tmp/compress.jpg", ba);
                    
                    stopWatch.Stop();
                    System.Console.WriteLine(stopWatch.ElapsedMilliseconds); // Mean value 370ms...
                } // Next i
                
            } // End if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux)) 


            // Average (except first): 52.1 ms
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                for (int i = 0; i < 100; ++i)
                {
                    System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                    stopWatch.Start();

                    byte[] ba = null; 

                    using (System.Drawing.Bitmap bmp = rtaNetworking.Windows.WindowsScreenshotWithCursor.CreateSingleScreenshot())
                    {
                        // System.Console.WriteLine(System.DateTime.Now.ToString("HH:mm:ss.fff"));
                        // bmp.Save("screenshot" + i.ToString() + ".bmp");    

                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                            ba = ms.ToArray();
                        }

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
            
            SafeX11.SlowScreenshot(display, window, 0, 0, (uint) xa.width, (uint)xa.height
                , AllPlanes2, LinScreen.ZPixmap, true);
            
            
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




        public static void TestX11_Shared()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            for (int i = 0; i < 100; ++i)
            {
                sw.Start ();
                // System.Console.WriteLine(System.DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss.fff"));
                byte[] res = SafeX11.X11Screenshot(true);
                sw.Stop();
                System.Console.WriteLine(sw.ElapsedMilliseconds);
                sw.Reset();
            } // Next i 
            
        } // End Sub TestX11_Shared 
        

        public static void ShowActiveTcpConnections()
        {
            System.Console.WriteLine("Active TCP Connections");
            System.Net.NetworkInformation.IPGlobalProperties properties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            
            // System.Console.WriteLine("Computer name: {0}", properties.HostName);
            // System.Console.WriteLine("Domain name:   {0}", properties.DomainName);
            // System.Console.WriteLine("Node type:     {0:f}", properties.NodeType);
            // System.Console.WriteLine("DHCP scope:    {0}", properties.DhcpScopeName);
            // System.Console.WriteLine("WINS proxy?    {0}", properties.IsWinsProxy);
            
            System.Net.NetworkInformation.TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            foreach (System.Net.NetworkInformation.TcpConnectionInformation c in connections)
            {
                System.Console.WriteLine("{0} <==> {1}",
                    c.LocalEndPoint.ToString(),
                    c .RemoteEndPoint.ToString());
            }
        }
        
        
        public static string GetLocalIpFromDnsProvider()
        {
            string localIP;
            using (System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0))
            {
                //socket.Connect("8.8.8.8", 65530); // Google
                socket.Connect("1.1.1.1", 65530); // CloudFlare
                System.Net.IPEndPoint endPoint = socket.LocalEndPoint as System.Net.IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            return localIP;
        }

        public static string GetPublicIPAddress()
        {  
            string result = string.Empty;
            
            string[] checkIPUrl =
            {
                "http://ipv4.icanhazip.com",
                "http://icanhazip.com",
                "https://ipinfo.io/ip",
                "https://checkip.amazonaws.com/",
                "https://api.ipify.org",
                "https://icanhazip.com",
                "https://wtfismyip.com/text"
            };
            
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                                               "(compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                foreach (var url in checkIPUrl)
                {
                    try
                    {
                        result = client.DownloadString(url);
                    }
                    catch
                    {
                    }

                    if (!string.IsNullOrEmpty(result))
                        break;
                }
            }

            if (string.IsNullOrEmpty(result))
            {

                try
                {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        result = wc.DownloadString("http://checkip.dyndns.org");
                        string[] a = result.Split(':');
                        string a2 = a[1].Substring(1);
                        string[] a3 = a2.Split('<');
                        result = a3[0];
                    }
                }
                catch
                { }

            }

            if (string.IsNullOrEmpty(result))
                result = "127.0.0.1";
            
            return result.Trim(new char[] { ' ', '\t', '\r', '\n', '\v', '\f'});
        }
        
        
        public static string GetLocalIpAddress()
        {
            string ipAddress = null;
            
            try
            {
                ipAddress = GetLocalIpFromDnsProvider();
            }
            catch 
            {
                try
                {
                    ipAddress = GuessLocalIpAddress();
                }
                catch 
                {
                    try
                    {
                        ipAddress = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().HostName;
                    }
                    catch 
                    {
                        ipAddress = "127.0.0.1";
                    }
                }
            }
            
            return ipAddress;
        } // End Function GetLocalIpAddress 

        public static string GuessLocalIpAddress()
        {
            return GuessLocalIpAddress(null);
        }
        
        
        public static string GuessLocalIpAddress(System.Net.NetworkInformation.NetworkInterfaceType? ofType)
        {
            
            
            System.Net.NetworkInformation.UnicastIPAddressInformation mostSuitableIp = null;
            System.Net.NetworkInformation.NetworkInterface[] networkInterfaces = 
                System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            
            foreach (System.Net.NetworkInformation.NetworkInterface network in networkInterfaces)
            {
                if (ofType.HasValue && network.NetworkInterfaceType != ofType)
                    continue;
                
                if (network.OperationalStatus != System.Net.NetworkInformation.OperationalStatus.Up)
                    continue;
                
                if (network.Description.ToLower().Contains("virtual")
                    || network.Description.ToLower().Contains("pseudo")
                )
                    continue;
                
                System.Net.NetworkInformation.IPInterfaceProperties properties = network.GetIPProperties();
                
                if (properties.GatewayAddresses.Count == 0)
                    continue;
                
                foreach (System.Net.NetworkInformation.UnicastIPAddressInformation address in properties.UnicastAddresses)
                {
                    if (address.Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                        continue;
                    
                    if (System.Net.IPAddress.IsLoopback(address.Address))
                        continue;
                    
                    if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)
                        && !address.IsDnsEligible)
                    {
                        if (mostSuitableIp == null)
                            mostSuitableIp = address;
                        continue;
                    }
                    
                    // The best IP is the IP got from DHCP server
                    if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)
                        && address.PrefixOrigin != System.Net.NetworkInformation.PrefixOrigin.Dhcp)
                    {
                        if (mostSuitableIp == null || !mostSuitableIp.IsDnsEligible)
                            mostSuitableIp = address;
                        continue;
                    }
                    
                    // System.Console.WriteLine(address.IPv4Mask); // Subnet Mask
                    return address.Address.ToString();
                }
            }
            
            return mostSuitableIp != null 
                ? mostSuitableIp.Address.ToString()
                : "";
        }
        
        
        // https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore21
        // dotnet publish -f netcoreapp2.1 -c Release -r linux-x64 -o /opt/desktop-stream/
        // dotnet publish -f netcoreapp3.1 -c Release -r linux-x64 -o /opt/desktop-stream/
        static void Main(string[] args)
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                Xorg.API.XInitThreads();
            } // End if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))

            // TestX11_Shared();
            // PerformanceTest();
            
            ShowActiveTcpConnections();
            
            // https://www.x.org/releases/ X11R7.5/doc/man/man3/XInitThreads.3.html
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



        private static rtaNetworking.Streaming.ImageStreamingServer s_server;
        private static System.DateTime time = System.DateTime.MinValue;


        static void RunServer()
        {
            s_server = new rtaNetworking.Streaming.ImageStreamingServer();
            s_server.Interval = 35;
            
            s_server.Start(8080);
            
            string port = s_server.Port.ToString(System.Globalization.CultureInfo.InvariantCulture);
            string link = $"http://{System.Environment.MachineName}:{port}";
            System.Console.WriteLine("Public:  http://{0}:{1}", GetPublicIPAddress(), port);
            System.Console.WriteLine("Private: http://{0}:{1}", GetLocalIpAddress(), port);
            System.Console.WriteLine("NetBios: http://{0}:{1}", System.Environment.MachineName, port);
                
#if false
            System.Timers.Timer tmr = new System.Timers.Timer(200);
            tmr.Elapsed += OnTimer1_Tick;
            tmr.AutoReset = true;
            tmr.Enabled = true;    
#endif
            
            BrowserWrapper.OpenBrowser(link);
        } // End Sub RunServer 


        static void StopServer()
        {
            if (s_server != null)
                s_server.Stop();
        } // End Sub StopServer 


        private static void OnTimer1_Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            System.Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);

            int count = (s_server.Clients != null) ? s_server.Clients.Count : 0;

            System.Console.Write("Clients: ");
            System.Console.WriteLine(count.ToString());
        } // End Sub OnTimer1_Tick 


    } // End Class Program 


} // End Namespace rtaStreamingServer 
