// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// Ported from X11\Xlib.h in the Xlib - C Language X Interface: X Version 11, Release 7.7
// Original source is Copyright © The Open Group


namespace libRtaNetworkStreaming
{
    sealed class NativeTypeNameAttribute
        : System.Attribute
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

        public object MarshalNativeToManaged(System.IntPtr nativeData) =>
            System.Runtime.InteropServices.Marshal.PtrToStructure<XImage>(nativeData);

        // in this sample I suppose the native side uses GlobalAlloc (or LocalAlloc)
        // but you can use any allocation library provided you use the same on both sides
        public void CleanUpNativeData(System.IntPtr nativeData) =>
            System.Runtime.InteropServices.Marshal.FreeHGlobal(nativeData);

        public System.IntPtr MarshalManagedToNative(object managedObj) => throw new System.NotImplementedException();
        public void CleanUpManagedData(object managedObj) => throw new System.NotImplementedException();
    }


    // #define IPC_CREAT	01000		/* Create key if key does not exist. */
    // #define IPC_EXCL	02000		/* Fail if key exists.  */
    // #define IPC_NOWAIT	04000		/* Return error on wait.  */

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct XShmSegmentInfo
    {
        public System.UIntPtr shmseg; /* resource id */ // typedef unsigned long ShmSeg;
        public int shmid; /* kernel id */
        public System.IntPtr shmaddr; /* char * - address in client */
        public int readOnly; /* Bool - how the server should attach it */
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
    unsafe struct User
    {
        int id;
        fixed char name[12];
        private char* lol;
    }


    public unsafe struct XExtData
    {
        public int number; /* number returned by XRegisterExtension */
        public XExtData* next; /* next item on list of data for structure */
        public System.IntPtr free_private; // int (*free_private)(	/* called to free private storage */
        public XExtData* extension;
        public char* private_data; /* data private to this extension. */ // typedef char *XPointer;
    };

    public unsafe struct Visual
    {
        public XExtData* ext_data; /* hook for extension to hang data */
        public System.UIntPtr visualid; /* visual id of this visual */ // typedef unsigned long VisualID;
        public int c_class; /* C++ class of screen (monochrome, etc.) */
        public System.UIntPtr red_mask, green_mask, blue_mask; /* mask values */ // unsigned long 
        public int bits_per_rgb; /* log base 2 of distinct color values */
        public int map_entries; /* color map entries */
    }

    public unsafe struct Depth
    {
        public int depth; /* this depth (Z) of the depth */
        public int nvisuals; /* number of Visual types at this depth */
        public Visual* visuals; /* list of visuals possible at this depth */
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe struct Screen
    {
        public XExtData* ext_data; /* hook for extension to hang data */
        public System.IntPtr display; /* back pointer to display structure */ // typedef struct _XDisplay Display;
        public System.UIntPtr root; /* Root window id. */ // typedef XID Window; typedef unsigned long XID;
        public int width, height; /* width and height of screen */
        public int mwidth, mheight; /* width and height of  in millimeters */
        public int ndepths; /* number of depths possible */
        public Depth* depths; /* list of allowable depths on the screen */
        public int root_depth; /* bits per pixel */
        public Visual* root_visual; /* root visual */
        public System.IntPtr default_gc; /* GC for the root root visual */ // GC
        public System.UIntPtr cmap; /* default color map */ // typedef XID Colormap; typedef unsigned long XID;
        public System.UIntPtr white_pixel; // unsigned long
        public System.UIntPtr black_pixel; /* White and Black pixel values */ // unsigned long
        public int max_maps, min_maps; /* max and min color maps */
        public int backing_store; /* Never, WhenMapped, Always */
        public int save_unders; // #define Bool int
        public long root_input_mask; /* initial root input mask */
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe struct XWindowAttributes
    {
        public int x, y; // location of window 
        public int width, height; // width and height of window 
        public int border_width; // border width of window 
        public int depth; // depth of window 
        public System.IntPtr visual; // Visual * - the associated visual structure 
        
        public System.UIntPtr root; // root of screen containing window  // typedef XID Window; // // typedef unsigned long XID;
        
        public int c_class; // InputOutput, InputOnly
        public int bit_gravity; // one of bit gravity values 
        public int win_gravity; // one of the window gravity values 
        public int backing_store; // NotUseful, WhenMapped, Always 
        public System.UIntPtr backing_planes; //unsigned long - planes to be preserved if possible 
        public System.UIntPtr backing_pixel; //unsigned long - value to be used when restoring planes 
        public int save_under; // Bool - boolean, should bits under be saved? 
        
        public System.UIntPtr colormap; // color map to be associated with window  // typedef XID Colormap; // typedef unsigned long XID;
        
        public int map_installed; // Bool - boolean, is color map currently installed
        public int map_state; // IsUnmapped, IsUnviewable, IsViewable 
        public long all_event_masks; // set of events all people have interest in
        public long your_event_mask; // my event mask 
        public long do_not_propagate_mask; // set of events that should not propagate 
        public int override_redirect; // Bool - boolean value for override-redirect 
        public Screen* screen; // Screen * - back pointer to correct screen 
    }
    
    
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe struct XFixesCursorImage
    {
        public short x;
        public short y;
        public ushort width; // unsigned short
        public ushort height; // unsigned short
        public ushort xhot; // unsigned short
        public ushort yhot; // unsigned short
        public System.UIntPtr cursor_serial; // unsigned long

        public System.UIntPtr* pixels; // unsigned long*

        // #if XFIXES_MAJOR >= 2 // #define XFIXES_MAJOR 5
        public System.UIntPtr atom; // Version >= 2 only // typedef unsigned long Atom;

        public System.IntPtr name; // Version >= 2 only // const char*
        // #endif
    }


    public static unsafe class XLib
    {
        // [System.Runtime.InteropServices.DllImport("mylib", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        // [return : System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.CustomMarshaler, System.Runtime.InteropServices.MarshalTypeRef= typeof(TypeMarshaler))]
        // [ System.Runtime.InteropServices.MarshalAs()]
        // private static extern XImage Foo();


        // language:C#  extension:cs

        // shmget shmat shmdt:
        // nm /lib/x86_64-linux-gnu/libc.so.6 -D | grep  "shmget"


        private const string LibX11 = "libX11";


        // [System.Runtime.InteropServices.DllImport("libc")] 
        // public static extern int shmctl(int shmid, int cmd, System.IntPtr buf); 
        // extern int shmctl (int __shmid, int __cmd, struct shmid_ds *__buf) __THROW;


        [System.Runtime.InteropServices.DllImport(LibX11,
            CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStruct)]
        private static extern XImage GetFoo([NativeTypeName("Display *")] System.UIntPtr display
            , [NativeTypeName("Drawable")] System.UIntPtr d, int x, int y
            , [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height
            , [NativeTypeName("unsigned long")] System.UIntPtr plane_mask, int format);
    }


    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public unsafe struct XImage
    {
        public int width; // offset 0
        public int height; // offset 4

        public int xoffset; // offset 8 
        public int format; // offset 12 

        [NativeTypeName("char *")] public sbyte* data; // offset 16

        public int byte_order; // 24 (x64), 20 (x32)
        public int bitmap_unit;
        public int bitmap_bit_order;
        public int bitmap_pad;
        public int depth;
        public int bytes_per_line;
        public int bits_per_pixel;

        [NativeTypeName("unsigned long")] public System.UIntPtr red_mask;

        [NativeTypeName("unsigned long")] public System.UIntPtr green_mask;

        [NativeTypeName("unsigned long")] public System.UIntPtr blue_mask;

        [NativeTypeName("XPointer")] // typedef char *XPointer;
        public sbyte* obdata;

        [NativeTypeName("struct funcs")] public funcs f;

        public partial struct funcs
        {
            [NativeTypeName(
                "struct XImage *(*)(struct XDisplay *, Visual *, unsigned int, int, int, char *, unsigned int, unsigned int, int, int)")]
            public System.IntPtr create_image;

            [NativeTypeName("int (*)(struct XImage *)")]
            public System.IntPtr destroy_image;

            [NativeTypeName("unsigned long (*)(struct XImage *, int, int)")]
            public System.IntPtr get_pixel;

            [NativeTypeName("int (*)(struct XImage *, int, int, unsigned long)")]
            public System.IntPtr put_pixel;

            [NativeTypeName("struct XImage *(*)(struct XImage *, int, int, unsigned int, unsigned int)")]
            public System.IntPtr sub_image;

            [NativeTypeName("int (*)(struct XImage *, long)")]
            public System.IntPtr add_pixel;
        }
    }
    
    
}