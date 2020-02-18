
using Gdk;
using Gtk;
using GLib;
using Pango;
using PangoSharp;
using GLibSharp;
using GtkSharp;
using libRtaNetworkStreaming;
using Window = Gdk.Window;


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


        public static void TestXGetImage()
        {
            // libRtaNetworkStreaming.XImage* foo = libRtaNetworkStreaming.XLib.XGetImage();



            // System.IntPtr pImage;
            // libRtaNetworkStreaming.XImage block1 = (libRtaNetworkStreaming.XImage)System.Runtime.InteropServices.Marshal.PtrToStructure(pImage, typeof(libRtaNetworkStreaming.XImage));
        } // End Sub TestXGetImage

        public class MyImage
        {
            public System.Drawing.Imaging.PixelFormat PixelFormat;
            public int Width;
            public int Height;

            private int Stride;

            public byte[] BmpBuffer
            {
                get
                {
                    return PointerToManagedArray(System.IntPtr.Zero, Height * Stride);
                }
            }

            // https://stackoverflow.com/questions/5486938/c-sharp-how-to-get-byte-from-intptr
            private static byte[] PointerToManagedArray(System.IntPtr pnt, int size)
            {
                byte[] managedArray = new byte[size];

                System.Runtime.InteropServices.Marshal.Copy(pnt, managedArray, 0, size);
                return managedArray;
            }

        }
        
        
        
        
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

        // https://stackoverflow.com/questions/11452246/add-a-bitmap-header-to-a-byte-array-then-create-a-bitmap-file
        public static byte[] ImageBitmap(MyImage image)
        {

            byte[] bmpBuffer = image.BmpBuffer;
            int bmpBufferSize = bmpBuffer.Length;


            // BmpBufferSize : a pure length of raw bitmap data without the header.
            // the 54 value here is the length of bitmap header.
            byte[] bitmapBytes = new byte[bmpBufferSize + 54];

            #region Bitmap Header
            // 0~2 "BM"
            bitmapBytes[0] = 0x42;
            bitmapBytes[1] = 0x4d;

            // 2~6 Size of the BMP file - Bit cound + Header 54
            System.Array.Copy(System.BitConverter.GetBytes(bmpBufferSize + 54), 0, bitmapBytes, 2, 4);

            // 6~8 Application Specific : normally, set zero
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 6, 2);

            // 8~10 Application Specific : normally, set zero
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 8, 2);

            // 10~14 Offset where the pixel array can be found - 24bit bitmap data always starts at 54 offset.
            System.Array.Copy(System.BitConverter.GetBytes(54), 0, bitmapBytes, 10, 4);
            #endregion

            #region DIB Header
            // 14~18 Number of bytes in the DIB header. 40 bytes constant.
            System.Array.Copy(System.BitConverter.GetBytes(40), 0, bitmapBytes, 14, 4);

            // 18~22 Width of the bitmap.
            System.Array.Copy(System.BitConverter.GetBytes(image.Width), 0, bitmapBytes, 18, 4);

            // 22~26 Height of the bitmap.
            System.Array.Copy(System.BitConverter.GetBytes(image.Height), 0, bitmapBytes, 22, 4);

            // 26~28 Number of color planes being used
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 26, 2);

            // 28~30 Number of bits. If you don't know the pixel format, trying to calculate it with the quality of the video/image source.
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                System.Array.Copy(System.BitConverter.GetBytes(24), 0, bitmapBytes, 28, 2);
            }

            // 30~34 BI_RGB no pixel array compression used : most of the time, just set zero if it is raw data.
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 30, 4);

            // 34~38 Size of the raw bitmap data ( including padding )
            System.Array.Copy(System.BitConverter.GetBytes(bmpBufferSize), 0, bitmapBytes, 34, 4);

            // 38~46 Print resolution of the image, 72 DPI x 39.3701 inches per meter yields
            if (image.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 38, 4);
                System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 42, 4);
            }

            // 46~50 Number of colors in the palette
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 46, 4);

            // 50~54 means all colors are important
            System.Array.Copy(System.BitConverter.GetBytes(0), 0, bitmapBytes, 50, 4);
            #endregion DIB Header

            // 54~end : Pixel Data : Finally, time to combine your raw data, BmpBuffer in this code, with a bitmap header you've just created.
            System.Array.Copy(bmpBuffer as System.Array, 0, bitmapBytes, 54, bmpBufferSize);

            return bitmapBytes;
        } // End Function ImageBitmap 

        // https://www.manpagez.com/man/3/X11::Protocol::Ext::XFIXES/
        // https://github.com/D-Programming-Deimos/libX11/blob/master/c/X11/extensions/Xfixes.h

        // https://www.programiz.com/c-programming/examples/sizeof-operator-example
        /*
         #include<stdio.h>
int main() {
    int intType;
    float floatType;
    double doubleType;
    char charType;
    // sizeof evaluates the size of a variable
    printf("Size of int: %ld bytes\n", sizeof(intType));
    printf("Size of float: %ld bytes\n", sizeof(floatType));
    printf("Size of double: %ld bytes\n", sizeof(doubleType));
    printf("Size of char: %ld byte\n", sizeof(charType));
    
    return 0;
}
             */


        // https://www.displayfusion.com/Discussions/View/converting-c-data-types-to-c/?ID=38db6001-45e5-41a3-ab39-8004450204b3
        

        // https://www.geeksforgeeks.org/reverse-an-array-in-groups-of-given-size/
        // Given an array, reverse every sub-array formed by consecutive k elements.
        // Function to reverse every sub-array formed by consecutive k elements 
        public static void ReverseArrayInGroupsOfK(int[] arr, int k)
        {
            int n = arr.Length;

            for (int i = 0; i < n; i += k)
            {
                int left = i;

                // to handle case when k is  not multiple of n 
                int right = System.Math.Min(i + k - 1, n - 1);
                int temp;

                // reverse the sub-array [left, right] 
                while (left < right)
                {
                    temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    left += 1;
                    right -= 1;
                } // Whend 

            } // Next i 

        } // End Sub ReverseArrayInGroupsOfK 

        /*


        #include <iostream> 
        using namespace std; 
  
        // Function to reverse every sub-array formed by 
        // consecutive k elements 
        void reverse(int arr[], int n, int k) 
        { 
            for (int i = 0; i < n; i += k) 
            { 
                int left = i; 
  
                // to handle case when k is not multiple of n 
                int right = min(i + k - 1, n - 1); 
  
                // reverse the sub-array [left, right] 
                while (left < right) 
                    swap(arr[left++], arr[right--]); 
  
            } 
        } 
  
        // Driver code 
        int main() 
        { 
            int arr[] = {1, 2, 3, 4, 5, 6, 7, 8}; 
            int k = 3; 
  
            int n = sizeof(arr) / sizeof(arr[0]); 
  
            reverse(arr, n, k); 
  
            for (int i = 0; i < n; i++) 
                cout << arr[i] << " "; 
  
            return 0; 
        } 
        */


        // https://stackoverflow.com/questions/6912601/write-ximage-to-bmp-file-in-c
        // https://stackoverflow.com/questions/14092290/creating-bitmap-object-from-raw-bytes
        // https://github.com/ststeiger/cef-pdf/blob/master/src/Bmp.h
        // https://github.com/ststeiger/cef-pdf/blob/master/src/Bmp.cpp
        // https://codereview.stackexchange.com/questions/196084/read-and-write-bmp-file-in-c/215955#215955
        static byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];

            for (int i = 0; i < rows; i++)
                System.Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);

            return newBytes;
        }


        // https://stackoverflow.com/questions/34176795/any-efficient-way-of-converting-ximage-data-to-pixel-map-e-g-array-of-rgb-quad/38298349
        public static void GetStride(System.IntPtr pixels, int format, int width, int height)
        {
            // The stride is the width of a single row of pixels(a scan line),
            // rounded up to a four - byte boundary. 
            // If the stride is positive, the bitmap is top-down. 
            // If the stride is negative, the bitmap is bottom-up.
            // https://stackoverflow.com/questions/2185944/why-must-stride-in-the-system-drawing-bitmap-constructor-be-a-multiple-of-4

            int bitsPerPixel = ((int)format & 0xff00) >> 8;
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * ((width * bytesPerPixel + 3) / 4);
            
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, pixels);
        }


        // https://stackoverflow.com/questions/14092290/creating-bitmap-object-from-raw-bytes
        public static void foo(int format, int imageWidth, int imageHeight, byte[] imageData)
        {
            int columns = imageWidth;
            int rows = imageHeight;
            // int stride = columns; 
            byte[] newbytes = PadLines(imageData, rows, columns);

            int bitsPerPixel = ((int)format & 0xff00) >> 8;
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * ((imageWidth * bytesPerPixel + 3) / 4);
            // which is actually: int stride = 4 * Floor((imageWidth * bytesPerPixel + 3) / 4);
            
            
            System.Drawing.Bitmap im = new System.Drawing.Bitmap(columns, rows, stride,
                     System.Drawing.Imaging.PixelFormat.Format8bppIndexed,
                     System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(newbytes, 0)
            );

            im.Save(@"C:\Users\musa\Documents\Hobby\image21.bmp");
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
