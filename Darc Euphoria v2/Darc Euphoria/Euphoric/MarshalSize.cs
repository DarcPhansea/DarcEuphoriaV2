using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric
{
    public static class MarshalSize<T>
    {
        static MarshalSize()
        {
            Size = Marshal.SizeOf(typeof(T));
        }

        public static int Size { get; }
    }
}