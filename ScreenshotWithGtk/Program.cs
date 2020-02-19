
using Gdk;
using Gtk;
using GLib;
using Pango;
using PangoSharp;
using GLibSharp;
using GtkSharp;


namespace ScreenshotWithGtk
{
    
    
    
    class Program
    {
        
        
        public static void TestGDK()
        {
            // https://github.com/GtkSharp/GtkSharp
            Gdk.Window window = Gdk.Global.DefaultRootWindow;
            if (window != null)
            {
                // Gdk.Pixbuf pixBuf = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8,
                //     window.Screen.Width, window.Screen.Height);

                // Gdk.CursorType.Arrow
                // Gdk.Display.Default.GetPointer();
                // Gdk.Cursor.GetObject().
                // Gdk.Display.Default.GetMonitor(0).Geometry.Bottom
                // Gdk.Display.Default.GetMonitor(0).IsPrimary
                // Gdk.Display.Default.NMonitors
                // Gdk.Display.Default.GetMonitorAtPoint()
                // Gdk.Display.Default.GetPointer();


                // pixBuf.dr
                // pixBuf.GetPixelsWithLength()
                // Gdk.Pixbuf buf;

                // pixBuf.GetFromDrawable(window, Gdk.Colormap.System, 0, 0, 0, 0, window.Screen.Width, window.Screen.Height);          
                // pixBuf.ScaleSimple(400, 300, Gdk.InterpType.Bilinear);
                // pixBuf.Save("screenshot0.jpeg", "jpeg");
            } // End if (window!=null)

        } // End Sub TestGDK()
        
        
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
        }
    }
}