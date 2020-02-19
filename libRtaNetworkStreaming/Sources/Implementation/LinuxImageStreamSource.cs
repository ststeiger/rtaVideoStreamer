
namespace rtaNetworking
{


    public class LinuxImageStreamSource
        : ImageStreamSource
    {


        // public static System.Collections.Generic.IEnumerable<System.Drawing.Image> Snapshots()
        public LinuxImageStreamSource()
        {
            this.m_bufferSource = Snapshots();
        }


        /// <summary>
        /// Returns a 
        /// </summary>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        public static System.Collections.Generic.IEnumerable<byte[]> Snapshots(int width, int height, bool showCursor)
        {
            while (true)
            {
                // System.Drawing.Bitmap dstImage = rtaStreamingServer.LinuxScreenShot.GetScreenshot();
                byte[] dstImage = rtaNetworking.Linux.SafeX11.X11Screenshot(true);
                yield return dstImage;
            }

        }


        public static System.Collections.Generic.IEnumerable<byte[]> Snapshots()
        {
            // return Snapshots(rtaNetworking.Windows.Screen.PrimaryScreen.Bounds.Width, rtaNetworking.Windows.Screen.PrimaryScreen.Bounds.Height, true);
            return Snapshots(-1, -1, true);
        }


    }


}
