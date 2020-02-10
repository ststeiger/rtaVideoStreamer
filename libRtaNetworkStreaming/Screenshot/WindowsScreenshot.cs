

// TODO: Solution for OSX: 
// https://github.com/ScreenshotMonitor/ScreenshotCapture/blob/master/src/Pranas.ScreenshotCapture/ScreenshotCapture.cs
namespace rtaStreamingServer
{

    public class CaptureSingleMonitor 
    {

        public static System.Drawing.Bitmap CaptureMonitor(rtaNetworking.Windows.Screen monitor, bool workingAreaOnly)
        {
            System.Drawing.Rectangle region;

            region = workingAreaOnly ? monitor.WorkingArea : monitor.Bounds;

            return WindowsScreenshot.CaptureRegion(region);
        }

        public static System.Drawing.Bitmap CaptureMonitor(rtaNetworking.Windows.Screen monitor)
        {
            return CaptureMonitor(monitor, false);
        }

        public static System.Drawing.Bitmap CaptureMonitor(int index)
        {
            return CaptureMonitor(index, false);
        }

        public static System.Drawing.Bitmap CaptureMonitor(int index, bool workingAreaOnly)
        {
            return CaptureMonitor(rtaNetworking.Windows.Screen.AllScreens[index], workingAreaOnly);
        }
    }


    public class CaptureEntireDesktop
    {
        public static System.Drawing.Bitmap CaptureDesktop()
        {
            return CaptureDesktop(false);
        }

        public static System.Drawing.Bitmap CaptureDesktop(bool workingAreaOnly)
        {
            System.Drawing.Rectangle desktop;
            rtaNetworking.Windows.Screen[] screens;

            desktop = System.Drawing.Rectangle.Empty;
            screens = rtaNetworking.Windows.Screen.AllScreens;

            for (int i = 0; i < screens.Length; i++)
            {
                rtaNetworking.Windows.Screen screen;
                screen = screens[i];
                desktop = System.Drawing.Rectangle.Union(desktop, workingAreaOnly ? screen.WorkingArea : screen.Bounds);
            }

            return WindowsScreenshot.CaptureRegion(desktop);
        }
    }


    public class CaptureSingleWindow
    {
        
        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.Process)]
        internal static extern System.IntPtr GetForegroundWindow();


        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        internal static extern bool GetWindowRect(System.IntPtr hwnd, out RECT lpRect);


        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        internal static extern bool GetWindowRect(System.Runtime.InteropServices.HandleRef hWnd, 
            [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref RECT rect);



        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        public static System.Drawing.Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }


        public static System.Drawing.Bitmap CaptureWindow(System.IntPtr hWnd)
        {
            RECT region;

            GetWindowRect(hWnd, out region);

            return WindowsScreenshot.CaptureRegion(System.Drawing.Rectangle.FromLTRB(region.Left, region.Top, region.Right, region.Bottom));
        }


    }




    // https://www.cyotek.com/blog/capturing-screenshots-using-csharp-and-p-invoke
    class WindowsScreenshot
    {

        const int SRCCOPY = 0x00CC0020;
        const int CAPTUREBLT = 0x40000000;



        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern bool BitBlt(System.IntPtr hdcDest, int nxDest, int nyDest, int nWidth, int nHeight, System.IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        
        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "CreateCompatibleBitmap", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern System.IntPtr CreateCompatibleBitmap(System.IntPtr hdc, int width, int nHeight);

        

        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "CreateCompatibleDC", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.Process)]
        static extern System.IntPtr CreateCompatibleDC(System.IntPtr hdc);



        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, EntryPoint = "DeleteDC", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        static extern System.IntPtr DeleteDC(System.IntPtr hdc);


        

        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.Process)]
        static extern System.IntPtr GetDesktopWindow();

        
        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, EntryPoint = "GetWindowDC", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.Process)]
        static extern System.IntPtr GetWindowDC(System.IntPtr hWnd);


        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, EntryPoint = "ReleaseDC", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        static extern bool ReleaseDC(System.IntPtr hWnd, System.IntPtr hDc);

        
        [System.Runtime.InteropServices.DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern System.IntPtr SelectObject(System.IntPtr hdc, System.IntPtr hObject);


        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        static extern System.IntPtr DeleteObject(System.IntPtr hObject);


        public static System.Drawing.Bitmap CaptureRegion(System.Drawing.Rectangle region)
        {
            System.IntPtr desktophWnd;
            System.IntPtr desktopDc;
            System.IntPtr memoryDc;
            System.IntPtr bitmap;
            System.IntPtr oldBitmap;
            bool success;
            System.Drawing.Bitmap result;

            desktophWnd = GetDesktopWindow();
            desktopDc = GetWindowDC(desktophWnd);
            memoryDc = CreateCompatibleDC(desktopDc);
            bitmap = CreateCompatibleBitmap(desktopDc, region.Width, region.Height);
            oldBitmap = SelectObject(memoryDc, bitmap);

            success = BitBlt(memoryDc, 0, 0, region.Width, region.Height, desktopDc, region.Left, region.Top, SRCCOPY | CAPTUREBLT);

            try
            {
                if (!success)
                {
                    throw new System.ComponentModel.Win32Exception();
                }

                result = System.Drawing.Image.FromHbitmap(bitmap);
            }
            finally
            {
                SelectObject(memoryDc, oldBitmap);
                DeleteObject(bitmap);
                DeleteDC(memoryDc);
                ReleaseDC(desktophWnd, desktopDc);
            }

            return result;
        }


    }


}
