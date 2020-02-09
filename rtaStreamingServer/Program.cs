
namespace rtaStreamingServer
{


    class Program
    {



        static void Main(string[] args)
        {
            RunServer();

            // https://www.cyotek.com/blog/capturing-screenshots-using-csharp-and-p-invoke

            

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();

            _Server.Stop();
        }



        static void TestScreenshot()
        {
            System.Drawing.Bitmap bmp = CaptureEntireDesktop.CaptureDesktop();
            System.Console.WriteLine(bmp);
            bmp.Save(@"d:\Screenshot.png", System.Drawing.Imaging.ImageFormat.Png);
        }



        private static rtaNetworking.Streaming.ImageStreamingServer _Server;
        private static System.DateTime time = System.DateTime.MinValue;

        static void RunServer()
        {
            string link = string.Format("http://{0}:8080", System.Environment.MachineName);

            _Server = new rtaNetworking.Streaming.ImageStreamingServer();


            _Server.Start(8080);


            System.Timers.Timer tmr = new System.Timers.Timer(200);
            tmr.Elapsed += timer1_Tick;
            // tmr.AutoReset = true;
            tmr.Enabled = true;

            BrowserWrapper.OpenBrowser(link);
        }


        private static void timer1_Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            System.Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);

            int count = (_Server.Clients != null) ? _Server.Clients.Count : 0;

            System.Console.Write("Clients: ");
            System.Console.WriteLine(count.ToString());
        }


    }


}
