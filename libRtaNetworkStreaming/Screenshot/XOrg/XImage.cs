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
    
    
    public static unsafe class XLib
    {
    
        private const string libraryPath = "libX11";
        
        [System.Runtime.InteropServices.DllImport(libraryPath, EntryPoint = "XGetImage", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        [return: NativeTypeName("XImage *")]
        public static extern XImage* XGetImage([NativeTypeName("Display *")] UIntPtr display, [NativeTypeName("Drawable")] UIntPtr d, int x, int y, [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height, [NativeTypeName("unsigned long")] UIntPtr plane_mask, int format);

        
        [System.Runtime.InteropServices.DllImport(libraryPath, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        [return:System.Runtime.InteropServices.MarshalAs( System.Runtime.InteropServices.UnmanagedType.LPStruct)]        
        private static extern XImage GetFoo([NativeTypeName("Display *")] UIntPtr display
            , [NativeTypeName("Drawable")] UIntPtr d, int x, int y
            , [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height
            , [NativeTypeName("unsigned long")] UIntPtr plane_mask, int format);
        
        
    }


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

        [NativeTypeName("XPointer")]
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