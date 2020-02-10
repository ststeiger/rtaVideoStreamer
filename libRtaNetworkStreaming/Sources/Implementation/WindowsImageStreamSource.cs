
namespace rtaNetworking
{


    public class WindowsImageStreamSource
        : ImageStreamSource
    {


        // public static System.Collections.Generic.IEnumerable<System.Drawing.Image> Snapshots()
        public WindowsImageStreamSource()
        {
            this.m_imageSource = rtaNetworking.Windows.WindowsScreenshotWithCursor.Snapshots();
        }


    }


}
