 
namespace rtaNetworking.Linux
{


    public class Gdip
    {
        public static System.IntPtr Display;
    }


    public class LinScreen
    {
        
        public static readonly System.IntPtr VisualIDMask = new System.IntPtr(0x01);
        
        public const int VisualNoMask = 0x0;
        // public const int VisualIDMask = 0x1;
        public const int VisualScreenMask = 0x2;
        public const int VisualDepthMask = 0x4;
        public const int VisualClassMask = 0x8;
        public const int VisualRedMaskMask = 0x10;
        public const int VisualGreenMaskMask = 0x20;
        public const int VisualBlueMaskMask = 0x40;
        public const int VisualColormapSizeMask = 0x80;
        public const int VisualBitsPerRGBMask = 0x100;
        public const int VisualAllMask = 0x1FF;


        public const int XYBitmap = 0;	// depth 1, XYFormat 
        public const int XYPixmap = 1;	// depth == drawable depth 
        public const int ZPixmap = 2;   // depth == drawable depth 


        private static System.Drawing.Size screenSize = GetXorgScreenSize();


        public static System.Drawing.Size GetXorgScreenSize()
        {
            int screen_width = 0;
            int screen_height = 0;
            
            System.IntPtr display = LibX11Functions.XOpenDisplay(System.IntPtr.Zero);

            if (display == System.IntPtr.Zero)
            {
                System.Console.WriteLine("Error: Failed on XOpenDisplay.\n");
            }
            else
            {
                int defaultScreen = LibX11Functions.XDefaultScreen(display);

                screen_width = LibX11Functions.DisplayWidth(display, defaultScreen);
                screen_height = LibX11Functions.DisplayHeight(display, defaultScreen);

                LibX11Functions.XCloseDisplay(display);
                System.Console.WriteLine("Width: " + screen_width.ToString() + " Height: " + screen_height.ToString());
            } // End Else (display == System.IntPtr.Zero)

            return new System.Drawing.Size(screen_width, screen_height);
        } // End Function GetXorgScreen



        public static System.Drawing.Bitmap CopyFromScreenX11()
        {
            // System.Drawing.Size screenSize = rtaStreamingServer.LinuxScreenShot.GetXorgScreenSize();
            return CopyFromScreenX11(0, 0, screenSize, System.Drawing.CopyPixelOperation.SourceCopy);
        }
        
        
        public static System.Drawing.Bitmap CopyFromScreenX11(int sourceX, int sourceY, 
            System.Drawing.Size blockRegionSize
            , System.Drawing.CopyPixelOperation copyPixelOperation)
        {
            System.UIntPtr window;
            System.IntPtr image, defvisual, vPtr;
            int AllPlanes = ~0, nitems = 0, pixel;
            System.UIntPtr AllPlanes2 = new System.UIntPtr((uint)AllPlanes);


            if (copyPixelOperation != System.Drawing.CopyPixelOperation.SourceCopy)
                throw new System.NotImplementedException("Operation not implemented under X11");

            if (Gdip.Display == System.IntPtr.Zero)
            {
                Gdip.Display = LibX11Functions.XOpenDisplay(System.IntPtr.Zero);
            }

            window = LibX11Functions.XRootWindow(Gdip.Display, 0);
            defvisual = LibX11Functions.XDefaultVisual(Gdip.Display, 0);
            XVisualInfo visual = new XVisualInfo();
            
            // Get XVisualInfo for this visual
            visual.visualid = LibX11Functions.XVisualIDFromVisual(defvisual);
            vPtr = LibX11Functions.XGetVisualInfo(Gdip.Display, VisualIDMask, ref visual, ref nitems);
            visual = (XVisualInfo) System.Runtime.InteropServices.Marshal.PtrToStructure(vPtr, typeof(XVisualInfo));
            image = LibX11Functions.XGetImage(Gdip.Display, window, sourceX, sourceY, (uint) blockRegionSize.Width,
               (uint) blockRegionSize.Height, AllPlanes2, ZPixmap);
            if (image == System.IntPtr.Zero)
            {
                string s = string.Format("XGetImage returned NULL when asked to for a {0}x{1} region block",
                    blockRegionSize.Width, blockRegionSize.Height);
                throw new System.InvalidOperationException(s);
            }
            
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(blockRegionSize.Width, blockRegionSize.Height);
            int red, blue, green;
            int red_mask = (int) visual.red_mask;
            int blue_mask = (int) visual.blue_mask;
            int green_mask = (int) visual.green_mask;
            for (int y = 0; y < blockRegionSize.Height; y++)
            {
                for (int x = 0; x < blockRegionSize.Width; x++)
                {
                    pixel = LibX11Functions.XGetPixel(image, x, y);
                    
                    switch (visual.depth)
                    {
                        case 16: /* 16bbp pixel transformation */
                            red = (int) ((pixel & red_mask) >> 8) & 0xff;
                            green = (int) (((pixel & green_mask) >> 3)) & 0xff;
                            blue = (int) ((pixel & blue_mask) << 3) & 0xff;
                            break;
                        case 24:
                        case 32:
                            // int a = (int)((pixel ) >> 24) & 0xff;
                            red = (int) ((pixel & red_mask) >> 16) & 0xff;
                            green = (int) (((pixel & green_mask) >> 8)) & 0xff;
                            blue = (int) ((pixel & blue_mask)) & 0xff;
                            break;
                        default:
                            string text = string.Format("{0}bbp depth not supported.", visual.depth);
                            throw new System.NotImplementedException(text);
                    }

                    bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(255, red, green, blue));
                }
            }
            
            // DrawImage(bmp, destinationX, destinationY);
            // bmp.Dispose();
            LibX11Functions.XDestroyImage(image);
            LibX11Functions.XFree(vPtr);
            
            return bmp;
        }
        
        
    }
    
    
}
