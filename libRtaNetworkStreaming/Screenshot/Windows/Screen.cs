
namespace rtaNetworking.Windows
{


    public class PictureBox
    {
        public System.Drawing.Image Image;
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


        public System.Drawing.Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }
        
        
        private class MonitorEnumCallback
        {
            public System.Collections.ArrayList screens = new System.Collections.ArrayList();

            public virtual bool Callback(System.IntPtr monitor, System.IntPtr hdc, System.IntPtr lprcMonitor, System.IntPtr lparam)
            {
                screens.Add(new Screen(monitor, hdc));
                return true;
            }
        }

        public delegate bool MonitorEnumProc(System.IntPtr monitor, System.IntPtr hdc, System.IntPtr lprcMonitor, System.IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern bool EnumDisplayMonitors(System.Runtime.InteropServices.HandleRef hdc, COMRECT rcClip, MonitorEnumProc lpfnEnum, System.IntPtr dwData);
        

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
                        EnumDisplayMonitors(new System.Runtime.InteropServices.HandleRef(null, System.IntPtr.Zero), null, proc, System.IntPtr.Zero);

                        if (closure.screens.Count > 0)
                        {
                            Screen[] temp = new Screen[closure.screens.Count];
                            closure.screens.CopyTo(temp, 0);
                            screens = temp;
                        }
                        else
                        {
                            screens = new Screen[] { new Screen((System.IntPtr)PRIMARY_MONITOR) };
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
                    return new Screen((System.IntPtr)PRIMARY_MONITOR, System.IntPtr.Zero);
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

            System.IntPtr screenDC = hdc;

            if (!multiMonitorSupport || monitor == (System.IntPtr)PRIMARY_MONITOR)
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
                GetMonitorInfo(new System.Runtime.InteropServices.HandleRef(null, monitor), info);

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

        public System.Drawing.Rectangle WorkingArea
        {
            get
            {

                //if the static Screen class has a different desktop change count 
                //than this instance then update the count and recalculate our working area
                if (currentDesktopChangedCount != Screen.DesktopChangedCount)
                {

                    System.Threading.Interlocked.Exchange(ref currentDesktopChangedCount, Screen.DesktopChangedCount);

                    if (!multiMonitorSupport || hmonitor == (System.IntPtr)PRIMARY_MONITOR)
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
                        GetMonitorInfo(new System.Runtime.InteropServices.HandleRef(null, hmonitor), info);
                        workingArea = System.Drawing.Rectangle.FromLTRB(info.rcWork.left, info.rcWork.top, info.rcWork.right, info.rcWork.bottom);
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




        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern bool GetMonitorInfo(System.Runtime.InteropServices.HandleRef hmonitor, [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out]MONITORINFOEX info);


        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern System.IntPtr MonitorFromPoint(POINTSTRUCT pt, int flags);
        
        public static Screen FromPoint(System.Drawing.Point point)
        {
            if (multiMonitorSupport)
            {
                POINTSTRUCT pt = new POINTSTRUCT(point.X, point.Y);
                return new Screen(MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST));
            }
            else
            {
                return new Screen((System.IntPtr)PRIMARY_MONITOR);
            }
        }

    }
}
