
namespace rtaNetworking.Linux
{

    
    using libRtaNetworkStreaming;


    // /usr/include/X11/cursorfont.h
    public enum CursorSerials 
        : int
    {
         XC_num_glyphs=154
        ,XC_X_cursor=0
        ,XC_arrow=2
        ,XC_based_arrow_down=4
        ,XC_based_arrow_up=6
        ,XC_boat=8
        ,XC_bogosity=10
        ,XC_bottom_left_corner=12
        ,XC_bottom_right_corner=14
        ,XC_bottom_side=16
        ,XC_bottom_tee=18
        ,XC_box_spiral=20
        ,XC_center_ptr=22
        ,XC_circle=24
        ,XC_clock=26
        ,XC_coffee_mug=28
        ,XC_cross=30
        ,XC_cross_reverse=32
        ,XC_crosshair=34
        ,XC_diamond_cross=36
        ,XC_dot=38
        ,XC_dotbox=40
        ,XC_double_arrow=42
        ,XC_draft_large=44
        ,XC_draft_small=46
        ,XC_draped_box=48
        ,XC_exchange=50
        ,XC_fleur=52
        ,XC_gobbler=54
        ,XC_gumby=56
        ,XC_hand1=58
        ,XC_hand2=60
        ,XC_heart=62
        ,XC_icon=64
        ,XC_iron_cross=66
        ,XC_left_ptr=68
        ,XC_left_side=70
        ,XC_left_tee=72
        ,XC_leftbutton=74
        ,XC_ll_angle=76
        ,XC_lr_angle=78
        ,XC_man=80
        ,XC_middlebutton=82
        ,XC_mouse=84
        ,XC_pencil=86
        ,XC_pirate=88
        ,XC_plus=90
        ,XC_question_arrow=92
        ,XC_right_ptr=94
        ,XC_right_side=96
        ,XC_right_tee=98
        ,XC_rightbutton=100
        ,XC_rtl_logo=102
        ,XC_sailboat=104
        ,XC_sb_down_arrow=106
        ,XC_sb_h_double_arrow=108
        ,XC_sb_left_arrow=110
        ,XC_sb_right_arrow=112
        ,XC_sb_up_arrow=114
        ,XC_sb_v_double_arrow=116
        ,XC_shuttle=118
        ,XC_sizing=120
        ,XC_spider=122
        ,XC_spraycan=124
        ,XC_star=126
        ,XC_target=128
        ,XC_tcross=130
        ,XC_top_left_arrow=132
        ,XC_top_left_corner=134
        ,XC_top_right_corner=136
        ,XC_top_side=138
        ,XC_top_tee=140
        ,XC_trek=142
        ,XC_ul_angle=144
        ,XC_umbrella=146
        ,XC_ur_angle=148
        ,XC_watch=150
        ,XC_xterm=152
    }
    
    
    public class SafeX11
    {

        public static byte[] X11Screenshot(bool withCursor)
        {
            return UnsafeX11ScreenshotWithCursor(withCursor);
        }


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static unsafe byte[] UnsafeX11ScreenshotWithCursor(bool withCursor)
        {
            byte[] result = null;
            
            
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

            if (withCursor)
            {
                PaintMousePointer(display, ximg);
            } // End if (withCursor) 


            using(System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ximg->width, ximg->height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, (System.IntPtr) ximg->data))
            {
                // bmp.Save("/tmp/shtest.bmp");

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    result = ms.ToArray();
                } // End Using ms 
                
            } // End Using bmp 
            
            LibX11Functions.XDestroyImage2(ximg);
            LibXExt.XShmDetach(display, ref shminfo);
            LibC.shmdt(shminfo.shmaddr);
            
            LibX11Functions.XCloseDisplay(display);
            
            return result;
        } // End Function UnsafeX11ScreenshotWithCursor 




        // https://stackoverflow.com/questions/28300149/is-there-a-list-of-all-xfixes-cursor-types
        // https://ffmpeg.org/doxygen/2.8/common_8h.html
        // #define 	FFMAX(a, b)   ((a) > (b) ? (a) : (b))
        // #define 	FFMIN(a, b)   ((a) > (b) ? (b) : (a))

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static int FFMAX(int a, int b)
        {
            if (a > b)
                return a;

            return b;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static int FFMIN(int a, int b)
        {
            if (a > b)
                return b;
            
            return a;
        }


        // http://www.staroceans.org/myprojects/ffplay/libavdevice/x11grab.c
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static unsafe void PaintMousePointer(System.IntPtr dpy, XImage *image) //, struct x11grab *s)
        {
            // int x_off = s->x_off;
            // int y_off = s->y_off;
            // int width = s->width;
            // int height = s->height;
            // Display *dpy = s->dpy;

            int x_off = 0;
            int y_off = 0;
            int width = image->width;
            int height = image->height;

            XFixesCursorImage *xcim;
            int x, y;
            int line, column;
            int to_line, to_column;
            int pixstride = image->bits_per_pixel >> 3;
            // Warning: in its insanity, xlib provides unsigned image data through a
            // char* pointer, so we have to make it uint8_t to make things not break.
            // Anyone who performs further investigation of the xlib API likely risks
            // permanent brain damage. 
            
            byte *pix = (byte*) image->data;
            
            // Cursor c;
            // Window w;
            // XSetWindowAttributes attr;
            
            // Code doesn't currently support 16-bit or PAL8
            if (image->bits_per_pixel != 24 && image->bits_per_pixel != 32)
                return;

            // c = XCreateFontCursor(dpy, XC_left_ptr);
            // w = DefaultRootWindow(dpy);
            // attr.cursor = c;
            // XChangeWindowAttributes(dpy, w, CWCursor, &attr);

            xcim = LibXfixes.XFixesGetCursorImage(dpy);
            
            x = xcim->x - xcim->xhot;
            y = xcim->y - xcim->yhot;

            to_line = FFMIN((y + xcim->height), (height + y_off));
            to_column = FFMIN((x + xcim->width), (width + x_off));

            for (line = FFMAX(y, y_off); line < to_line; line++)
            {
                for (column = FFMAX(x, x_off); column < to_column; column++)
                {
                    int xcim_addr = (line - y) * xcim->width + column - x;
                    int image_addr = ((line - y_off) * width + column - x_off) * pixstride;
                    byte r = (byte)(xcim->pixels[xcim_addr].ToUInt64() >>  0);
                    byte g = (byte)(xcim->pixels[xcim_addr].ToUInt64() >>  8);
                    byte b = (byte)(xcim->pixels[xcim_addr].ToUInt64() >> 16);
                    byte a = (byte)(xcim->pixels[xcim_addr].ToUInt64() >> 24);
                    
                    if (a == 255)
                    {
                        pix[image_addr+0] = r;
                        pix[image_addr+1] = g;
                        pix[image_addr+2] = b;
                    }
                    else if (a != 0)
                    {
                        byte aaa = pix[image_addr + 2];
                        
                        // pixel values from XFixesGetCursorImage come premultiplied by alpha
                        pix[image_addr+0] = (byte) (r + (pix[image_addr+0]*(255-a) + 255/2) / 255);
                        pix[image_addr+1] = (byte) (g + (pix[image_addr+1]*(255-a) + 255/2) / 255);
                        pix[image_addr+2] = (byte) (b + (pix[image_addr+2]*(255-a) + 255/2) / 255);
                    }
                }
            }
            
            LibX11Functions.XFree(xcim);
            xcim = null;
        }



        public static byte[] SlowScreenshot(
             System.IntPtr display
           , System.UIntPtr d
           , int x, int y
           , uint width
           , uint height
           , System.UIntPtr plane_mask
           , int format, bool withCursor)
        {
            byte[] ret = SlowScreenshotWithCursor(display, d, x, y, width, height, plane_mask, format, withCursor);

            return ret;
        }



        private static unsafe byte[] SlowScreenshotWithCursor(
              System.IntPtr display
            , System.UIntPtr d
            , int x, int y
            , uint width
            , uint height
            , System.UIntPtr plane_mask
            , int format, bool withCursor)
        {
            byte[] result = null;

            XImage* img = LibX11Functions.XGetImage2(display, d, x, y, width, height, plane_mask, format);

            if (withCursor)
            {
                PaintMousePointer(display, img);
            } // End if (withCursor) 

            
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
            System.IntPtr ptr = (System.IntPtr)(ptrImg.ToUInt64()+16);
            long longValue = System.Runtime.InteropServices.Marshal.ReadInt64(ptr);

            

            System.Console.WriteLine("pt1: {0}, pt2: {1}", ((System.UIntPtr)(img->data)).ToUInt64(), longValue);
            
            
            // BMPImage * foo = CreateBitmapFromScan0(uint16_t bitsPerPixel, int32_t w, int32_t h, uint8_t* scan0);
            // string filename = "/tmp/lol1.bmp";
            // WriteBitmapToFile(filename, bitsPerPixel, width, height, );
            
            System.Console.WriteLine("Format: {0}, bpp: {1}", format, bitsPerPixel);
            
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * (((int)width * bytesPerPixel + 3) / 4);

            // long size = height * stride;
            // byte[] managedArray = new byte[size];
            // System.Runtime.InteropServices.Marshal.Copy((System.IntPtr)(img->data), managedArray, 0, (int)size);

            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)width, (int)height, stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, (System.IntPtr)img->data))
            {
                // bmp.Save("/tmp/lol1.bmp");

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    result = ms.ToArray();
                } // End Using ms 

            } // End Using bmp 
                
            LibX11Functions.XDestroyImage2(img);

            return result;
        }




    }
    


    // https://linux.die.net/man/3/xvisualinfo
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct XVisualInfo
    {
        public System.IntPtr visual;
        public System.UIntPtr visualid; // typedef unsigned long VisualID;; // https://code.woboq.org/qt5/include/X11/X.h.html
        public int screen;
        public int depth;
        public int klass;
        public System.UIntPtr red_mask; // unsigned long
        public System.UIntPtr green_mask; // unsigned long
        public System.UIntPtr blue_mask; // unsigned long
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
        public static extern int XShmDetach(System.IntPtr display, ref XShmSegmentInfo shminfo);
        // Bool XShmDetach(Display* /* dpy */, XShmSegmentInfo*	/* shminfo */ );

        
        // XShmCreateImage XShmGetImage XShmAttach XShmDetach 
        // nm /usr/lib/x86_64-linux-gnu/libXext.so.6 -D | grep "XShmCreateImage"
        
        [System.Runtime.InteropServices.DllImport(LIBEXT, EntryPoint = "XShmGetImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern unsafe int XShmGetImage(System.IntPtr display, System.UIntPtr drawable, XImage* image, int x, int y, System.UIntPtr plane_mask);
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
        
        
        [System.Runtime.InteropServices.DllImport(LIBXFIXES, EntryPoint = "XFixesGetCursorImage")]
        public static extern unsafe XFixesCursorImage* XFixesGetCursorImage(System.IntPtr display);    
        // XFixesCursorImage * XFixesGetCursorImage (Display *dpy);
        
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
        public static extern int XGetWindowAttributes(System.IntPtr display, System.UIntPtr window, ref XWindowAttributes windowAttributesReturn);
        
        
        // Some special X11 stuff
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XOpenDisplay")]
        public static extern System.IntPtr XOpenDisplay(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XCloseDisplay")]
        public static extern int XCloseDisplay(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XRootWindow")]
        public static extern System.UIntPtr XRootWindow(System.IntPtr display, int screen);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDefaultScreen")]
        public static extern int XDefaultScreen(System.IntPtr display);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDefaultDepth")]
        public static extern uint XDefaultDepth(System.IntPtr display, int screen);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDisplayHeight")]
        public static extern int DisplayHeight(System.IntPtr display, int screen_number);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDisplayWidth")]
        public static extern int DisplayWidth(System.IntPtr display, int screen_number);

        
        
        // https://tronche.com/gui/x/xlib/graphics/XGetImage.html
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XGetImage")]
        public static extern System.IntPtr XGetImage(System.IntPtr display, System.UIntPtr drawable, int src_x, int src_y, uint width, uint height, System.UIntPtr pane, int format);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XGetPixel")]
        public static extern int XGetPixel(System.IntPtr image, int x, int y);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDestroyImage")]
        public static extern int XDestroyImage(System.IntPtr image);
        
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDestroyImage")]
        public static extern int XDestroyImage2(XImage* image);
        
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XDefaultVisual")]
        public static extern System.IntPtr XDefaultVisual(System.IntPtr display, int screen);

        // https://linux.die.net/man/3/xgetvisualinfo
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XGetVisualInfo")]
        public static extern System.IntPtr XGetVisualInfo(System.IntPtr display, System.IntPtr vinfo_mask, ref XVisualInfo vinfo_template, ref int nitems);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XVisualIDFromVisual")]
        public static extern System.UIntPtr XVisualIDFromVisual(System.IntPtr visual);

        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XFree")]
        public static extern void XFree(System.IntPtr data);
        
        [System.Runtime.InteropServices.DllImport(LIBX11, EntryPoint = "XFree")]
        public static extern void XFree(XFixesCursorImage *xcim);
        
    }
    
    
}
