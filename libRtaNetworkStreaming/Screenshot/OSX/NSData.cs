
namespace Foundation
{


    // https://github.com/xamarin/xamarin-macios/blob/master/src/Foundation/NSData.cs

    public partial class NSData
        : System.Collections.IEnumerable, System.Collections.Generic.IEnumerable<byte>
    {


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }


        System.Collections.Generic.IEnumerator<byte> System.Collections.Generic.IEnumerable<byte>.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }


        // [Export("length")]
        // nuint Length { get; [NotImplemented("Not available on NSData, only available on NSMutableData")] set; }

        public long Length;

        // https://github.com/xamarin/xamarin-macios/blob/master/src/foundation.cs
        // interface NSData 

        // [Export("getBytes:length:")]
        public void GetBytes(System.IntPtr buffer, System.IntPtr length)
        { }


    }


}
