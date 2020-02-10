
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
                // https://stackoverflow.com/questions/17334786/get-pixel-from-the-screen-screenshot-in-max-osx/17343305#17343305

                // Get byte-array from CGImage
                // https://gist.github.com/zhangao0086/5fafb1e1c0b5d629eb76

                AppKit.NSBitmapImageRep bitmapRep = new AppKit.NSBitmapImageRep(cgImage);

                // var imageData = bitmapRep.representationUsingType(NSBitmapImageFileType.NSPNGFileType, properties: [:])
                Foundation.NSData imageData = bitmapRep.RepresentationUsingTypeProperties(AppKit.NSBitmapImageFileType.Png);

                long len = imageData.Length;
                byte[] bytes = new byte[len];
                System.Runtime.InteropServices.GCHandle pinnedArray = System.Runtime.InteropServices.GCHandle.Alloc(bytes, System.Runtime.InteropServices.GCHandleType.Pinned);
                System.IntPtr pointer = pinnedArray.AddrOfPinnedObject();
                // Do your stuff...
                imageData.GetBytes(pointer, new System.IntPtr(len));
                pinnedArray.Free();

                using (AppKit.NSImage nsImage = new AppKit.NSImage(cgImage, new System.Drawing.SizeF(cgImage.Width, cgImage.Height)))
                {
                    // ImageView.Image = nsImage;
                    // And now ? How to get the image bytes ? 

                    // https://theconfuzedsourcecode.wordpress.com/2016/02/24/convert-android-bitmap-image-and-ios-uiimage-to-byte-array-in-xamarin/
                    // https://stackoverflow.com/questions/5645157/nsimage-from-byte-array
                    // https://stackoverflow.com/questions/53060723/nsimage-source-from-byte-array-cocoa-app-xamarin-c-sharp
                    // https://gist.github.com/zhangao0086/5fafb1e1c0b5d629eb76
                    // https://www.quora.com/What-is-a-way-to-convert-UIImage-to-a-byte-array-in-Swift?share=1
                    // https://stackoverflow.com/questions/17112314/converting-uiimage-to-byte-array

                } // End Using nsImage 

            } // End Using cgImage 

        } // End Sub TestCapture 


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
