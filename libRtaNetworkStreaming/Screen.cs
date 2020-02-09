
namespace System.Windows.Forms
{



    public class PictureBox
    {
        public System.Drawing.Image Image;
    }


    // use this in cases where the Native API takes a POINT not a POINT*
    // classes marshal by ref.
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct POINTSTRUCT
    {
        public int x;
        public int y;
        public POINTSTRUCT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }



    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r)
        {
            this.left = r.Left;
            this.top = r.Top;
            this.right = r.Right;
            this.bottom = r.Bottom;
        }

        public static RECT FromXYWH(int x, int y, int width, int height)
        {
            return new RECT(x, y, x + width, y + height);
        }

        public System.Drawing.Size Size
        {
            get
            {
                return new System.Drawing.Size(this.right - this.left, this.bottom - this.top);
            }
        }
    }


    [Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)]
    public class COMRECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public COMRECT()
        {
        }

        public COMRECT(System.Drawing.Rectangle r)
        {
            this.left = r.X;
            this.top = r.Y;
            this.right = r.Right;
            this.bottom = r.Bottom;
        }


        public COMRECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        /* Unused
        public RECT ToRECT() {
            return new RECT(left, top, right, bottom);
        }
        */

        public static COMRECT FromXYWH(int x, int y, int width, int height)
        {
            return new COMRECT(x, y, x + width, y + height);
        }

        public override string ToString()
        {
            return "Left = " + left + " Top " + top + " Right = " + right + " Bottom = " + bottom;
        }
    }



    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Auto, Pack = 4)]
    public class MONITORINFOEX
    {
        internal int cbSize = Runtime.InteropServices.Marshal.SizeOf(typeof(MONITORINFOEX));
        internal RECT rcMonitor = new RECT();
        internal RECT rcWork = new RECT();
        internal int dwFlags = 0;
        [Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
        internal char[] szDevice = new char[32];
    }

    public class SystemInformation
    {

        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        public const int SM_CMONITORS = 80;


        public const int SM_XVIRTUALSCREEN = 76;
        public const int SM_YVIRTUALSCREEN = 77;
        public const int SM_CXVIRTUALSCREEN = 78;
        public const int SM_CYVIRTUALSCREEN = 79;


        private static bool checkMultiMonitorSupport = false;
        private static bool multiMonitorSupport = false;

        [Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = Runtime.InteropServices.CharSet.Auto)]
        [Runtime.Versioning.ResourceExposure(Runtime.Versioning.ResourceScope.None)]
        public static extern int GetSystemMetrics(int nIndex);


        private static bool MultiMonitorSupport
        {
            get
            {
                if (!checkMultiMonitorSupport)
                {
                    multiMonitorSupport = (GetSystemMetrics(SM_CMONITORS) != 0);
                    checkMultiMonitorSupport = true;
                }
                return multiMonitorSupport;
            }
        }


        public static System.Drawing.Size PrimaryMonitorSize
        {
            get
            {
                return new System.Drawing.Size(GetSystemMetrics(SM_CXSCREEN),
                                GetSystemMetrics(SM_CYSCREEN));
            }
        }

        public static Drawing.Rectangle VirtualScreen
        {
            get
            {
                if (MultiMonitorSupport)
                {
                    return new Drawing.Rectangle(GetSystemMetrics(SM_XVIRTUALSCREEN),
                                         GetSystemMetrics(SM_YVIRTUALSCREEN),
                                         GetSystemMetrics(SM_CXVIRTUALSCREEN),
                                         GetSystemMetrics(SM_CYVIRTUALSCREEN));
                }
                else
                {
                    Drawing.Size size = PrimaryMonitorSize;
                    return new Drawing.Rectangle(0, 0, size.Width, size.Height);
                }
            }
        }

        public const int SPI_GETWORKAREA = 48;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref RECT rc, int nUpdate);

        public static Drawing.Rectangle WorkingArea
        {
            get
            {
                RECT rc = new RECT();
                SystemParametersInfo(SPI_GETWORKAREA, 0, ref rc, 0);
                return Drawing.Rectangle.FromLTRB(rc.left, rc.top, rc.right, rc.bottom);
            }
        }

    }


    // https://referencesource.microsoft.com/#system.windows.forms/winforms/managed/system/winforms/Screen.cs,61c7e4f4309b6603,references
    public class Screen
    {
        private const int SM_CMONITORS = 80;

        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern int GetSystemMetrics(int nIndex);

        private static bool multiMonitorSupport = (GetSystemMetrics(SM_CMONITORS) != 0);
        private static Screen[] screens;

        private const int PRIMARY_MONITOR = unchecked((int)0xBAADF00D);

        private const int MONITOR_DEFAULTTONULL = 0x00000000;
        private const int MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        private const int MONITOR_DEFAULTTONEAREST = 0x00000002;
        private const int MONITORINFOF_PRIMARY = 0x00000001;


        readonly System.IntPtr hmonitor;
        readonly System.Drawing.Rectangle bounds;

        readonly bool primary;
        readonly string deviceName;
        private System.Drawing.Rectangle workingArea = System.Drawing.Rectangle.Empty;


        public Drawing.Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        private class MonitorEnumCallback
        {
            public Collections.ArrayList screens = new Collections.ArrayList();

            public virtual bool Callback(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lparam)
            {
                screens.Add(new Screen(monitor, hdc));
                return true;
            }
        }

        public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern bool EnumDisplayMonitors(Runtime.InteropServices.HandleRef hdc, COMRECT rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);
        

        public static Screen[] AllScreens
        {
            get
            {
                if (screens == null)
                {
                    if (multiMonitorSupport)
                    {
                        MonitorEnumCallback closure = new MonitorEnumCallback();
                        MonitorEnumProc proc = new MonitorEnumProc(closure.Callback);
                        EnumDisplayMonitors(new Runtime.InteropServices.HandleRef(null, IntPtr.Zero), null, proc, IntPtr.Zero);

                        if (closure.screens.Count > 0)
                        {
                            Screen[] temp = new Screen[closure.screens.Count];
                            closure.screens.CopyTo(temp, 0);
                            screens = temp;
                        }
                        else
                        {
                            screens = new Screen[] { new Screen((IntPtr)PRIMARY_MONITOR) };
                        }
                    }
                    else
                    {
                        screens = new Screen[] { PrimaryScreen };
                    }

                    // Now that we have our screens, attach a display setting changed
                    // event so that we know when to invalidate them.
                    //
                    // SystemEvents.DisplaySettingsChanging += new EventHandler(OnDisplaySettingsChanging);
                }

                return screens;
            }
        }


        public static Screen PrimaryScreen
        {
            get
            {
                if (multiMonitorSupport)
                {
                    Screen[] screens = AllScreens;
                    for (int i = 0; i < screens.Length; i++)
                    {
                        if (screens[i].primary)
                        {
                            return screens[i];
                        }
                    }
                    return null;
                }
                else
                {
                    return new Screen((IntPtr)PRIMARY_MONITOR, IntPtr.Zero);
                }
            }
        }


        private static object syncLock = new object();
        private static int desktopChangedCount = -1;//static counter of desktop size changes
        private int currentDesktopChangedCount = -1;//instance-based counter used to invalidate WorkingArea



        internal Screen(System.IntPtr monitor) : this(monitor, System.IntPtr.Zero)
        {
        }

        internal Screen(System.IntPtr monitor, System.IntPtr hdc)
        {

            IntPtr screenDC = hdc;

            if (!multiMonitorSupport || monitor == (IntPtr)PRIMARY_MONITOR)
            {
                // Single monitor system
                //
                bounds = SystemInformation.VirtualScreen;
                primary = true;
                deviceName = "DISPLAY";
            }
            else
            {
                // MultiMonitor System
                // We call the 'A' version of GetMonitorInfoA() because
                // the 'W' version just never fills out the struct properly on Win2K.
                //
                MONITORINFOEX info = new MONITORINFOEX();
                GetMonitorInfo(new Runtime.InteropServices.HandleRef(null, monitor), info);

                bounds = System.Drawing.Rectangle.FromLTRB(info.rcMonitor.left, info.rcMonitor.top, info.rcMonitor.right, info.rcMonitor.bottom);
                
                primary = ((info.dwFlags & MONITORINFOF_PRIMARY) != 0);

                deviceName = new string(info.szDevice);
                deviceName = deviceName.TrimEnd((char)0);

                // if (hdc == IntPtr.Zero)
                // {
                //     screenDC = UnsafeNativeMethods.CreateDC(deviceName);
                // }
            }
            hmonitor = monitor;

            // this.bitDepth = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, screenDC), NativeMethods.BITSPIXEL);
            // this.bitDepth *= UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, screenDC), NativeMethods.PLANES);

            // if (hdc != screenDC)
            // {
            // UnsafeNativeMethods.DeleteDC(new HandleRef(null, screenDC));
            // }
        }


        private static int DesktopChangedCount
        {
            get
            {
                if (desktopChangedCount == -1)
                {

                    lock (syncLock)
                    {

                        //now that we have a lock, verify (again) our changecount...
                        if (desktopChangedCount == -1)
                        {
                            //sync the UserPreference.Desktop change event.  We'll keep count 
                            //of desktop changes so that the WorkingArea property on Screen 
                            //instances know when to invalidate their cache.
                            // SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(OnUserPreferenceChanged);

                            desktopChangedCount = 0;
                        }
                    }
                }
                return desktopChangedCount;
            }
        }

        public Drawing.Rectangle WorkingArea
        {
            get
            {

                //if the static Screen class has a different desktop change count 
                //than this instance then update the count and recalculate our working area
                if (currentDesktopChangedCount != Screen.DesktopChangedCount)
                {

                    Threading.Interlocked.Exchange(ref currentDesktopChangedCount, Screen.DesktopChangedCount);

                    if (!multiMonitorSupport || hmonitor == (IntPtr)PRIMARY_MONITOR)
                    {
                        // Single monitor system
                        //
                        workingArea = SystemInformation.WorkingArea;
                    }
                    else
                    {
                        // MultiMonitor System
                        // We call the 'A' version of GetMonitorInfoA() because
                        // the 'W' version just never fills out the struct properly on Win2K.
                        //
                        MONITORINFOEX info = new MONITORINFOEX();
                        GetMonitorInfo(new Runtime.InteropServices.HandleRef(null, hmonitor), info);
                        workingArea = Drawing.Rectangle.FromLTRB(info.rcWork.left, info.rcWork.top, info.rcWork.right, info.rcWork.bottom);
                    }
                }

                return workingArea;
            }
        }


        public static System.Drawing.Rectangle GetBounds(System.Drawing.Point pt)
        {
            // Wouldn't be necessary if GetBounds on mono wasn't buggy.
            // if (Environment.OSVersion.Platform == PlatformID.Unix) return GetXorgScreen();

            return Screen.FromPoint(pt).Bounds;
        } // End Function GetScrBounds




        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern bool GetMonitorInfo(Runtime.InteropServices.HandleRef hmonitor, [Runtime.InteropServices.In, Runtime.InteropServices.Out]MONITORINFOEX info);


        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern IntPtr MonitorFromPoint(POINTSTRUCT pt, int flags);
        
        public static Screen FromPoint(System.Drawing.Point point)
        {
            if (multiMonitorSupport)
            {
                POINTSTRUCT pt = new POINTSTRUCT(point.X, point.Y);
                return new Screen(MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST));
            }
            else
            {
                return new Screen((IntPtr)PRIMARY_MONITOR);
            }
        }

    }
}
