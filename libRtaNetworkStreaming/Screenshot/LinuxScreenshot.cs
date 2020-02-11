
namespace rtaStreamingServer
{


    // https://stackoverflow.com/questions/3732486/take-a-screenshot-of-the-active-window-in-mono/5310777#5310777
    public class LinuxScreenShot
    {
        // http://www.eggheadcafe.com/tutorials/aspnet/064b41e4-60bc-4d35-9136-368603bcc27a/7zip-lzma-inmemory-com.aspx

        private static System.Drawing.Rectangle rectScreenBounds = GetScrBounds();
        //private static System.Drawing.Rectangle rectScreenBounds = System.Windows.Forms.Screen.GetBounds(System.Drawing.Point.Empty);
        private static System.Drawing.Bitmap bmpScreenshot = new System.Drawing.Bitmap(1, 1);
        

        private static System.Drawing.Rectangle GetXorgScreen()
        {
            int screen_width = 0;
            int screen_height = 0;

            System.IntPtr display = Xorg.API.XOpenDisplay(System.IntPtr.Zero);

            if (display == System.IntPtr.Zero)
            {
                System.Console.WriteLine("Error: Failed on XOpenDisplay.\n");
            }
            else
            {
                screen_width = Xorg.API.DisplayWidth(display, Xorg.API.XDefaultScreen(display));
                screen_height = Xorg.API.DisplayHeight(display, Xorg.API.XDefaultScreen(display));

                Xorg.API.XCloseDisplay(display);
                System.Console.WriteLine("Width: " + screen_width.ToString() + " Height: " + screen_height.ToString());
            } // End Else (display == System.IntPtr.Zero)

            return new System.Drawing.Rectangle(0, 0, screen_width, screen_height);
        } // End Function GetXorgScreen


        protected static System.Drawing.Rectangle GetScrBounds()
        {
            // Wouldn't be necessary if GetBounds on mono wasn't buggy.
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                return GetXorgScreen();
            
            return rtaNetworking.Windows.Screen.GetBounds(System.Drawing.Point.Empty);
        } // End Function GetScrBounds
        
        
        // http://jalpesh.blogspot.com/2007/06/how-to-take-screenshot-in-c.html
        // Tools.Graphics.ScreenShot.GetScreenshot();
        public static System.Drawing.Bitmap GetScreenshot()
        {
            /*
            if (this.pictureBox1.Image != null)
                this.pictureBox1.Image.Dispose();
            */
            // if(bmpScreenshot != null) bmpScreenshot.Dispose();
            
            bmpScreenshot = new System.Drawing.Bitmap(rectScreenBounds.Width, rectScreenBounds.Height);
            
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpScreenshot))
            {
                try
                {
                    g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, rectScreenBounds.Size);
                }
                catch (System.Exception ex)
                {
                }
                
            } // End Using g

            return bmpScreenshot;
        } // End Function GetScreenshotImage


        // http://jalpesh.blogspot.com/2007/06/how-to-take-screenshot-in-c.html
        // Tools.Graphics.ScreenShot.GetScreenshot(this.PictureBox1);
        public static void GetScreenshot(rtaNetworking.Windows.PictureBox pbThisPictureBox)
        {
            /*
            if (this.pictureBox1.Image != null)
                this.pictureBox1.Image.Dispose();
            */
            bmpScreenshot.Dispose();
            bmpScreenshot = new System.Drawing.Bitmap(rectScreenBounds.Width, rectScreenBounds.Height);

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpScreenshot))
            {
                g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, rectScreenBounds.Size);
            } // End Using g

            pbThisPictureBox.Image = bmpScreenshot;

        } // End Sub GetScreenshot


        // http://jalpesh.blogspot.com/2007/06/how-to-take-screenshot-in-c.html
        // Tools.Graphics.ScreenShot.SaveScreenshot(@"C:\Users\Stefan.Steiger.COR\Desktop\test.jpg");
        public static void SaveScreenshot(string strFileNameAndPath)
        {
            System.Drawing.Rectangle rectBounds = rtaNetworking.Windows.Screen.GetBounds(System.Drawing.Point.Empty);
            using (System.Drawing.Bitmap bmpScreenshotBitmap = new System.Drawing.Bitmap(rectBounds.Width, rectBounds.Height))
            {
                
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmpScreenshotBitmap))
                {
                    g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, rectBounds.Size);
                } // End Using g

                bmpScreenshotBitmap.Save(strFileNameAndPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            } // End Using

        } // End Sub SaveScreenshot


    } // End  Class ScreenShot


} // End Namespace Tools.Graphics
