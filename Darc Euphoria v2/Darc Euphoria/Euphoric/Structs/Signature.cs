using System;
using System.Globalization;

namespace Darc_Euphoria.Euphoric.Structs
{
    public struct Signature
    {
        public readonly int Offset;
        public readonly byte[] ByteArray;
        public readonly IntPtr Address;
        public readonly string Mask;

        public Signature(byte[] _byteArray, string _mask, int _offset = 0)
        {
            ByteArray = _byteArray;
            Mask = _mask;
            Offset = _offset;
            Address = IntPtr.Zero;
        }

        public Signature(IntPtr _address)
        {
            ByteArray = null;
            Offset = 0;
            Address = _address;
            Mask = string.Empty;
        }

        public Signature(string _signature, int _offset = 0)
        {
            var _mask = string.Empty;
            var patternBlocks = _signature.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var pattern = new byte[patternBlocks.Length];

            for (var i = 0; i < patternBlocks.Length; i++)
            {
                var block = patternBlocks[i];

                if (block == "?")
                {
                    _mask += block;
                    pattern[i] = 0;
                }
                else
                {
                    _mask += "x";
                    if (!byte.TryParse(patternBlocks[i], NumberStyles.HexNumber,
                        CultureInfo.DefaultThreadCurrentCulture, out pattern[i]))
                        throw new Exception("Signature Parsing Error");
                }
            }

            ByteArray = pattern;
            Offset = _offset;
            Address = IntPtr.Zero;
            Mask = _mask;
        }
    }
}