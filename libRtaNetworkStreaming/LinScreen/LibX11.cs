namespace rtaNetworking.Linux
{
    using System;
    using System.Runtime.InteropServices;
    
    
    // https://linux.die.net/man/3/xvisualinfo
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XVisualInfo
    {
        internal IntPtr visual;
        internal UIntPtr visualid; // typedef unsigned long VisualID;; // https://code.woboq.org/qt5/include/X11/X.h.html
        internal int screen;
        internal int depth;
        internal int klass;
        internal UIntPtr red_mask; // unsigned long
        internal UIntPtr green_mask; // unsigned long
        internal UIntPtr blue_mask; // unsigned long
        internal int colormap_size;
        internal int bits_per_rgb;
    }
    
    
    internal static class LibX11Functions
    {
        // Some special X11 stuff
        [DllImport("libX11", EntryPoint = "XOpenDisplay")]
        internal extern static IntPtr XOpenDisplay(IntPtr display);

        [DllImport("libX11", EntryPoint = "XCloseDisplay")]
        internal extern static int XCloseDisplay(IntPtr display);

        [DllImport("libX11", EntryPoint = "XRootWindow")]
        internal extern static IntPtr XRootWindow(IntPtr display, int screen);

        [DllImport("libX11", EntryPoint = "XDefaultScreen")]
        internal extern static int XDefaultScreen(IntPtr display);

        [DllImport("libX11", EntryPoint = "XDefaultDepth")]
        internal extern static uint XDefaultDepth(IntPtr display, int screen);

        [System.Runtime.InteropServices.DllImport("libX11", EntryPoint = "XDisplayHeight")]
        internal extern static int DisplayHeight(System.IntPtr display, int screen_number);

        [System.Runtime.InteropServices.DllImport("libX11", EntryPoint = "XDisplayWidth")]
        internal extern static int DisplayWidth(System.IntPtr display, int screen_number);


        // https://tronche.com/gui/x/xlib/graphics/XGetImage.html
        [DllImport("libX11", EntryPoint = "XGetImage")]
        internal extern static IntPtr XGetImage(IntPtr display, IntPtr drawable, int src_x, int src_y, uint width, uint height, System.UIntPtr pane, int format);

        [DllImport("libX11", EntryPoint = "XGetPixel")]
        internal extern static int XGetPixel(IntPtr image, int x, int y);

        [DllImport("libX11", EntryPoint = "XDestroyImage")]
        internal extern static int XDestroyImage(IntPtr image);

        [DllImport("libX11", EntryPoint = "XDefaultVisual")]
        internal extern static IntPtr XDefaultVisual(IntPtr display, int screen);

        // https://linux.die.net/man/3/xgetvisualinfo
        [DllImport("libX11", EntryPoint = "XGetVisualInfo")]
        internal extern static IntPtr XGetVisualInfo(IntPtr display, System.IntPtr vinfo_mask, ref XVisualInfo vinfo_template, ref int nitems);

        [DllImport("libX11", EntryPoint = "XVisualIDFromVisual")]
        internal extern static UIntPtr XVisualIDFromVisual(IntPtr visual);

        [DllImport("libX11", EntryPoint = "XFree")]
        internal extern static void XFree(IntPtr data);
    }
    
    
}