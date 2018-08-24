using System;
using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric.Structs
{
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

        public static Vector3 Zero => new Vector3(0, 0, 0);

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", x, y, z);
        }

        public float Dot(Vector3 right)
        {
            return x * right.x + y * right.y + z * right.z;
        }

        public static float Dot(Vector3 left, Vector3 right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z;
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

        public float Length => (float) Math.Sqrt(x * x + y * y + z * z);

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
            var len = Length;
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
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}