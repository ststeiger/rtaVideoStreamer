
// https://stackoverflow.com/questions/1537587/capture-screen-image-in-c-on-osx
// https://github.com/Acollie/C-Screenshot-OSX/blob/master/C%2B%2B-screenshot/C%2B%2B-screenshot/main.cpp
// https://github.com/ScreenshotMonitor/ScreenshotCapture/blob/master/src/Pranas.ScreenshotCapture/ScreenshotCapture.cs
// https://screenshotmonitor.com/blog/capturing-screenshots-in-net-and-mono/
namespace rtaStreamingServer
{

    // https://github.com/xamarin/xamarin-macios
    

    // https://qiita.com/shimshimkaz/items/18bcf4767143ea5897c7
    public static class OSxScreenshot
    {

        private const string LIBCOREGRAPHICS = "/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics";

        [System.Runtime.InteropServices.DllImport(LIBCOREGRAPHICS)]
        private static extern System.IntPtr CGDisplayCreateImage(System.UInt32 displayId);

        [System.Runtime.InteropServices.DllImport(LIBCOREGRAPHICS)]
        private static extern void CFRelease(System.IntPtr handle);


        public static void TestCapture()
        {
            Foundation.NSNumber mainScreen = (Foundation.NSNumber)AppKit.NSScreen.MainScreen.DeviceDescription["NSScreenNumber"];

            using (CoreGraphics.CGImage cgImage = CreateImage(mainScreen.UInt32Value))
            {
                using (AppKit.NSImage nsImage = new AppKit.NSImage(cgImage, new System.Drawing.SizeF(cgImage.Width, cgImage.Height)))
                {
                    // ImageView.Image = nsImage;
                }
            }
        }


        public static CoreGraphics.CGImage CreateImage(System.UInt32 displayId)
        {
            System.IntPtr handle = System.IntPtr.Zero;

            try
            {
                handle = CGDisplayCreateImage(displayId);
                return new CoreGraphics.CGImage(handle);
            }
            finally
            {
                if (handle != System.IntPtr.Zero)
                {
                    CFRelease(handle);
                }
            }
        } // End Sub CreateImage 


    } // End Class OSxScreenshot 


} // End Namespace rtaStreamingServer 
