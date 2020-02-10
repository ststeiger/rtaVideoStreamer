
// https://stackoverflow.com/questions/1537587/capture-screen-image-in-c-on-osx
// https://github.com/Acollie/C-Screenshot-OSX/blob/master/C%2B%2B-screenshot/C%2B%2B-screenshot/main.cpp
// https://github.com/ScreenshotMonitor/ScreenshotCapture/blob/master/src/Pranas.ScreenshotCapture/ScreenshotCapture.cs
// https://screenshotmonitor.com/blog/capturing-screenshots-in-net-and-mono/
namespace rtaStreamingServer
{

    // https://github.com/xamarin/xamarin-macios

    // using CoreGraphics;
    // using AppKit;
    // using Foundation;


    // https://www.nuget.org/packages/CocoaSharp.CoreGraphics/

    // https://github.com/xamarin/xamarin-macios/blob/master/src/CoreGraphics/CGImage.cs
    public class CGImage // CoreGraphics
        : System.IDisposable
    {

        public CGImage(System.IntPtr hDisplayId)
        {

        }

        public float Width;
        public float Height;


        void System.IDisposable.Dispose()
        {
            throw new System.NotImplementedException();
        }
    }

    // https://github.com/xamarin/xamarin-macios/blob/master/src/AppKit/NSImage.cs
    public class NSImage // AppKit 
        : System.IDisposable
    {
        public NSImage(CGImage image, System.Drawing.SizeF size)
        { }

        void System.IDisposable.Dispose()
        {
            throw new System.NotImplementedException();
        }
    }

    // https://github.com/smartmobili/CocoaBuilder/blob/master/XibParser/Foundation/NSNumber.cs
    // https://github.com/xamarin/xamarin-macios/blob/master/src/Foundation/NSNumber.mac.cs
    public class NSNumber // Foundation
    {
        public System.UInt32 UInt32Value;
    }

    // https://github.com/xamarin/xamarin-macios/blob/master/src/AppKit/NSScreen.cs
    public class NSScreen // AppKit 
    {

        public System.Collections.Generic.Dictionary<string, object> DeviceDescription;

        public static NSScreen MainScreen;
    }


    // https://qiita.com/shimshimkaz/items/18bcf4767143ea5897c7
    public static class ScreenCapture
    {
        private const string DllName = "/System/Library/Frameworks/CoreGraphics.framework/CoreGraphics";

        [System.Runtime.InteropServices.DllImport(DllName)]
        extern static System.IntPtr CGDisplayCreateImage(System.UInt32 displayId);

        [System.Runtime.InteropServices.DllImport(DllName)]
        extern static void CFRelease(System.IntPtr handle);


        public static void TestCapture()
        {
            var mainScreen = (NSNumber)NSScreen.MainScreen.DeviceDescription["NSScreenNumber"];

            using (var cgImage = ScreenCapture.CreateImage(mainScreen.UInt32Value))
            {
                using (var nsImage = new NSImage(cgImage, new System.Drawing.SizeF(cgImage.Width, cgImage.Height)))
                {
                    // ImageView.Image = nsImage;
                }
            }
        }


        public static CGImage CreateImage(System.UInt32 displayId)
        {
            System.IntPtr handle = System.IntPtr.Zero;

            try
            {
                handle = CGDisplayCreateImage(displayId);
                return new CGImage(handle);
            }
            finally
            {
                if (handle != System.IntPtr.Zero)
                {
                    CFRelease(handle);
                }
            }
        }


    }


}
