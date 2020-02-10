
namespace rtaNetworking.Windows
{


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


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
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
        internal int cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(MONITORINFOEX));
        internal RECT rcMonitor = new RECT();
        internal RECT rcWork = new RECT();
        internal int dwFlags = 0;
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32)]
        internal char[] szDevice = new char[32];
    }

}
