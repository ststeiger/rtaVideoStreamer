
namespace rtaNetworking.Windows
{
    
    
    public static class WindowsScreenshotWithCursor
    {
        
        
        
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public System.Int32 cbSize;
            public System.Int32 flags;
            public System.IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        struct POINTAPI
        {
            public int x;
            public int y;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool DrawIcon(System.IntPtr hDC, int X, int Y, System.IntPtr hIcon);

        const System.Int32 CURSOR_SHOWING = 0x00000001;

        public static System.Drawing.Bitmap CaptureScreen(rtaNetworking.Windows.Screen thisScreen, bool CaptureMouse)
        {
            System.Drawing.Bitmap result = new System.Drawing.Bitmap(thisScreen.Bounds.Width
                , rtaNetworking.Windows.Screen.PrimaryScreen.Bounds.Height
                , System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            try
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(result))
                {
                    g.CopyFromScreen(0, 0, 0, 0, thisScreen.Bounds.Size, System.Drawing.CopyPixelOperation.SourceCopy);

                    if (CaptureMouse)
                    {
                        CURSORINFO pci;
                        pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

                        if (GetCursorInfo(out pci))
                        {
                            if (pci.flags == CURSOR_SHOWING)
                            {
                                DrawIcon(g.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
                                g.ReleaseHdc();
                            }
                        }
                    }
                }
            }
            catch
            {
                result = null;
            }

            return result;
        } // End Function CaptureScreen 




        /// <summary>
        /// Returns a 
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<System.Drawing.Image> Snapshots(int width, int height, bool showCursor)
        {
            // rtaNetworking.Windows.Screen thisScreen = rtaNetworking.Windows.Screen.AllScreens[1];
            rtaNetworking.Windows.Screen thisScreen = rtaNetworking.Windows.Screen.PrimaryScreen;

            System.Drawing.Size size = new System.Drawing.Size(rtaNetworking.Windows.Screen.PrimaryScreen.Bounds.Width
                , rtaNetworking.Windows.Screen.PrimaryScreen.Bounds.Height);

            System.Drawing.Bitmap srcImage = new System.Drawing.Bitmap(size.Width, size.Height);
            System.Drawing.Graphics srcGraphics = System.Drawing.Graphics.FromImage(srcImage);

            bool scaled = (width != size.Width || height != size.Height);

            System.Drawing.Bitmap dstImage = srcImage;
            System.Drawing.Graphics dstGraphics = srcGraphics;

            if (scaled)
            {
                dstImage = new System.Drawing.Bitmap(width, height);
                dstGraphics = System.Drawing.Graphics.FromImage(dstImage);
            } // End if (scaled) 

            System.Drawing.Rectangle src = new System.Drawing.Rectangle(0, 0, size.Width, size.Height);
            System.Drawing.Rectangle dst = new System.Drawing.Rectangle(0, 0, width, height);
            System.Drawing.Size curSize = new System.Drawing.Size(32, 32);

            while (true)
            {   
                //srcGraphics.CopyFromScreen(0, 0, 0, 0, size);
                srcGraphics.CopyFromScreen(
                      thisScreen.WorkingArea.Left
                    , thisScreen.WorkingArea.Top // Top is bottom...
                    , 0, 0, size
                );

                
                /*
                // This results in the wrong cursor...
                if (showCursor)
                    // System.Windows.Forms.Cursors.Default.Draw(srcGraphics,
                    System.Windows.Forms.Cursor.Current.Draw(srcGraphics,
                        new System.Drawing.Rectangle(System.Windows.Forms.Cursor.Position, curSize)
                );
                */

                
                // https://stackoverflow.com/questions/6750056/how-to-capture-the-screen-and-mouse-pointer-using-windows-apis
                if (showCursor)
                {
                    CURSORINFO pci;
                    pci.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(CURSORINFO));

                    if (GetCursorInfo(out pci))
                    {
                        if (pci.flags == CURSOR_SHOWING)
                        {

                            // Check if cursor on the screen that is being captured...
                            if (pci.ptScreenPos.x >= thisScreen.WorkingArea.Left && pci.ptScreenPos.x <= thisScreen.WorkingArea.Right)
                            {
                                DrawIcon(srcGraphics.GetHdc(), pci.ptScreenPos.x - thisScreen.WorkingArea.Left, pci.ptScreenPos.y, pci.hCursor);
                                srcGraphics.ReleaseHdc();
                            } // End if (pci.ptScreenPos.x >= thisScreen.Bounds.X && pci.ptScreenPos.x <= thisScreen.Bounds.X + thisScreen.Bounds.Width) 

                        } // End if (pci.flags == CURSOR_SHOWING) 
                    } // End if (GetCursorInfo(out pci)) 
                } // End if (showCursor) 
                
                if (scaled)
                    dstGraphics.DrawImage(srcImage, dst, src, System.Drawing.GraphicsUnit.Pixel);
                
                yield return dstImage;
            } // Whend 
            
            srcGraphics.Dispose();
            dstGraphics.Dispose();
            
            srcImage.Dispose();
            dstImage.Dispose();
            
            yield break;
        } // End Function Snapshots 
        
        
        public static System.Collections.Generic.IEnumerable<System.Drawing.Image> Snapshots()
        {
            return Snapshots(rtaNetworking.Windows.Screen.PrimaryScreen.Bounds.Width, rtaNetworking.Windows.Screen.PrimaryScreen.Bounds.Height, true);
        }
        
        
        internal static System.Collections.Generic.IEnumerable<System.IO.MemoryStream> Streams(this System.Collections.Generic.IEnumerable<System.Drawing.Image> source)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            foreach (System.Drawing.Image img in source)
            {
                ms.SetLength(0);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                yield return ms;
            } // Next img 
            
            ms.Close();
            ms = null;
            
            yield break;
        } // End Function Streams 
        
        
    } //End  Class OrigScreen 
    
    
} // End Namespace libRtaNetworkStreaming 
