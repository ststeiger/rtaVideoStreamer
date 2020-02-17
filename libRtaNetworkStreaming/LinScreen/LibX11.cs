using System.Drawing.Imaging;
using System.IO;

namespace rtaNetworking.Linux
{
    using System;
    using System.Runtime.InteropServices;
    using libRtaNetworkStreaming;

    public class tt
    {

        public static void safe()
        {
            //foo();
        }

     
        
        public static unsafe void TestX11()
        {
            int AllPlanes = ~0;
            System.UIntPtr AllPlanes2 = new System.UIntPtr((uint)AllPlanes);

            
            System.IntPtr display = LibX11Functions.XOpenDisplay(System.IntPtr.Zero);
            
            int defaultScreen = LibX11Functions.XDefaultScreen(display);
            System.UIntPtr window = LibX11Functions.XRootWindow(display, defaultScreen);
            XWindowAttributes xa = new XWindowAttributes();
            LibX11Functions.XGetWindowAttributes(display, window, ref xa);
            Screen* screen = xa.screen; // struct screen
            
            XShmSegmentInfo shminfo = new XShmSegmentInfo();
            
            
            XImage* ximg = LibXExt.XShmCreateImage(display, LibXExt.DefaultVisualOfScreen(screen)
                , (uint) LibXExt.DefaultDepthOfScreen(screen), LinScreen.ZPixmap
                , System.IntPtr.Zero, ref shminfo, (uint) xa.width, (uint) xa.height);
            
            shminfo.shmid = LibC.shmget(LibC.IPC_PRIVATE,  new System.IntPtr(ximg->bytes_per_line * ximg->height), LibC.IPC_CREAT|0777);
                ximg->data = (sbyte*)LibC.shmat(shminfo.shmid, System.IntPtr.Zero, 0);
            shminfo.shmaddr = (System.IntPtr) ximg->data;
            
            shminfo.readOnly = 0;    
            
            if(shminfo.shmid < 0)
                System.Console.WriteLine("Fatal shminfo error!");
            
            int s1 = LibXExt.XShmAttach(display, ref shminfo);
            // System.Console.WriteLine("XShmAttach() {0}\n", s1 != 0 ? "success!" : "failure!");
            
            int res = LibXExt.XShmGetImage(display, window, ximg, 0, 0, AllPlanes2);
            
            // const char *filename = "/tmp/test.bmp";
            // WriteBitmapToFile(filename, (int) ximg->bits_per_pixel, (int)window_attributes.width, (int)window_attributes.height, (const void*) ximg->data);
            int bytesPerPixel = (ximg->bits_per_pixel + 7) / 8;
            int stride = 4 * ((ximg->width * bytesPerPixel + 3) / 4);
            
            // long size = ximg->height * stride;
            // byte[] managedArray = new byte[size];
            // Marshal.Copy((IntPtr)(ximg->data), managedArray, 0, (int)size);
            // System.Console.WriteLine(managedArray);
            
            
            string filename = "/tmp/shtest.bmp";
            using(System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ximg->width, ximg->height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, (System.IntPtr) ximg->data))
            {
                // bmp.Save(filename);
                
                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    ms.ToArray();
                }
                
            }

            
            LibX11Functions.XDestroyImage2(ximg);
            LibXExt.XShmDetach(display, ref shminfo);
            LibC.shmdt(shminfo.shmaddr);
            
            LibX11Functions.XCloseDisplay(display);
        }
        
       
            
        public static unsafe void Foo(
            System.IntPtr display
            , System.UIntPtr d
            , int x, int y
            , uint width
            , uint height
            , System.UIntPtr plane_mask
            , int format)
        {
            XImage* img = LibX11Functions.XGetImage2(display, d, x, y, width, height, plane_mask, format);
            
            
            int bitsPerPixel = img->bits_per_pixel;
            System.UIntPtr ptrImg = (System.UIntPtr)img;
            System.UIntPtr pixels = (System.UIntPtr) (&(img->data));
            System.UIntPtr ptr_byte_order = (System.UIntPtr) (&(img->byte_order));

            ulong int64 = pixels.ToUInt64()- ptrImg.ToUInt64();
            ulong int642 = ptr_byte_order.ToUInt64()- ptrImg.ToUInt64();
            
            
            
            System.Console.WriteLine("p1: {0}, p2: {1}", ptrImg.ToUInt64(), pixels.ToUInt64());
            System.Console.WriteLine("Delta: {0}", int64); // 16
            System.Console.WriteLine("Delta 2: {0}", int642); // 16
            
            // https://stackoverflow.com/questions/30476131/c-sharp-read-pointer-address-value
            IntPtr ptr = (IntPtr)(ptrImg.ToUInt64()+16);
            long longValue = Marshal.ReadInt64(ptr);
            
            System.Console.WriteLine("pt1: {0}, pt2: {1}", ((System.UIntPtr)(img->data)).ToUInt64(), longValue);
            
            
            // BMPImage * foo = CreateBitmapFromScan0(uint16_t bitsPerPixel, int32_t w, int32_t h, uint8_t* scan0);
            string filename = "/tmp/lol1.bmp";
            // WriteBitmapToFile(filename, bitsPerPixel, width, height, );
            
            System.Console.WriteLine("Format: {0}, bpp: {1}", format, bitsPerPixel);
            
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * (((int)width * bytesPerPixel + 3) / 4);

            // long size = height * stride;
            // byte[] managedArray = new byte[size];
            // Marshal.Copy((IntPtr)(img->data), managedArray, 0, (int)size);
            


            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)width, (int)height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, (System.IntPtr)img->data);
            bmp.Save(filename);
            LibX11Functions.XDestroyImage2(img);
        }




    }
    


    // https://linux.die.net/man/3/xvisualinfo
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct XVisualInfo
    {
        public IntPtr visual;
        public UIntPtr visualid; // typedef unsigned long VisualID;; // https://code.woboq.org/qt5/include/X11/X.h.html
        public int screen;
        public int depth;
        public int klass;
        public UIntPtr red_mask; // unsigned long
        public UIntPtr green_mask; // unsigned long
        public UIntPtr blue_mask; // unsigned long
        public int colormap_size;
        public int bits_per_rgb;
    }


    public static class LibXExt
    {

        private const string LIBEXT = "libXext";
        
        [System.Runtime.InteropServices.DllImport(LIBEXT, EntryPoint = "XShmAttach", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int XShmAttach(System.IntPtr display, ref XShmSegmentInfo shminfo);
        // Bool XShmAttach( Display* /* dpy */, XShmSegmentInfo*	/* shminfo */ );
        
        
        // typedef struct _XDisplay Display
        // No, addresses aren't always positive - on x86_64,
        // pointers are sign-extended and the address space
        // is clustered symmetrically around 0
        // (though it is usual for the "negative" addresses to be kernel addresses).
        
        [System.Runtime.InteropServices.DllImport(LIBEXT, EntryPoint = "XShmDetach", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int XShmDetach(IntPtr display, ref XShmSegmentInfo shminfo);
        // Bool XShmDetach(Display* /* dpy */, XShmSegmentInfo*	/* shminfo */ );

        
        // XShmCreateImage XShmGetImage XShmAttach XShmDetach 
        // nm /usr/lib/x86_64-linux-gnu/libXext.so.6 -D | grep "XShmCreateImage"
        
        [System.Runtime.InteropServices.DllImport(LIBEXT, EntryPoint = "XShmGetImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern unsafe int XShmGetImage(IntPtr display, UIntPtr drawable, XImage* image, int x, int y, System.UIntPtr plane_mask);
        // Bool XShmGetImage(Display* /* dpy */, Drawable /* d */, XImage* /* image */, int /* x */, int /* y */, unsigned long /* plane_mask */);
        
        [System.Runtime.InteropServices.DllImport(LIBEXT, EntryPoint = "XShmCreateImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern unsafe XImage* XShmCreateImage(System.IntPtr display, Visual* visual, uint depth 
             , int format, System.IntPtr data, ref XShmSegmentInfo shminfo, uint width, uint height);
        
        // XImage* ximg = XShmCreateImage(display, DefaultVisualOfScreen(screen), DefaultDepthOfScreen(screen)
        // , ZPixmap, NULL, &shminfo, window_attributes.width, window_attributes.height);
        // XImage *XShmCreateImage(Display* /* dpy */, Visual* /* visual */, unsigned int /* depth */,
        // int /* format */,char* /* data */, XShmSegmentInfo*	/* shminfo */, unsigned int	/* width */, unsigned int	/* height */ );
        
        
        

        public static unsafe Visual* DefaultVisualOfScreen(Screen* screen)
        {
            return screen->root_visual;
        }
        
        public static unsafe int DefaultDepthOfScreen(Screen* screen)
        {
            return screen->root_depth;
        }

    }

    public static class LibC
    {
        
        public const int IPC_PRIVATE = 0; 
        public const int IPC_CREAT =	01000;		// Create key if key does not exist.
        public const int IPC_EXCL =02000;		// Fail if key exists.
        public const int IPC_NOWAIT = 04000;		// Return error on wait.

        
        
        private const string LIBC = "libc";
        
        
        [System.Runtime.InteropServices.DllImport(LIBC, EntryPoint = "shmget", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)] 
        public static extern int shmget( int key, System.IntPtr size, int shmflg ); 
        // extern int shmget (key_t __key, size_t __size, int __shmflg) __THROW;
        
        [System.Runtime.InteropServices.DllImport(LIBC, EntryPoint = "shmat", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)] 
        public static extern System.IntPtr shmat( int shmid, System.IntPtr shmaddr, int shmflg ); 
        // extern void *shmat (int __shmid, const void *__shmaddr, int __shmflg)
        
        [System.Runtime.InteropServices.DllImport(LIBC, EntryPoint = "shmdt", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)] 
        public static extern int shmdt( System.IntPtr shmaddr ); 
        // extern int shmdt (const void *__shmaddr) __THROW;
        
    }


    public static class LibXfixes
    {
        private const string LIBXFIXES = "libXfixes";
        
        
        [DllImport(LIBXFIXES, EntryPoint = "XFixesGetCursorImage")]
        public static extern IntPtr XFixesGetCursorImage(IntPtr display);    
    }
    
    
    public static unsafe class LibX11Functions
    {
        private const string LIBX11 = "libX11";
        
        
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XGetImage"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        [return: NativeTypeName("XImage *")]
        public static extern XImage* XGetImage2(
              [NativeTypeName("Display *")] System.IntPtr display
            , [NativeTypeName("Drawable")] System.UIntPtr d
            , int x, int y
            , [NativeTypeName("unsigned int")] uint width
            , [NativeTypeName("unsigned int")] uint height
            , [NativeTypeName("unsigned long")] System.UIntPtr plane_mask
            , int format);
        
        
        
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XGetWindowAttributes"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        // extern Status XGetWindowAttributes(Display* /* display */, Window /* w */, XWindowAttributes*	/* window_attributes_return */);
        public static extern int XGetWindowAttributes(System.IntPtr display, UIntPtr window, ref XWindowAttributes windowAttributesReturn);
        
        
        // Some special X11 stuff
        [DllImport(LIBX11, EntryPoint = "XOpenDisplay")]
        public static extern IntPtr XOpenDisplay(System.IntPtr display);

        [DllImport(LIBX11, EntryPoint = "XCloseDisplay")]
        public static extern int XCloseDisplay(System.IntPtr display);

        [DllImport(LIBX11, EntryPoint = "XRootWindow")]
        public static extern System.UIntPtr XRootWindow(System.IntPtr display, int screen);

        [DllImport(LIBX11, EntryPoint = "XDefaultScreen")]
        public static extern int XDefaultScreen(System.IntPtr display);

        [DllImport(LIBX11, EntryPoint = "XDefaultDepth")]
        public static extern uint XDefaultDepth(System.IntPtr display, int screen);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDisplayHeight")]
        public static extern int DisplayHeight(System.IntPtr display, int screen_number);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDisplayWidth")]
        public static extern int DisplayWidth(System.IntPtr display, int screen_number);

        
        [System.Runtime.InteropServices.DllImport("libXfixes", EntryPoint = "XFixesGetCursorImage")]
        public static extern System.IntPtr XFixesGetCursorImage(System.IntPtr display);
        // XFixesCursorImage * XFixesGetCursorImage (Display *dpy);
        
        
        // https://tronche.com/gui/x/xlib/graphics/XGetImage.html
        [DllImport(LIBX11, EntryPoint = "XGetImage")]
        public static extern IntPtr XGetImage(IntPtr display, System.UIntPtr drawable, int src_x, int src_y, uint width, uint height, System.UIntPtr pane, int format);

        [DllImport(LIBX11, EntryPoint = "XGetPixel")]
        public static extern int XGetPixel(IntPtr image, int x, int y);

        [DllImport(LIBX11, EntryPoint = "XDestroyImage")]
        public static extern int XDestroyImage(IntPtr image);
        
        [DllImport(LIBX11, EntryPoint = "XDestroyImage")]
        public static extern int XDestroyImage2(XImage* image);
        
        [DllImport(LIBX11, EntryPoint = "XDefaultVisual")]
        public static extern IntPtr XDefaultVisual(IntPtr display, int screen);

        // https://linux.die.net/man/3/xgetvisualinfo
        [DllImport(LIBX11, EntryPoint = "XGetVisualInfo")]
        public static extern IntPtr XGetVisualInfo(IntPtr display, System.IntPtr vinfo_mask, ref XVisualInfo vinfo_template, ref int nitems);

        [DllImport(LIBX11, EntryPoint = "XVisualIDFromVisual")]
        public static extern UIntPtr XVisualIDFromVisual(IntPtr visual);

        [DllImport(LIBX11, EntryPoint = "XFree")]
        public static extern void XFree(IntPtr data);
    }
    
    
}