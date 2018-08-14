using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Darc_Euphoria.Euphoric
{
    public static class Structs
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CUserCmd
        {
            public int command_number;//0x4
            public int tick_count;//0x8
            public Vector3 viewangles;//0xC
            public Vector3 aimdirection;//0x18
            public float forwardmove;//0x1C
            public float sidemove;//0x20
            public float upmove;//0x24
            public int buttons;//0x28
            public char impulse;//0x2C
            public int weaponselect;//0x2E
            public int weaponsubtype;//0x32
            public int random_seed;//0x36
            public short mousedx;//2
            public short mousedy;//2
            [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
            public bool hasbeenpredicted;//1
            public Vector3 headangles;//12
            public Vector3 headoffset;//12
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x2)]
            public char[] unknown;
        };

        public struct Matrix4x4
        {
            public float m11;
            public float m12;
            public float m13;
            public float m14;
            public float m21;
            public float m22;
            public float m23;
            public float m24;
            public float m31;
            public float m32;
            public float m33;
            public float m34;
            public float m41;
            public float m42;
            public float m43;
            public float m44;
        }

        public struct DrawArea
        {
            public float x, y, height, width;
        }

        public struct Rect
        {
            public int Left, Top, Right, Bottom;
        }


        public struct GlowColor
        {
            public float r;
            public float g;
            public float b;
            public float a;
            public GlowColor(float __r, float __g, float __b, float __a)
            {
                r = __r;
                g = __g;
                b = __b;
                a = __a;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GlowSettings
        {
            byte m_renderWhenOccluded;
            byte m_renderWhenUnoccluded;
            byte m_fullBloomRender;

            public GlowSettings(bool __renderWhenOccluded, bool __renderWhenUnoccluded, bool __fullBloom)
            {
                m_renderWhenOccluded = __renderWhenOccluded == true ? (byte)1 : (byte)0;
                m_renderWhenUnoccluded = __renderWhenUnoccluded == true ? (byte)1 : (byte)0;
                m_fullBloomRender = __fullBloom == true ? (byte)1 : (byte)0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        unsafe public struct GlobalVarBase
        {
            public float realtime;
            public int framecount;
            public float absolute_frametime;
            public float absolute_framestarttimestddev;
            public float curtime;
            public float frameTime;
            public int maxClients;
            public int tickcount;
            public float interval_per_tick;
            public float interpolation_amount;
            public int simThicksThisFrame;
            public int network_protocol;
        };

        public struct RenderColor
        {
            public byte R;
            public byte G;
            public byte B;
            public byte A;

            public RenderColor(byte r, byte g, byte b, byte a)
            {
                B = b;
                R = r;
                A = a;
                G = g;
            }
        }

        public struct Margins
        {
            public int Left, Right, Top, Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Vector3
        {
            public float x;
            public float y;
            public float z;

            public Vector3(float _x, float _y, float _z)
            {
                x = _x;
                y = _y;
                z = _z;
            }

            public Vector3(Vector2 vec, float _z = 0)
            {
                x = vec.x;
                y = vec.y;
                z = _z;
            }

            public static Vector3 Zero
            {
                get
                {
                    return new Vector3(0, 0, 0);
                }
            }

            public override string ToString()
            {
                return String.Format("{0}, {1}, {2}", x, y, z);
            }

            public float Dot(Vector3 right)
            {
                return (x * right.x) + (y * right.y) + (z * right.z);
            }

            public static float Dot(Vector3 left, Vector3 right)
            {
                return (left.x * right.x) + (left.y * right.y) + (left.z * right.z);
            }

            public static Vector3 operator -(Vector3 a, Vector3 b)
            {
                return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
            }
            public static Vector3 operator +(Vector3 a, Vector3 b)
            {
                return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
            }
            public static Vector3 operator *(Vector3 a, Vector3 b)
            {
                return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
            }
            public static Vector3 operator /(Vector3 a, Vector3 b)
            {
                return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
            }

            public float Length
            {
                get
                {
                    return (float)Math.Sqrt((x * x) + (y * y) + (z * z));
                }
            }

            public static Vector3 operator *(Vector3 a, float b)
            {
                return new Vector3(a.x * b, a.y * b, a.z * b);
            }
            public static Vector3 operator /(Vector3 a, float b)
            {
                return new Vector3(a.x / b, a.y / b, a.z / b);
            }

            public static Vector3 operator +(Vector3 a, float b)
            {
                return new Vector3(a.x + b, a.y + b, a.z + b);
            }
            public static Vector3 operator -(Vector3 a, float b)
            {
                return new Vector3(a.x - b, a.y - b, a.z - b);
            }

            public static bool operator ==(Vector3 v1, Vector3 v2)
            {
                return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
            }
            public static bool operator !=(Vector3 v1, Vector3 v2)
            {
                return !(v1 == v2);
            }

            public void Normalize()
            {
                float len = Length;
                if (len != 0f)
                {
                    x /= len;
                    y /= len;
                    z /= len;
                }
            }

            public float this[int i]
            {
                get
                {
                    switch (i)
                    {
                        case 0:
                            return this.x;
                        case 1:
                            return this.y;
                        case 2:
                            return this.z;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
                set
                {
                    switch (i)
                    {
                        case 0:
                            this.x = value;
                            break;
                        case 1:
                            this.y = value;
                            break;
                        case 2:
                            this.z = value;
                            break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
            }
        }

        public static bool Equals(this Vector2 a, Vector2 b)
        {
            if (a.x == b.x && a.y == b.y)
                return true;
            else
                return false;
        }

        public static bool Equals(this Vector3 a, Vector3 b)
        {
            if (a.x == b.x && a.y == b.y && a.z == b.z)
                return true;
            else
                return false;
        }

        public static bool Equals(this Vector3 a, float b)
        {
            if (a.x == b && a.y == b && a.z == b)
                return true;
            else
                return false;
        }

        public static bool Equals(this Vector2 a, float b)
        {
            if (a.x == b && a.y == b)
                return true;
            else
                return false;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2
        {
            

            public float y;
            public float x;

            public Vector2(float _x, float _y)
            {
                x = _x;
                y = _y;
            }

            public override string ToString()
            {
                return String.Format("{0}, {1}", x, y);
            }

            public static Vector2 operator -(Vector2 a, Vector2 b)
            {
                return new Vector2(a.x - b.x, a.y - b.y);
            }
            public static Vector2 operator +(Vector2 a, Vector2 b)
            {
                return new Vector2(a.x + b.x, a.y + b.y);
            }
            public static Vector2 operator *(Vector2 a, Vector2 b)
            {
                return new Vector2(a.x * b.x, a.y * b.y);
            }
            public static Vector2 operator /(Vector2 a, Vector2 b)
            {
                return new Vector2(a.x / b.x, a.y / b.y);
            }

            


            public float Length()
            {
                return (float)Math.Sqrt((x * x) + (y * y));
            }

            public static Vector2 operator *(Vector2 a, float b)
            {
                return new Vector2(a.x * b, a.y * b);
            }
            public static Vector2 operator /(Vector2 a, float b)
            {
                return new Vector2(a.x / b, a.y / b);
            }
            public static Vector2 operator +(Vector2 a, float b)
            {
                return new Vector2(a.x + b, a.y + b);
            }
            public static Vector2 operator -(Vector2 a, float b)
            {
                return new Vector2(a.x - b, a.y - b);
            }

        }

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
                var patternBlocks = _signature.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var pattern = new byte[patternBlocks.Length];

                for (int i = 0; i < patternBlocks.Length; i++)
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
}
