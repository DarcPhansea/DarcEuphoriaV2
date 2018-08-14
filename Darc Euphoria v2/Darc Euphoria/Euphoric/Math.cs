using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Darc_Euphoria.Euphoric.Structs;
using SharpDX.Mathematics.Interop;
using SharpDX.DirectWrite;
using System.Drawing;
using Darc_Euphoria.Hacks;
using Darc_Euphoria.Euphoric.Objects;

namespace Darc_Euphoria.Euphoric
{
    public static class MathFuncs
    {
        public static Vector2 CalcAngle(Vector3 src, Vector3 dist)
        {
            Vector3 delta = new Vector3()
            {
                x = dist.x - src.x,
                y = dist.y - src.y,
                z = dist.z - src.z,
            };

            float magn = (float)Math.Sqrt(
                (delta.x * delta.x) +
                (delta.y * delta.y) +
                (delta.z * delta.z)
            );

            Vector2 returnAngle = new Vector2()
            {
                x = (float)(Math.Atan2(delta.y, delta.x) * (180f / 3.14f)),
                y = (float)(-Math.Atan2(delta.z, magn) * (180f / 3.14f)),
            };

            return returnAngle;
        }

        public static double CalcFov(Vector2 src, Vector2 dist)
        {
            double i = Math.Sqrt(
                ((dist.x - src.x) * (dist.x - src.x)) +
                ((dist.y - src.y) * (dist.y - src.y))
            );
            return i;
        }

        public static string GetRankName(this int id)
        {
            switch (id)
            {
                case 0:
                    return "Unranked";
                case 1:
                    return "Silver 1";
                case 2:
                    return "Silver 2";
                case 3:
                    return "Silver 3";
                case 4:
                    return "Silver 4";
                case 5:
                    return "Silver Elite";
                case 6:
                    return "Silver Elite Master";
                case 7:
                    return "Gold Nova 1";
                case 8:
                    return "Gold Nova 2";
                case 9:
                    return "Gold Nova 3";
                case 10:
                    return "Gold Nova Master";
                case 11:
                    return "Master Guardian 1";
                case 12:
                    return "Master Guardian 2";
                case 13:
                    return "Master Guardian Elite";
                case 14:
                    return "Distinguished Master Guardian";
                case 15:
                    return "Legendary Eagle";
                case 16:
                    return "Legendary Eagle Master";
                case 17:
                    return "Supreme Master First Class";
                case 18:
                    return "Global Elite";
            }

            return string.Empty;
        }

        public static float VectorDistance(Vector3 src, Vector3 dist, bool noZ = false)
        {
            if (!noZ)
            {
                double distance = Math.Sqrt(
                ((dist.x - src.x) * (dist.x - src.x)) +
                ((dist.y - src.y) * (dist.y - src.y)) +
                ((dist.z - src.z) * (dist.z - src.z))
                );
                distance = Math.Round(distance, 4);
                return (float)distance;
            }
            else
            {
                double distance = Math.Sqrt(
                   ((dist.x - src.x) * (dist.x - src.x)) +
                   ((dist.y - src.y) * (dist.y - src.y))
                   );

                distance = Math.Round(distance, 4);
                return (float)distance;
            }
        }

        public static float VectorDistance(Vector2 src, Vector2 dist)
        {
            double distance = Math.Sqrt(
                (dist.x - src.x) * (dist.x - src.x) +
                (dist.y - src.y) * (dist.y - src.y)
             );

            distance = Math.Round(distance, 4);
            return (float)distance;
        }

        public static Vector2 NormalizeAngle(this Vector2 angle)
        {
            while (0f > angle.x || angle.x > 360f)
            {
                if (angle.x < 0f) angle.x += 360.0f;
                if (angle.x > 360f) angle.x -= 360.0f;
            }
            return angle;
        }

        public static Vector2 ClampAngle(this Vector2 angle)
        {
            while (angle.x > 180f)
                angle.x -= 360f;

            while (angle.x < -180f)
                angle.x += 360f;

            while (angle.y > 89f)
                angle.y -= (angle.y - 89) * 2;

            while (angle.y < -89f)
                angle.y += (-89 - angle.y) * 2;


            if (angle.x > 180f)
                return Local.ViewAngle;

            if (angle.x < -180f)
                return Local.ViewAngle; //This is to FORCE clamped angles 
                                        //even if i failed it somehow
            if (angle.y > 89f)
                return Local.ViewAngle;

            if (angle.y < -89f)
                return Local.ViewAngle;


            return angle;
        }

        public static Vector3 ToVector3(this Vector2 angle)
        {
            return new Vector3(angle.y, angle.x, 0);
        }

        public static Color HealthToColor(this int id)
        {
            int i = id;
            if (255 - (int)(id * 2.55) < 0)
                i = 0;

            if (255 - (int)(id * 2.55) > 255)
                i = 255;

            if ((int)(id * 2.55) > 255)
                i = 255;

            if ((int)(id * 2.55) < 0)
                i = 0;

            return Color.FromArgb(255, 255 - (int)(i * 2.55), (int)(i * 2.55), 80);
        }

        public static Color BombToColor(this float id)
        {
            float i = id;
            if (255 - (int)(id * 6.375) < 0)
                i = 0;

            if (255 - (int)(id * 6.375) > 255)
                i = 255;

            if ((int)(id * 6.375) > 255)
                i = 255;

            if ((int)(id * 6.375) < 0)
                i = 0;

            return Color.FromArgb(255, 255 - (int)(i * 6.375), (int)(i * 6.375), 80);
        }

        public static Vector2 ToVector2(this Vector3 angle)
        {
            return new Vector2(angle.x, angle.y);
        }

        public static Bitmap bmp = new Bitmap(1000, 1000);
        public static RawRectangleF StringSize(string text, Point p)
        {
            Graphics e = Graphics.FromImage(bmp);

            SizeF size = e.MeasureString(text, gvar.font);
            RawRectangleF rect = new RawRectangleF(p.X, p.Y, size.Width, size.Height);
            return rect;
        }

        public static RawRectangleF StringSize(string text, int x, int y)
        {
            Graphics e = Graphics.FromImage(bmp);

            SizeF size = e.MeasureString(text, gvar.font);
            RawRectangleF rect = new RawRectangleF(x, y, size.Width, size.Height);
            return rect;
        }

        public static SizeF MeasureString(string text)
        {
            Graphics e = Graphics.FromImage(bmp);
            SizeF size = e.MeasureString(text, gvar.font);
            return size;
        }

        public static RawColor4 toRawColor4(this Color c)
        {
            RawColor4 rc4 = new RawColor4();
            rc4.A = (float)c.A / 255f;
            rc4.R = (float)c.R / 255f;
            rc4.G = (float)c.G / 255f;
            rc4.B = (float)c.B / 255f;

            return rc4;
        }

        public static GlowColor toGlow(this Color c)
        {
            GlowColor g = new GlowColor();
            g.r = c.R / 255f;
            g.g = c.G / 255f;
            g.b = c.B / 255f;
            g.a = c.A / 255f;
            return g;
        }

        public static RenderColor toRender(this Color c)
        {
            RenderColor render = new RenderColor(c.R, c.G, c.B, c.A);
            return render;
        }

        public static Vector2 ToScreen(this Vector3 position)
        {
            Vector2 returnVector = new Vector2()
            {
                x = ESP.matrix.m11 * position.x + ESP.matrix.m12 * position.y + ESP.matrix.m13 * position.z + ESP.matrix.m14,
                y = ESP.matrix.m21 * position.x + ESP.matrix.m22 * position.y + ESP.matrix.m23 * position.z + ESP.matrix.m24,
            };
            float floatTemp = ESP.matrix.m41 * position.x + ESP.matrix.m42 * position.y + ESP.matrix.m43 * position.z + ESP.matrix.m44;

            if (floatTemp < 0.01f)
                return new Vector2(-1, -1);

            float invFloatTemp = 1f / floatTemp;

            returnVector *= invFloatTemp;
            float x = gvar.OverlaySize.Width / 2f;
            float y = gvar.OverlaySize.Height / 2f;

            x += 0.5f * returnVector.x * gvar.OverlaySize.Width + 0.5f;
            y -= 0.5f * returnVector.y * gvar.OverlaySize.Height + 0.5f;
            returnVector.x = x;
            returnVector.y = y;

            return returnVector;
        }

        public static RawVector2 ToVector(this Vector2 position)
        {
            var vec = new RawVector2();
            vec.X = position.x;
            vec.Y = position.y;
            return vec;
        }
    }
}
