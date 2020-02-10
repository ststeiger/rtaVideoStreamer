
namespace rtaNetworking.Windows
{


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

        [System.Runtime.InteropServices.DllImport("user32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
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

        public static System.Drawing.Rectangle VirtualScreen
        {
            get
            {
                if (MultiMonitorSupport)
                {
                    return new System.Drawing.Rectangle(GetSystemMetrics(SM_XVIRTUALSCREEN),
                                         GetSystemMetrics(SM_YVIRTUALSCREEN),
                                         GetSystemMetrics(SM_CXVIRTUALSCREEN),
                                         GetSystemMetrics(SM_CYVIRTUALSCREEN));
                }
                else
                {
                    System.Drawing.Size size = PrimaryMonitorSize;
                    return new System.Drawing.Rectangle(0, 0, size.Width, size.Height);
                }
            }
        }

        public const int SPI_GETWORKAREA = 48;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [System.Runtime.Versioning.ResourceExposure(System.Runtime.Versioning.ResourceScope.None)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref RECT rc, int nUpdate);

        public static System.Drawing.Rectangle WorkingArea
        {
            get
            {
                RECT rc = new RECT();
                SystemParametersInfo(SPI_GETWORKAREA, 0, ref rc, 0);
                return System.Drawing.Rectangle.FromLTRB(rc.left, rc.top, rc.right, rc.bottom);
            }
        }

    }


}
