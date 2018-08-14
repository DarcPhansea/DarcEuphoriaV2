using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric
{
    public static class MarshalSize<T>
    {
        public static int Size { get; private set; }

        static MarshalSize()
        {
            Size = Marshal.SizeOf(typeof(T));
        }
    }
}
