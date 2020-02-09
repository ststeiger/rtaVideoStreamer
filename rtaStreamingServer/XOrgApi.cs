
// http://www.koders.com/csharp/fid5A7CBAABE4E399E1BED8C2C2FB6E1B36C289628D.aspx?s=zoom#L293
namespace Xorg
{


    // http://www.koders.com/csharp/fidFC528FE04222FE631D31990CC4B30889DB6ACCA8.aspx?s=socket
    public class API
    {


        [System.Flags]
        internal enum EventMask
        {
            NoEventMask = 0,
            KeyPressMask = 1 << 0,
            KeyReleaseMask = 1 << 1,
            ButtonPressMask = 1 << 2,
            ButtonReleaseMask = 1 << 3,
            EnterWindowMask = 1 << 4,
            LeaveWindowMask = 1 << 5,
            PointerMotionMask = 1 << 6,
            PointerMotionHintMask = 1 << 7,
            Button1MotionMask = 1 << 8,
            Button2MotionMask = 1 << 9,
            Button3MotionMask = 1 << 10,
            Button4MotionMask = 1 << 11,
            Button5MotionMask = 1 << 12,
            ButtonMotionMask = 1 << 13,
            KeymapStateMask = 1 << 14,
            ExposureMask = 1 << 15,
            VisibilityChangeMask = 1 << 16,
            StructureNotifyMask = 1 << 17,
            ResizeRedirectMask = 1 << 18,
            SubstructureNotifyMask = 1 << 19,
            SubstructureRedirectMask = 1 << 20,
            FocusChangeMask = 1 << 21,
            PropertyChangeMask = 1 << 22,
            ColormapChangeMask = 1 << 23,
            OwnerGrabButtonMask = 1 << 24
        }


        protected const string m_strSharedObjectName = "libX11";
        protected const string m_strSharedObjectName_Video = "libXxf86vm";

        // For AIX shared object, use "dump -Tv /path/to/foo.o"
        // For an ELF shared library, use "readelf -s /path/to/libfoo.so"
        // or (if you have GNU nm) "nm -D /path/to/libfoo.so"
        // For a Windows DLL, use "dumpbin /EXPORTS foo.dll".


        // nm -D $(locate libX11 | sed '/\/usr\/lib/!d;' | grep ".so$")
        // nm -D $(locate "libX11.so" | grep ".so$")
        // nm -D $(locate "libX11.so" | grep ".so$") | grep "DisplayHeight"


        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XOpenDisplay")]
        internal extern static System.IntPtr XOpenDisplay(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XDefaultScreen")]
        internal extern static int XDefaultScreen(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XDisplayHeight")]
        internal extern static int DisplayHeight(System.IntPtr display, int screen_number);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XDisplayWidth")]
        internal extern static int DisplayWidth(System.IntPtr display, int screen_number);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XRootWindow")]
        internal extern static System.IntPtr XRootWindow(System.IntPtr display, int screen_number);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XCloseDisplay")]
        internal extern static int XCloseDisplay(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XSynchronize")]
        internal extern static System.IntPtr XSynchronize(System.IntPtr display, bool onoff);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XGrabServer")]
        internal extern static void XGrabServer(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XUngrabServer")]
        internal extern static void XUngrabServer(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName)]
        internal extern static int XFlush(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XFree")]
        internal extern static int XFree(System.IntPtr data);

        //[System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XSendEvent")]
        //internal extern static int XSendEvent(System.IntPtr display, System.IntPtr window, bool propagate, System.IntPtr event_mask, ref XEvent send_event);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XSendEvent")]
        internal extern static int XSendEvent(System.IntPtr display, System.IntPtr window, bool propagate, int event_mask, ref Xorg.Structs.XEvent send_event);

        //[System.Runtime.InteropServices.DllImport (m_strSharedObjectName, EntryPoint="XSendEvent")]
        //internal extern static int XSendEvent(System.IntPtr display, System.IntPtr window, bool propagate, EventMask event_mask, ref XEvent send_event);
        //internal extern static int XSendEvent(System.IntPtr display, System.IntPtr window, bool propagate, EventMask event_mask, ref XClientMessageEvent send_event);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XQueryPointer")]
        internal extern static bool XQueryPointer(System.IntPtr display, System.IntPtr window, out System.IntPtr root, out System.IntPtr child, out int root_x, out int root_y, out int win_x, out int win_y, out int keys_buttons);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XWarpPointer")]
        internal extern static uint XWarpPointer(System.IntPtr display, System.IntPtr src_w, System.IntPtr dest_w, int src_x, int src_y, uint src_width, uint src_height, int dest_x, int dest_y);

        [System.Runtime.InteropServices.DllImport(m_strSharedObjectName, EntryPoint = "XGetWindowProperty")]
        internal extern static int XGetWindowProperty(System.IntPtr display, System.IntPtr window, System.IntPtr atom, System.IntPtr long_offset, System.IntPtr long_length, bool delete, System.IntPtr req_type, out System.IntPtr actual_type, out int actual_format, out System.IntPtr nitems, out System.IntPtr bytes_after, ref System.IntPtr prop);


    } // End Class Mouse


} // End Namespace Xorg
