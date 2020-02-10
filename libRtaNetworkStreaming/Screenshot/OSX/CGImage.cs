
namespace CoreGraphics
{
    // https://www.nuget.org/packages/CocoaSharp.CoreGraphics/

    // https://github.com/xamarin/xamarin-macios/blob/master/src/CoreGraphics/CGImage.cs
    public class CGImage // CoreGraphics
        : System.IDisposable
    {

        public CGImage(System.IntPtr hDisplayId)
        {

        }

        public float Width;
        public float Height;


        void System.IDisposable.Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
