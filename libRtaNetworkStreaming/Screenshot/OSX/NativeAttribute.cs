
namespace ObjCRuntime
{

    // https://github.com/xamarin/xamarin-macios/blob/master/src/ObjCRuntime/NativeAttribute.cs

    [System.AttributeUsage(System.AttributeTargets.Enum)]
    public sealed class NativeAttribute 
        : System.Attribute
    {


        public NativeAttribute()
        { }

        // use in case where the managed name is different from the native name
        // Extrospection tests will use this to find the matching type to compare
        public NativeAttribute(string name)
        {
            NativeName = name;
        }

        public string NativeName { get; set; }
    }
}
