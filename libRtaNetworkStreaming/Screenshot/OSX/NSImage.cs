
namespace AppKit
{

    // https://github.com/xamarin/xamarin-macios/blob/master/src/AppKit/NSImage.cs
    public class NSImage // AppKit 
        : System.IDisposable
    {
        public NSImage(CoreGraphics.CGImage image, System.Drawing.SizeF size)
        { }

        void System.IDisposable.Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
