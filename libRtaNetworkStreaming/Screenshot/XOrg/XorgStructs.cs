﻿#region License
/*
MIT License
Copyright (c) 2004 Novell, Inc.
 Authors: Peter Bartok	pbartok@novell.com
All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License


// https://github.com/mono/tao/blob/master/src/Tao.Platform.X11/Structs.cs
// https://raw.githubusercontent.com/mono/tao/master/src/Tao.Platform.X11/Structs.cs

// X11 Version
namespace Xorg.Structs
{
    //
    // In the structures below, fields of type long are mapped to System.IntPtr.
    // This will work on all platforms where sizeof(long)==sizeof(void*), which
    // is almost all platforms except WIN64.
    //

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XAnyEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XKeyEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr root;
        internal System.IntPtr subwindow;
        internal System.IntPtr time;
        internal int x;
        internal int y;
        internal int x_root;
        internal int y_root;
        internal int state;
        internal int keycode;
        internal bool same_screen;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XButtonEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr root;
        internal System.IntPtr subwindow;
        internal System.IntPtr time;
        internal int x;
        internal int y;
        internal int x_root;
        internal int y_root;
        internal int state;
        internal int button;
        internal bool same_screen;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XMotionEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr root;
        internal System.IntPtr subwindow;
        internal System.IntPtr time;
        internal int x;
        internal int y;
        internal int x_root;
        internal int y_root;
        internal int state;
        internal byte is_hint;
        internal bool same_screen;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XCrossingEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr root;
        internal System.IntPtr subwindow;
        internal System.IntPtr time;
        internal int x;
        internal int y;
        internal int x_root;
        internal int y_root;
        internal NotifyMode mode;
        internal NotifyDetail detail;
        internal bool same_screen;
        internal bool focus;
        internal int state;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XFocusChangeEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal int mode;
        internal NotifyDetail detail;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XKeymapEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal byte key_vector0;
        internal byte key_vector1;
        internal byte key_vector2;
        internal byte key_vector3;
        internal byte key_vector4;
        internal byte key_vector5;
        internal byte key_vector6;
        internal byte key_vector7;
        internal byte key_vector8;
        internal byte key_vector9;
        internal byte key_vector10;
        internal byte key_vector11;
        internal byte key_vector12;
        internal byte key_vector13;
        internal byte key_vector14;
        internal byte key_vector15;
        internal byte key_vector16;
        internal byte key_vector17;
        internal byte key_vector18;
        internal byte key_vector19;
        internal byte key_vector20;
        internal byte key_vector21;
        internal byte key_vector22;
        internal byte key_vector23;
        internal byte key_vector24;
        internal byte key_vector25;
        internal byte key_vector26;
        internal byte key_vector27;
        internal byte key_vector28;
        internal byte key_vector29;
        internal byte key_vector30;
        internal byte key_vector31;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XExposeEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int count;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XGraphicsExposeEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr drawable;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int count;
        internal int major_code;
        internal int minor_code;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XNoExposeEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr drawable;
        internal int major_code;
        internal int minor_code;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XVisibilityEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal int state;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XCreateWindowEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr parent;
        internal System.IntPtr window;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int border_width;
        internal bool override_redirect;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XDestroyWindowEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr xevent;
        internal System.IntPtr window;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XUnmapEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr xevent;
        internal System.IntPtr window;
        internal bool from_configure;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XMapEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr xevent;
        internal System.IntPtr window;
        internal bool override_redirect;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XMapRequestEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr parent;
        internal System.IntPtr window;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XReparentEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr xevent;
        internal System.IntPtr window;
        internal System.IntPtr parent;
        internal int x;
        internal int y;
        internal bool override_redirect;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XConfigureEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr xevent;
        internal System.IntPtr window;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int border_width;
        internal System.IntPtr above;
        internal bool override_redirect;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XGravityEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr xevent;
        internal System.IntPtr window;
        internal int x;
        internal int y;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XResizeRequestEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal int width;
        internal int height;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XConfigureRequestEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr parent;
        internal System.IntPtr window;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int border_width;
        internal System.IntPtr above;
        internal int detail;
        internal System.IntPtr value_mask;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XCirculateEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr xevent;
        internal System.IntPtr window;
        internal int place;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XCirculateRequestEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr parent;
        internal System.IntPtr window;
        internal int place;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XPropertyEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr atom;
        internal System.IntPtr time;
        internal int state;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XSelectionClearEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr selection;
        internal System.IntPtr time;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XSelectionRequestEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr owner;
        internal System.IntPtr requestor;
        internal System.IntPtr selection;
        internal System.IntPtr target;
        internal System.IntPtr property;
        internal System.IntPtr time;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XSelectionEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr requestor;
        internal System.IntPtr selection;
        internal System.IntPtr target;
        internal System.IntPtr property;
        internal System.IntPtr time;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XColormapEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr colormap;
        internal bool c_new;
        internal int state;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XClientMessageEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal System.IntPtr message_type;
        internal int format;
        internal System.IntPtr ptr1;
        internal System.IntPtr ptr2;
        internal System.IntPtr ptr3;
        internal System.IntPtr ptr4;
        internal System.IntPtr ptr5;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XMappingEvent
    {
        internal XEventName type;
        internal System.IntPtr serial;
        internal bool send_event;
        internal System.IntPtr display;
        internal System.IntPtr window;
        internal int request;
        internal int first_keycode;
        internal int count;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XErrorEvent
    {
        internal XEventName type;
        internal System.IntPtr display;
        internal System.IntPtr resourceid;
        internal System.IntPtr serial;
        internal byte error_code;
        internal XRequest request_code;
        internal byte minor_code;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XEventPad
    {
        internal System.IntPtr pad0;
        internal System.IntPtr pad1;
        internal System.IntPtr pad2;
        internal System.IntPtr pad3;
        internal System.IntPtr pad4;
        internal System.IntPtr pad5;
        internal System.IntPtr pad6;
        internal System.IntPtr pad7;
        internal System.IntPtr pad8;
        internal System.IntPtr pad9;
        internal System.IntPtr pad10;
        internal System.IntPtr pad11;
        internal System.IntPtr pad12;
        internal System.IntPtr pad13;
        internal System.IntPtr pad14;
        internal System.IntPtr pad15;
        internal System.IntPtr pad16;
        internal System.IntPtr pad17;
        internal System.IntPtr pad18;
        internal System.IntPtr pad19;
        internal System.IntPtr pad20;
        internal System.IntPtr pad21;
        internal System.IntPtr pad22;
        internal System.IntPtr pad23;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    internal struct XEvent
    {
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XEventName type;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XAnyEvent AnyEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XKeyEvent KeyEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XButtonEvent ButtonEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XMotionEvent MotionEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XCrossingEvent CrossingEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XFocusChangeEvent FocusChangeEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XExposeEvent ExposeEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XGraphicsExposeEvent GraphicsExposeEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XNoExposeEvent NoExposeEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XVisibilityEvent VisibilityEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XCreateWindowEvent CreateWindowEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XDestroyWindowEvent DestroyWindowEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XUnmapEvent UnmapEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XMapEvent MapEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XMapRequestEvent MapRequestEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XReparentEvent ReparentEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XConfigureEvent ConfigureEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XGravityEvent GravityEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XResizeRequestEvent ResizeRequestEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XConfigureRequestEvent ConfigureRequestEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XCirculateEvent CirculateEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XCirculateRequestEvent CirculateRequestEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XPropertyEvent PropertyEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XSelectionClearEvent SelectionClearEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XSelectionRequestEvent SelectionRequestEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XSelectionEvent SelectionEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XColormapEvent ColormapEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XClientMessageEvent ClientMessageEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XMappingEvent MappingEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XErrorEvent ErrorEvent;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XKeymapEvent KeymapEvent;

        //[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=24)]
        //[ System.Runtime.InteropServices.FieldOffset(0) ] internal int[] pad;
        [System.Runtime.InteropServices.FieldOffset(0)]
        internal XEventPad Pad;
        public override string ToString()
        {
            switch (type)
            {
                case XEventName.PropertyNotify:
                    return ToString(PropertyEvent);
                case XEventName.ResizeRequest:
                    return ToString(ResizeRequestEvent);
                case XEventName.ConfigureNotify:
                    return ToString(ConfigureEvent);
                default:
                    return type.ToString();
            }
        }

        public static string ToString(object ev)
        {
            string result = string.Empty;
            System.Type type = ev.GetType();
            System.Reflection.FieldInfo[] fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                if (result != string.Empty)
                {
                    result += ", ";
                }
                object value = fields[i].GetValue(ev);
                result += fields[i].Name + "=" + (value == null ? "<null>" : value.ToString());
            }
            return type.Name + " (" + result + ")";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class XVisualInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public System.IntPtr visual;
        /// <summary>
        /// 
        /// </summary>
        public System.IntPtr visualid;
        /// <summary>
        /// 
        /// </summary>
        public int screen;
        /// <summary>
        /// 
        /// </summary>
        public int depth;
        /// <summary>
        /// 
        /// </summary>
        public XVisualClass @class;
        /// <summary>
        /// 
        /// </summary>
        public long redMask;
        /// <summary>
        /// 
        /// </summary>
        public long greenMask;
        /// <summary>
        /// 
        /// </summary>
        public long blueMask;
        /// <summary>
        /// 
        /// </summary>
        public int colormap_size;
        /// <summary>
        /// 
        /// </summary>
        public int bits_per_rgb;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("id ({0}), screen ({1}), depth ({2}), class ({3})",
            visualid, screen, depth, @class);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum XVisualClass : int
    {
        /// <summary>
        /// 
        /// </summary>
        StaticGray = 0,
        /// <summary>
        /// 
        /// </summary>
        GrayScale = 1,
        /// <summary>
        /// 
        /// </summary>
        StaticColor = 2,
        /// <summary>
        /// 
        /// </summary>
        PseudoColor = 3,
        /// <summary>
        /// 
        /// </summary>
        TrueColor = 4,
        /// <summary>
        /// 
        /// </summary>
        DirectColor = 5
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XSetWindowAttributes
    {
        internal System.IntPtr background_pixmap;
        internal System.IntPtr background_pixel;
        internal System.IntPtr border_pixmap;
        internal System.IntPtr border_pixel;
        internal Gravity bit_gravity;
        internal Gravity win_gravity;
        internal int backing_store;
        internal System.IntPtr backing_planes;
        internal System.IntPtr backing_pixel;
        internal bool save_under;
        internal System.IntPtr event_mask;
        internal System.IntPtr do_not_propagate_mask;
        internal bool override_redirect;
        internal System.IntPtr colormap;
        internal System.IntPtr cursor;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XWindowAttributes
    {
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int border_width;
        internal int depth;
        internal System.IntPtr visual;
        internal System.IntPtr root;
        internal int c_class;
        internal Gravity bit_gravity;
        internal Gravity win_gravity;
        internal int backing_store;
        internal System.IntPtr backing_planes;
        internal System.IntPtr backing_pixel;
        internal bool save_under;
        internal System.IntPtr colormap;
        internal bool map_installed;
        internal MapState map_state;
        internal System.IntPtr all_event_masks;
        internal System.IntPtr your_event_mask;
        internal System.IntPtr do_not_propagate_mask;
        internal bool override_direct;
        internal System.IntPtr screen;

        public override string ToString()
        {
            return XEvent.ToString(this);
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XTextProperty
    {
        internal string value;
        internal System.IntPtr encoding;
        internal int format;
        internal System.IntPtr nitems;
    }

    internal enum XWindowClass
    {
        InputOutput = 1,
        InputOnly = 2
    }

    internal enum XEventName
    {
        KeyPress = 2,
        KeyRelease = 3,
        ButtonPress = 4,
        ButtonRelease = 5,
        MotionNotify = 6,
        EnterNotify = 7,
        LeaveNotify = 8,
        FocusIn = 9,
        FocusOut = 10,
        KeymapNotify = 11,
        Expose = 12,
        GraphicsExpose = 13,
        NoExpose = 14,
        VisibilityNotify = 15,
        CreateNotify = 16,
        DestroyNotify = 17,
        UnmapNotify = 18,
        MapNotify = 19,
        MapRequest = 20,
        ReparentNotify = 21,
        ConfigureNotify = 22,
        ConfigureRequest = 23,
        GravityNotify = 24,
        ResizeRequest = 25,
        CirculateNotify = 26,
        CirculateRequest = 27,
        PropertyNotify = 28,
        SelectionClear = 29,
        SelectionRequest = 30,
        SelectionNotify = 31,
        ColormapNotify = 32,
        ClientMessage = 33,
        MappingNotify = 34,

        LASTEvent
    }

    [System.Flags]
    internal enum SetWindowValuemask
    {
        Nothing = 0,
        BackPixmap = 1,
        BackPixel = 2,
        BorderPixmap = 4,
        BorderPixel = 8,
        BitGravity = 16,
        WinGravity = 32,
        BackingStore = 64,
        BackingPlanes = 128,
        BackingPixel = 256,
        OverrideRedirect = 512,
        SaveUnder = 1024,
        EventMask = 2048,
        DontPropagate = 4096,
        ColorMap = 8192,
        Cursor = 16384
    }

    internal enum CreateWindowArgs
    {
        CopyFromParent = 0,
        ParentRelative = 1,
        InputOutput = 1,
        InputOnly = 2
    }

    internal enum Gravity
    {
        ForgetGravity = 0,
        NorthWestGravity = 1,
        NorthGravity = 2,
        NorthEastGravity = 3,
        WestGravity = 4,
        CenterGravity = 5,
        EastGravity = 6,
        SouthWestGravity = 7,
        SouthGravity = 8,
        SouthEastGravity = 9,
        StaticGravity = 10
    }

    internal enum XKeySym : uint
    {
        XK_BackSpace = 0xFF08,
        XK_Tab = 0xFF09,
        XK_Clear = 0xFF0B,
        XK_Return = 0xFF0D,
        XK_Home = 0xFF50,
        XK_Left = 0xFF51,
        XK_Up = 0xFF52,
        XK_Right = 0xFF53,
        XK_Down = 0xFF54,
        XK_Page_Up = 0xFF55,
        XK_Page_Down = 0xFF56,
        XK_End = 0xFF57,
        XK_Begin = 0xFF58,
        XK_Menu = 0xFF67,
        XK_Shift_L = 0xFFE1,
        XK_Shift_R = 0xFFE2,
        XK_Control_L = 0xFFE3,
        XK_Control_R = 0xFFE4,
        XK_Caps_Lock = 0xFFE5,
        XK_Shift_Lock = 0xFFE6,
        XK_Meta_L = 0xFFE7,
        XK_Meta_R = 0xFFE8,
        XK_Alt_L = 0xFFE9,
        XK_Alt_R = 0xFFEA,
        XK_Super_L = 0xFFEB,
        XK_Super_R = 0xFFEC,
        XK_Hyper_L = 0xFFED,
        XK_Hyper_R = 0xFFEE,
    }

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

    internal enum GrabMode
    {
        GrabModeSync = 0,
        GrabModeAsync = 1
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XStandardColormap
    {
        internal System.IntPtr colormap;
        internal System.IntPtr red_max;
        internal System.IntPtr red_mult;
        internal System.IntPtr green_max;
        internal System.IntPtr green_mult;
        internal System.IntPtr blue_max;
        internal System.IntPtr blue_mult;
        internal System.IntPtr base_pixel;
        internal System.IntPtr visualid;
        internal System.IntPtr killid;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 2)]
    internal struct XColor
    {
        internal System.IntPtr pixel;
        internal ushort red;
        internal ushort green;
        internal ushort blue;
        internal byte flags;
        internal byte pad;
    }

    internal enum Atom
    {
        AnyPropertyType = 0,
        XA_PRIMARY = 1,
        XA_SECONDARY = 2,
        XA_ARC = 3,
        XA_ATOM = 4,
        XA_BITMAP = 5,
        XA_CARDINAL = 6,
        XA_COLORMAP = 7,
        XA_CURSOR = 8,
        XA_CUT_BUFFER0 = 9,
        XA_CUT_BUFFER1 = 10,
        XA_CUT_BUFFER2 = 11,
        XA_CUT_BUFFER3 = 12,
        XA_CUT_BUFFER4 = 13,
        XA_CUT_BUFFER5 = 14,
        XA_CUT_BUFFER6 = 15,
        XA_CUT_BUFFER7 = 16,
        XA_DRAWABLE = 17,
        XA_FONT = 18,
        XA_INTEGER = 19,
        XA_PIXMAP = 20,
        XA_POINT = 21,
        XA_RECTANGLE = 22,
        XA_RESOURCE_MANAGER = 23,
        XA_RGB_COLOR_MAP = 24,
        XA_RGB_BEST_MAP = 25,
        XA_RGB_BLUE_MAP = 26,
        XA_RGB_DEFAULT_MAP = 27,
        XA_RGB_GRAY_MAP = 28,
        XA_RGB_GREEN_MAP = 29,
        XA_RGB_RED_MAP = 30,
        XA_STRING = 31,
        XA_VISUALID = 32,
        XA_WINDOW = 33,
        XA_WM_COMMAND = 34,
        XA_WM_HINTS = 35,
        XA_WM_CLIENT_MACHINE = 36,
        XA_WM_ICON_NAME = 37,
        XA_WM_ICON_SIZE = 38,
        XA_WM_NAME = 39,
        XA_WM_NORMAL_HINTS = 40,
        XA_WM_SIZE_HINTS = 41,
        XA_WM_ZOOM_HINTS = 42,
        XA_MIN_SPACE = 43,
        XA_NORM_SPACE = 44,
        XA_MAX_SPACE = 45,
        XA_END_SPACE = 46,
        XA_SUPERSCRIPT_X = 47,
        XA_SUPERSCRIPT_Y = 48,
        XA_SUBSCRIPT_X = 49,
        XA_SUBSCRIPT_Y = 50,
        XA_UNDERLINE_POSITION = 51,
        XA_UNDERLINE_THICKNESS = 52,
        XA_STRIKEOUT_ASCENT = 53,
        XA_STRIKEOUT_DESCENT = 54,
        XA_ITALIC_ANGLE = 55,
        XA_X_HEIGHT = 56,
        XA_QUAD_WIDTH = 57,
        XA_WEIGHT = 58,
        XA_POINT_SIZE = 59,
        XA_RESOLUTION = 60,
        XA_COPYRIGHT = 61,
        XA_NOTICE = 62,
        XA_FONT_NAME = 63,
        XA_FAMILY_NAME = 64,
        XA_FULL_NAME = 65,
        XA_CAP_HEIGHT = 66,
        XA_WM_CLASS = 67,
        XA_WM_TRANSIENT_FOR = 68,

        XA_LAST_PREDEFINED = 68
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XScreen
    {
        internal System.IntPtr ext_data;
        internal System.IntPtr display;
        internal System.IntPtr root;
        internal int width;
        internal int height;
        internal int mwidth;
        internal int mheight;
        internal int ndepths;
        internal System.IntPtr depths;
        internal int root_depth;
        internal System.IntPtr root_visual;
        internal System.IntPtr default_gc;
        internal System.IntPtr cmap;
        internal System.IntPtr white_pixel;
        internal System.IntPtr black_pixel;
        internal int max_maps;
        internal int min_maps;
        internal int backing_store;
        internal bool save_unders;
        internal System.IntPtr root_input_mask;
    }

    [System.Flags]
    internal enum ChangeWindowFlags
    {
        CWX = 1 << 0,
        CWY = 1 << 1,
        CWWidth = 1 << 2,
        CWHeight = 1 << 3,
        CWBorderWidth = 1 << 4,
        CWSibling = 1 << 5,
        CWStackMode = 1 << 6
    }

    internal enum StackMode
    {
        Above = 0,
        Below = 1,
        TopIf = 2,
        BottomIf = 3,
        Opposite = 4
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XWindowChanges
    {
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int border_width;
        internal System.IntPtr sibling;
        internal StackMode stack_mode;
    }

    [System.Flags]
    internal enum ColorFlags
    {
        DoRed = 1 << 0,
        DoGreen = 1 << 1,
        DoBlue = 1 << 2
    }

    internal enum NotifyMode
    {
        NotifyNormal = 0,
        NotifyGrab = 1,
        NotifyUngrab = 2
    }

    internal enum NotifyDetail
    {
        NotifyAncestor = 0,
        NotifyVirtual = 1,
        NotifyInferior = 2,
        NotifyNonlinear = 3,
        NotifyNonlinearVirtual = 4,
        NotifyPointer = 5,
        NotifyPointerRoot = 6,
        NotifyDetailNone = 7
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct MotifWmHints
    {
        internal System.IntPtr flags;
        internal System.IntPtr functions;
        internal System.IntPtr decorations;
        internal System.IntPtr input_mode;
        internal System.IntPtr status;

        public override string ToString()
        {
            return string.Format("MotifWmHints <flags={0}, functions={1}, decorations={2}, input_mode={3}, status={4}", (MotifFlags)flags.ToInt32(), (MotifFunctions)functions.ToInt32(), (MotifDecorations)decorations.ToInt32(), (MotifInputMode)input_mode.ToInt32(), status.ToInt32());
        }
    }

    [System.Flags]
    internal enum MotifFlags
    {
        Functions = 1,
        Decorations = 2,
        InputMode = 4,
        Status = 8
    }

    [System.Flags]
    internal enum MotifFunctions
    {
        All = 0x01,
        Resize = 0x02,
        Move = 0x04,
        Minimize = 0x08,
        Maximize = 0x10,
        Close = 0x20
    }

    [System.Flags]
    internal enum MotifDecorations
    {
        All = 0x01,
        Border = 0x02,
        ResizeH = 0x04,
        Title = 0x08,
        Menu = 0x10,
        Minimize = 0x20,
        Maximize = 0x40,

    }

    [System.Flags]
    internal enum MotifInputMode
    {
        Modeless = 0,
        ApplicationModal = 1,
        SystemModal = 2,
        FullApplicationModal = 3
    }

    [System.Flags]
    internal enum KeyMasks
    {
        ShiftMask = (1 << 0),
        LockMask = (1 << 1),
        ControlMask = (1 << 2),
        Mod1Mask = (1 << 3),
        Mod2Mask = (1 << 4),
        Mod3Mask = (1 << 5),
        Mod4Mask = (1 << 6),
        Mod5Mask = (1 << 7),

        ModMasks = Mod1Mask | Mod2Mask | Mod3Mask | Mod4Mask | Mod5Mask
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XModifierKeymap
    {
        public int max_keypermod;
        public System.IntPtr modifiermap;
    }

    internal enum PropertyMode
    {
        Replace = 0,
        Prepend = 1,
        Append = 2
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XKeyBoardState
    {
        public int key_click_percent;
        public int bell_percent;
        public uint bell_pitch, bell_duration;
        public System.IntPtr led_mask;
        public int global_auto_repeat;
        public AutoRepeats auto_repeats;

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
        public struct AutoRepeats
        {
            [System.Runtime.InteropServices.FieldOffset(0)]
            public byte first;

            [System.Runtime.InteropServices.FieldOffset(31)]
            public byte last;
        }
    }

    [System.Flags]
    internal enum GCFunction
    {
        GCFunction = 1 << 0,
        GCPlaneMask = 1 << 1,
        GCForeground = 1 << 2,
        GCBackground = 1 << 3,
        GCLineWidth = 1 << 4,
        GCLineStyle = 1 << 5,
        GCCapStyle = 1 << 6,
        GCJoinStyle = 1 << 7,
        GCFillStyle = 1 << 8,
        GCFillRule = 1 << 9,
        GCTile = 1 << 10,
        GCStipple = 1 << 11,
        GCTileStipXOrigin = 1 << 12,
        GCTileStipYOrigin = 1 << 13,
        GCFont = 1 << 14,
        GCSubwindowMode = 1 << 15,
        GCGraphicsExposures = 1 << 16,
        GCClipXOrigin = 1 << 17,
        GCClipYOrigin = 1 << 18,
        GCClipMask = 1 << 19,
        GCDashOffset = 1 << 20,
        GCDashList = 1 << 21,
        GCArcMode = 1 << 22
    }

    internal enum GCJoinStyle
    {
        JoinMiter = 0,
        JoinRound = 1,
        JoinBevel = 2
    }

    internal enum GCLineStyle
    {
        LineSolid = 0,
        LineOnOffDash = 1,
        LineDoubleDash = 2
    }

    internal enum GCCapStyle
    {
        CapNotLast = 0,
        CapButt = 1,
        CapRound = 2,
        CapProjecting = 3
    }

    internal enum GCFillStyle
    {
        FillSolid = 0,
        FillTiled = 1,
        FillStippled = 2,
        FillOpaqueStppled = 3
    }

    internal enum GCFillRule
    {
        EvenOddRule = 0,
        WindingRule = 1
    }

    internal enum GCArcMode
    {
        ArcChord = 0,
        ArcPieSlice = 1
    }

    internal enum GCSubwindowMode
    {
        ClipByChildren = 0,
        IncludeInferiors = 1
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XGCValues
    {
        internal GXFunction function;
        internal System.IntPtr plane_mask;
        internal System.IntPtr foreground;
        internal System.IntPtr background;
        internal int line_width;
        internal GCLineStyle line_style;
        internal GCCapStyle cap_style;
        internal GCJoinStyle join_style;
        internal GCFillStyle fill_style;
        internal GCFillRule fill_rule;
        internal GCArcMode arc_mode;
        internal System.IntPtr tile;
        internal System.IntPtr stipple;
        internal int ts_x_origin;
        internal int ts_y_origin;
        internal System.IntPtr font;
        internal GCSubwindowMode subwindow_mode;
        internal bool graphics_exposures;
        internal int clip_x_origin;
        internal int clib_y_origin;
        internal System.IntPtr clip_mask;
        internal int dash_offset;
        internal byte dashes;
    }

    internal enum GXFunction
    {
        GXclear = 0x0,		/* 0 */
        GXand = 0x1,		/* src AND dst */
        GXandReverse = 0x2,		/* src AND NOT dst */
        GXcopy = 0x3,		/* src */
        GXandInverted = 0x4,		/* NOT src AND dst */
        GXnoop = 0x5,		/* dst */
        GXxor = 0x6,		/* src XOR dst */
        GXor = 0x7,		/* src OR dst */
        GXnor = 0x8,		/* NOT src AND NOT dst */
        GXequiv = 0x9,		/* NOT src XOR dst */
        GXinvert = 0xa,		/* NOT dst */
        GXorReverse = 0xb,		/* src OR NOT dst */
        GXcopyInverted = 0xc,		/* NOT src */
        GXorInverted = 0xd,		/* NOT src OR dst */
        GXnand = 0xe,		/* NOT src OR NOT dst */
        GXset = 0xf		/* 1 */
    }

    internal enum NetWindowManagerState
    {
        Remove = 0,
        Add = 1,
        Toggle = 2
    }

    internal enum RevertTo
    {
        None = 0,
        PointerRoot = 1,
        Parent = 2
    }

    internal enum MapState
    {
        IsUnmapped = 0,
        IsUnviewable = 1,
        IsViewable = 2
    }

    internal enum CursorFontShape
    {
        XC_X_cursor = 0,
        XC_arrow = 2,
        XC_based_arrow_down = 4,
        XC_based_arrow_up = 6,
        XC_boat = 8,
        XC_bogosity = 10,
        XC_bottom_left_corner = 12,
        XC_bottom_right_corner = 14,
        XC_bottom_side = 16,
        XC_bottom_tee = 18,
        XC_box_spiral = 20,
        XC_center_ptr = 22,

        XC_circle = 24,
        XC_clock = 26,
        XC_coffee_mug = 28,
        XC_cross = 30,
        XC_cross_reverse = 32,
        XC_crosshair = 34,
        XC_diamond_cross = 36,
        XC_dot = 38,
        XC_dotbox = 40,
        XC_double_arrow = 42,
        XC_draft_large = 44,
        XC_draft_small = 46,

        XC_draped_box = 48,
        XC_exchange = 50,
        XC_fleur = 52,
        XC_gobbler = 54,
        XC_gumby = 56,
        XC_hand1 = 58,
        XC_hand2 = 60,
        XC_heart = 62,
        XC_icon = 64,
        XC_iron_cross = 66,
        XC_left_ptr = 68,
        XC_left_side = 70,

        XC_left_tee = 72,
        XC_left_button = 74,
        XC_ll_angle = 76,
        XC_lr_angle = 78,
        XC_man = 80,
        XC_middlebutton = 82,
        XC_mouse = 84,
        XC_pencil = 86,
        XC_pirate = 88,
        XC_plus = 90,
        XC_question_arrow = 92,
        XC_right_ptr = 94,

        XC_right_side = 96,
        XC_right_tee = 98,
        XC_rightbutton = 100,
        XC_rtl_logo = 102,
        XC_sailboat = 104,
        XC_sb_down_arrow = 106,
        XC_sb_h_double_arrow = 108,
        XC_sb_left_arrow = 110,
        XC_sb_right_arrow = 112,
        XC_sb_up_arrow = 114,
        XC_sb_v_double_arrow = 116,
        XC_sb_shuttle = 118,

        XC_sizing = 120,
        XC_spider = 122,
        XC_spraycan = 124,
        XC_star = 126,
        XC_target = 128,
        XC_tcross = 130,
        XC_top_left_arrow = 132,
        XC_top_left_corner = 134,
        XC_top_right_corner = 136,
        XC_top_side = 138,
        XC_top_tee = 140,
        XC_trek = 142,

        XC_ul_angle = 144,
        XC_umbrella = 146,
        XC_ur_angle = 148,
        XC_watch = 150,
        XC_xterm = 152,
        XC_num_glyphs = 154
    }

    internal enum SystrayRequest
    {
        SYSTEM_TRAY_REQUEST_DOCK = 0,
        SYSTEM_TRAY_BEGIN_MESSAGE = 1,
        SYSTEM_TRAY_CANCEL_MESSAGE = 2
    }

    [System.Flags]
    internal enum XSizeHintsFlags
    {
        USPosition = (1 << 0),
        USSize = (1 << 1),
        PPosition = (1 << 2),
        PSize = (1 << 3),
        PMinSize = (1 << 4),
        PMaxSize = (1 << 5),
        PResizeInc = (1 << 6),
        PAspect = (1 << 7),
        PAllHints = (PPosition | PSize | PMinSize | PMaxSize | PResizeInc | PAspect),
        PBaseSize = (1 << 8),
        PWinGravity = (1 << 9),
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XSizeHints
    {
        internal System.IntPtr flags;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int min_width;
        internal int min_height;
        internal int max_width;
        internal int max_height;
        internal int width_inc;
        internal int height_inc;
        internal int min_aspect_x;
        internal int min_aspect_y;
        internal int max_aspect_x;
        internal int max_aspect_y;
        internal int base_width;
        internal int base_height;
        internal int win_gravity;
    }

    [System.Flags]
    internal enum XWMHintsFlags
    {
        InputHint = (1 << 0),
        StateHint = (1 << 1),
        IconPixmapHint = (1 << 2),
        IconWindowHint = (1 << 3),
        IconPositionHint = (1 << 4),
        IconMaskHint = (1 << 5),
        WindowGroupHint = (1 << 6),
        AllHints = (InputHint | StateHint | IconPixmapHint | IconWindowHint | IconPositionHint | IconMaskHint | WindowGroupHint)
    }

    internal enum XInitialState
    {
        DontCareState = 0,
        NormalState = 1,
        ZoomState = 2,
        IconicState = 3,
        InactiveState = 4
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XWMHints
    {
        internal System.IntPtr flags;
        internal bool input;
        internal XInitialState initial_state;
        internal System.IntPtr icon_pixmap;
        internal System.IntPtr icon_window;
        internal int icon_x;
        internal int icon_y;
        internal System.IntPtr icon_mask;
        internal System.IntPtr window_group;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    internal struct XIconSize
    {
        internal int min_width;
        internal int min_height;
        internal int max_width;
        internal int max_height;
        internal int width_inc;
        internal int height_inc;
    }

    internal delegate int XErrorHandler(System.IntPtr DisplayHandle, ref XErrorEvent error_event);

    internal enum XRequest : byte
    {
        X_CreateWindow = 1,
        X_ChangeWindowAttributes = 2,
        X_GetWindowAttributes = 3,
        X_DestroyWindow = 4,
        X_DestroySubwindows = 5,
        X_ChangeSaveSet = 6,
        X_ReparentWindow = 7,
        X_MapWindow = 8,
        X_MapSubwindows = 9,
        X_UnmapWindow = 10,
        X_UnmapSubwindows = 11,
        X_ConfigureWindow = 12,
        X_CirculateWindow = 13,
        X_GetGeometry = 14,
        X_QueryTree = 15,
        X_InternAtom = 16,
        X_GetAtomName = 17,
        X_ChangeProperty = 18,
        X_DeleteProperty = 19,
        X_GetProperty = 20,
        X_ListProperties = 21,
        X_SetSelectionOwner = 22,
        X_GetSelectionOwner = 23,
        X_ConvertSelection = 24,
        X_SendEvent = 25,
        X_GrabPointer = 26,
        X_UngrabPointer = 27,
        X_GrabButton = 28,
        X_UngrabButton = 29,
        X_ChangeActivePointerGrab = 30,
        X_GrabKeyboard = 31,
        X_UngrabKeyboard = 32,
        X_GrabKey = 33,
        X_UngrabKey = 34,
        X_AllowEvents = 35,
        X_GrabServer = 36,
        X_UngrabServer = 37,
        X_QueryPointer = 38,
        X_GetMotionEvents = 39,
        X_TranslateCoords = 40,
        X_WarpPointer = 41,
        X_SetInputFocus = 42,
        X_GetInputFocus = 43,
        X_QueryKeymap = 44,
        X_OpenFont = 45,
        X_CloseFont = 46,
        X_QueryFont = 47,
        X_QueryTextExtents = 48,
        X_ListFonts = 49,
        X_ListFontsWithInfo = 50,
        X_SetFontPath = 51,
        X_GetFontPath = 52,
        X_CreatePixmap = 53,
        X_FreePixmap = 54,
        X_CreateGC = 55,
        X_ChangeGC = 56,
        X_CopyGC = 57,
        X_SetDashes = 58,
        X_SetClipRectangles = 59,
        X_FreeGC = 60,
        X_ClearArea = 61,
        X_CopyArea = 62,
        X_CopyPlane = 63,
        X_PolyPoint = 64,
        X_PolyLine = 65,
        X_PolySegment = 66,
        X_PolyRectangle = 67,
        X_PolyArc = 68,
        X_FillPoly = 69,
        X_PolyFillRectangle = 70,
        X_PolyFillArc = 71,
        X_PutImage = 72,
        X_GetImage = 73,
        X_PolyText8 = 74,
        X_PolyText16 = 75,
        X_ImageText8 = 76,
        X_ImageText16 = 77,
        X_CreateColormap = 78,
        X_FreeColormap = 79,
        X_CopyColormapAndFree = 80,
        X_InstallColormap = 81,
        X_UninstallColormap = 82,
        X_ListInstalledColormaps = 83,
        X_AllocColor = 84,
        X_AllocNamedColor = 85,
        X_AllocColorCells = 86,
        X_AllocColorPlanes = 87,
        X_FreeColors = 88,
        X_StoreColors = 89,
        X_StoreNamedColor = 90,
        X_QueryColors = 91,
        X_LookupColor = 92,
        X_CreateCursor = 93,
        X_CreateGlyphCursor = 94,
        X_FreeCursor = 95,
        X_RecolorCursor = 96,
        X_QueryBestSize = 97,
        X_QueryExtension = 98,
        X_ListExtensions = 99,
        X_ChangeKeyboardMapping = 100,
        X_GetKeyboardMapping = 101,
        X_ChangeKeyboardControl = 102,
        X_GetKeyboardControl = 103,
        X_Bell = 104,
        X_ChangePointerControl = 105,
        X_GetPointerControl = 106,
        X_SetScreenSaver = 107,
        X_GetScreenSaver = 108,
        X_ChangeHosts = 109,
        X_ListHosts = 110,
        X_SetAccessControl = 111,
        X_SetCloseDownMode = 112,
        X_KillClient = 113,
        X_RotateProperties = 114,
        X_ForceScreenSaver = 115,
        X_SetPointerMapping = 116,
        X_GetPointerMapping = 117,
        X_SetModifierMapping = 118,
        X_GetModifierMapping = 119,
        X_NoOperation = 127
    }

    [System.Flags]
    internal enum XIMProperties
    {
        XIMPreeditArea = 0x0001,
        XIMPreeditCallbacks = 0x0002,
        XIMPreeditPosition = 0x0004,
        XIMPreeditNothing = 0x0008,
        XIMPreeditNone = 0x0010,
        XIMStatusArea = 0x0100,
        XIMStatusCallbacks = 0x0200,
        XIMStatusNothing = 0x0400,
        XIMStatusNone = 0x0800,
    }

    [System.Flags]
    internal enum WindowType
    {
        Client = 1,
        Whole = 2,
        Both = 3
    }

    internal enum XEmbedMessage
    {
        EmbeddedNotify = 0,
        WindowActivate = 1,
        WindowDeactivate = 2,
        RequestFocus = 3,
        FocusIn = 4,
        FocusOut = 5,
        FocusNext = 6,
        FocusPrev = 7,
        /* 8-9 were used for XEMBED_GRAB_KEY/XEMBED_UNGRAB_KEY */
        ModalityOn = 10,
        ModalityOff = 11,
        RegisterAccelerator = 12,
        UnregisterAccelerator = 13,
        ActivateAccelerator = 14
    }
}