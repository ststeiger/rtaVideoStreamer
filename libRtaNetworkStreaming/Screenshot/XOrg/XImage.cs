// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from X11\Xlib.h in the Xlib - C Language X Interface: X Version 11, Release 7.7
// Original source is Copyright © The Open Group

using System;

namespace libRtaNetworkStreaming
{
    
    sealed class NativeTypeNameAttribute 
        : Attribute
    {
        // See the attribute guidelines at http://go.microsoft.com/fwlink/?LinkId=85236
        public NativeTypeNameAttribute(string name)
        {
        }
        
    }
    
    
    public class TypeMarshaler 
        : System.Runtime.InteropServices.ICustomMarshaler
    {
        public static readonly TypeMarshaler s_Instance = new TypeMarshaler();

        public static System.Runtime.InteropServices.ICustomMarshaler 
            GetInstance(string cookie) => s_Instance;

        public int GetNativeDataSize() => System.Runtime.InteropServices.Marshal.SizeOf(typeof(XImage));

        public object MarshalNativeToManaged(IntPtr nativeData) => 
            System.Runtime.InteropServices.Marshal.PtrToStructure<XImage>(nativeData);

        // in this sample I suppose the native side uses GlobalAlloc (or LocalAlloc)
        // but you can use any allocation library provided you use the same on both sides
        public void CleanUpNativeData(IntPtr nativeData) => System.Runtime.InteropServices.Marshal.FreeHGlobal(nativeData);

        public IntPtr MarshalManagedToNative(object managedObj) => throw new NotImplementedException();
        public void CleanUpManagedData(object managedObj) => throw new NotImplementedException();
    }

    // #define IPC_CREAT	01000		/* Create key if key does not exist. */
    // #define IPC_EXCL	02000		/* Fail if key exists.  */
    // #define IPC_NOWAIT	04000		/* Return error on wait.  */
    
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct XShmSegmentInfo 
    {
        public System.UIntPtr shmseg;	/* resource id */ // typedef unsigned long ShmSeg;
        public int shmid;		/* kernel id */
        public System.IntPtr shmaddr;	/* char * - address in client */
        public int readOnly;	/* Bool - how the server should attach it */ 
    } 

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct XWindowAttributes 
    {
        public int x, y;			/* location of window */
        public int width, height;		/* width and height of window */
        public int border_width;		/* border width of window */
        public int depth;          	/* depth of window */
        public System.IntPtr visual;		/* Visual * - the associated visual structure */
        public System.UIntPtr root;        	/* root of screen containing window */ // typedef XID Window; // // typedef unsigned long XID;
        public int c_class;			/* InputOutput, InputOnly*/
        public int bit_gravity;		/* one of bit gravity values */
        public int win_gravity;		/* one of the window gravity values */
        public int backing_store;		/* NotUseful, WhenMapped, Always */
        public System.UIntPtr backing_planes;/*unsigned long - planes to be preserved if possible */
        public System.UIntPtr backing_pixel;/*unsigned long - value to be used when restoring planes */
        public int save_under;		/* Bool - boolean, should bits under be saved? */
        public System.UIntPtr colormap;		/* color map to be associated with window */ // typedef XID Colormap; // typedef unsigned long XID;
        public int map_installed;		/* Bool - boolean, is color map currently installed*/
        public int map_state;		/* IsUnmapped, IsUnviewable, IsViewable */
        public long all_event_masks;	/* set of events all people have interest in*/
        public long your_event_mask;	/* my event mask */
        public long do_not_propagate_mask; /* set of events that should not propagate */
        public int override_redirect;	/* Bool - boolean value for override-redirect */
        public System.IntPtr screen;		/* Screen * - back pointer to correct screen */
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct XFixesCursorImage
    {
        public short x, y;
        public ushort width, height; // unsigned short
        public ushort xhot, yhot; // unsigned short
        public System.UIntPtr cursor_serial; // unsigned long
        public System.IntPtr pixels; // unsigned long*
        // #if XFIXES_MAJOR >= 2
        public System.UIntPtr atom; /* Version >= 2 only */ // typedef unsigned long Atom;
        public System.IntPtr name; /* Version >= 2 only */ // const char*
        // #endif
    }


    public static unsafe class XLib
    {
    
        
        // [System.Runtime.InteropServices.DllImport("mylib", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        // [return : System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.CustomMarshaler, System.Runtime.InteropServices.MarshalTypeRef= typeof(TypeMarshaler))]
        // [ System.Runtime.InteropServices.MarshalAs()]
        // private static extern XImage Foo();

        
        
        // XShmCreateImage XShmAttach XShmGetImage XShmDetach 
        // nm /usr/lib/x86_64-linux-gnu/libXext.so.6 -D | grep "XShmCreateImage"
        // language:C#  extension:cs
        
        // shmget shmat shmdt:
        // nm /lib/x86_64-linux-gnu/libc.so.6 -D | grep  "shmget"


        private const string LibX11 = "libX11";
        
        
        [System.Runtime.InteropServices.DllImport("libc")] 
        public static extern int shmget( int key, System.IntPtr size, int shmflg ); 
        // extern int shmget (key_t __key, size_t __size, int __shmflg) __THROW;
        
        [System.Runtime.InteropServices.DllImport("libc")] 
        public static extern int shmat( int shmid, System.IntPtr shmaddr, int shmflg ); 
        // extern void *shmat (int __shmid, const void *__shmaddr, int __shmflg)
        
        [System.Runtime.InteropServices.DllImport("libc")] 
        public static extern int shmdt( System.IntPtr shmaddr ); 
        // extern int shmdt (const void *__shmaddr) __THROW;
        
        // [System.Runtime.InteropServices.DllImport("libc")] 
        // public static extern int shmctl(int shmid, int cmd, System.IntPtr buf); 
        // extern int shmctl (int __shmid, int __cmd, struct shmid_ds *__buf) __THROW;
        
            
        
        [System.Runtime.InteropServices.DllImport(LibX11, EntryPoint = "XShmAttach", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int XShmAttach(UIntPtr display, ref XShmSegmentInfo shminfo);
        // Bool XShmAttach( Display* /* dpy */, XShmSegmentInfo*	/* shminfo */ );
        
        [System.Runtime.InteropServices.DllImport(LibX11, EntryPoint = "XShmDetach", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int XShmDetach(UIntPtr display, ref XShmSegmentInfo shminfo);
        // Bool XShmDetach(Display* /* dpy */, XShmSegmentInfo*	/* shminfo */ );

        
        
        [System.Runtime.InteropServices.DllImport(LibX11, EntryPoint = "XShmGetImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int XShmGetImage(UIntPtr display, UIntPtr drawable, ref XImage image, int x, int y, System.UIntPtr plane_mask);
        // Bool XShmGetImage(Display* /* dpy */, Drawable /* d */, XImage* /* image */, int /* x */, int /* y */, unsigned long /* plane_mask */);
        
        
        

        [System.Runtime.InteropServices.DllImport(LibX11, EntryPoint = "XGetImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        [return: NativeTypeName("XImage *")]
        public static extern XImage* XGetImage(
              [NativeTypeName("Display *")] UIntPtr display
            , [NativeTypeName("Drawable")] UIntPtr d
            , int x, int y
            , [NativeTypeName("unsigned int")] uint width
            , [NativeTypeName("unsigned int")] uint height
            , [NativeTypeName("unsigned long")] UIntPtr plane_mask, int format);


        [System.Runtime.InteropServices.DllImport(LibX11, EntryPoint = "XGetWindowAttributes"
            , CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        // extern Status XGetWindowAttributes(Display* /* display */, Window /* w */, XWindowAttributes*	/* window_attributes_return */);
        private static extern int XGetWindowAttributes(UIntPtr display, UIntPtr window, ref XWindowAttributes windowAttributesReturn);
        
        
        
        
        [System.Runtime.InteropServices.DllImport(LibX11, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        [return:System.Runtime.InteropServices.MarshalAs( System.Runtime.InteropServices.UnmanagedType.LPStruct)]        
        private static extern XImage GetFoo([NativeTypeName("Display *")] UIntPtr display
            , [NativeTypeName("Drawable")] UIntPtr d, int x, int y
            , [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height
            , [NativeTypeName("unsigned long")] UIntPtr plane_mask, int format);
        
        
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe partial struct XImage
    {
        public int width;
        public int height;

        public int xoffset;
        public int format;

        [NativeTypeName("char *")]
        public sbyte* data;

        public int byte_order;
        public int bitmap_unit;    
        public int bitmap_bit_order;
        public int bitmap_pad;
        public int depth;
        public int bytes_per_line;
        public int bits_per_pixel;
        
        [NativeTypeName("unsigned long")]
        public UIntPtr red_mask;
        
        [NativeTypeName("unsigned long")]
        public UIntPtr green_mask;
        
        [NativeTypeName("unsigned long")]
        public UIntPtr blue_mask;
        
        [NativeTypeName("XPointer")] // typedef char *XPointer;
        public sbyte* obdata;
        
        [NativeTypeName("struct funcs")]
        public funcs f;
        
        public partial struct funcs
        {
            [NativeTypeName("struct XImage *(*)(struct XDisplay *, Visual *, unsigned int, int, int, char *, unsigned int, unsigned int, int, int)")]
            public IntPtr create_image;

            [NativeTypeName("int (*)(struct XImage *)")]
            public IntPtr destroy_image;

            [NativeTypeName("unsigned long (*)(struct XImage *, int, int)")]
            public IntPtr get_pixel;

            [NativeTypeName("int (*)(struct XImage *, int, int, unsigned long)")]
            public IntPtr put_pixel;

            [NativeTypeName("struct XImage *(*)(struct XImage *, int, int, unsigned int, unsigned int)")]
            public IntPtr sub_image;

            [NativeTypeName("int (*)(struct XImage *, long)")]
            public IntPtr add_pixel;
        }
    }
}