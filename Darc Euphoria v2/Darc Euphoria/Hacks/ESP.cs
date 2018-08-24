using System.Drawing;
using System.Windows.Forms;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Euphoric.Structs;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using Factory = SharpDX.DirectWrite.Factory;
using FontStyle = SharpDX.DirectWrite.FontStyle;

namespace Darc_Euphoria.Hacks
{
    internal class ESP
    {
        public static Settings.UserSettings.Visuals visuals = Settings.userSettings.VisualSettings;
        public static Settings.UserSettings.VisColors visColors = Settings.userSettings.VisualColors;
        public static string spectatorString;

        public static TextFormat txtForm = new TextFormat(new Factory(),
            "Calibri", FontWeight.Black, FontStyle.Normal, 10);

        public static TextFormat txtForm2 = new TextFormat(new Factory(),
            "Calibri", FontWeight.Black, FontStyle.Normal, 20);


        private static Matrix4x4 _matrix4;
        private static int rMatrix = 0;
        public static Matrix4x4 matrix
        {
            get
            {
                if (rMatrix.Upd())
                    _matrix4 = Memory.Read<Matrix4x4>(Memory.Client + Offsets.dwViewMatrix);

                return _matrix4;
            }
        }


        public static void Start(RenderTarget Device)
        {
            spectatorString = "Spectators:\n";
            visuals = Settings.userSettings.VisualSettings;
            visColors = Settings.userSettings.VisualColors;

            EntityList.List = Entity.EntityArray;
            EntityList.ItemList = ItemObjects.ItemList;

            if (!visuals.Enabled)
                if (!Settings.userSettings.MiscSettings.C4Countdown)
                    if (!Settings.userSettings.MiscSettings.Spectators)
                        return;

            Players(Device);
            Items(Device);
        }

        public static void Players(RenderTarget Device)
        {
            foreach (var player in EntityList.List)
            {
                if (Settings.userSettings.MiscSettings.Spectators && Local.Health > 0)
                {
                    foreach (var spec in EntityList.List)
                        if (player.Observe == spec.Ptr)
                            spectatorString += player.Name + " -> " + spec.Name + "\n";
                        else if (player.Observe == Local.Ptr)
                            spectatorString += player.Name + " -> " + spec.Name + "<<\n";
                    Spectators(Device);
                }

                if (!visuals.Enabled) continue;

                if (player.Health <= 0) continue;
                if (player.Dormant) continue;
                if (player.isTeam && !visuals.DisplayTeam) continue;

                var screenPos = player.Position.ToScreen();
                if (screenPos.x == -1f && screenPos.y == -1f) continue;

                var headPos = player.BonePosition(8).ToScreen();
                if (headPos.x == -1f && headPos.y == -1f) continue;

                if (visuals.Snaplines)
                    Snaplines(Device, player);

                var drawArea = new DrawArea();
                drawArea.height = (int) (screenPos.y - headPos.y);
                drawArea.width = (int) (drawArea.height / 2.1f);
                drawArea.x = (int) ((screenPos.x + headPos.x) / 2f - drawArea.width / 2f);
                drawArea.y = (int) (headPos.y - drawArea.height / 10f);
                drawArea.height *= 1.1f;

                if (drawArea.x + drawArea.width + 20 < 0)
                    continue;

                if (drawArea.y + drawArea.height + 20 < 0)
                    continue;

                if (drawArea.x - 20 > gvar.OverlaySize.Width)
                    continue;

                if (drawArea.y - 20 > gvar.OverlaySize.Height)
                    continue;

                if (visuals.BoxEsp == Settings.BoxDisplay._3D)
                    if (drawArea.x + drawArea.width >= 0)
                        if (drawArea.y + drawArea.height >= 0)
                            if (drawArea.x <= gvar.OverlaySize.Width)
                                if (drawArea.y <= gvar.OverlaySize.Height)
                                    Boxes3D(Device, player);

                if (visuals.BoxEsp == Settings.BoxDisplay._2D)
                    Boxes2D(Device, drawArea, player);

                if (visuals.BoxEsp == Settings.BoxDisplay.Edges)
                    BoxesEdges(Device, drawArea, player);

                if (visuals.Name)
                    Name(Device, drawArea, player);

                if (visuals.Rank)
                    Rank(Device, drawArea, player);

                if (visuals.ActiveWeapon)
                    ActiveWeapon(Device, drawArea, player);

                if (visuals.HealthPostion != Settings.HealthDisplay.Off)
                    HealthBar(Device, drawArea, player);

                if (visuals.Glow || visuals.PseudoChams)
                    Glow.Start(player, Settings.userSettings.VisualSettings, Settings.userSettings.VisualColors);
            }
        }

        public static void Items(RenderTarget Device)
        {
            txtForm.SetWordWrapping(WordWrapping.NoWrap);
            txtForm.SetTextAlignment(TextAlignment.Center);

            foreach (var item in EntityList.ItemList)
            {
                if (item.ClassName == "Planted C4" && Settings.userSettings.MiscSettings.C4Countdown)
                {
                    txtForm2.SetWordWrapping(WordWrapping.NoWrap);
                    txtForm2.SetTextAlignment(TextAlignment.Center);

                    var c4Blow = Memory.Read<float>(item.Ptr + Netvars.m_flC4Blow);
                    var TimeLeft = c4Blow - gvar.GlobalVarsBase.curtime;
                    if (TimeLeft < 0) TimeLeft = 0;
                    var rect = new RawRectangleF(0, 45, gvar.OverlaySize.Width, 57);
                    var brush = new SolidColorBrush(Device, Color.FromArgb(1, 1, 1).toRawColor4());

                    Device.DrawText(TimeLeft.ToString("0.00"), txtForm2, rect, brush);
                    brush = new SolidColorBrush(Device, TimeLeft.BombToColor().toRawColor4());
                    Device.DrawText(TimeLeft.ToString("0.00"), txtForm2, rect, brush);
                }

                if (!visuals.Enabled) continue;
                if (item.Dormant) continue;
                if (item.HasOwner) continue;
                if (item.ClassName == "-1") continue;

                using (var brush = new SolidColorBrush(Device, Color.Empty.toRawColor4()))
                {
                    if (visuals.ItemEsp || visuals.GrenadesEsp)
                        if (item.WeaponName != "-1")
                        {
                            var rect = new RawRectangleF
                            {
                                Top = item.Position.ToScreen().y - 10,
                                Bottom = item.Position.ToScreen().y,
                                Left = item.Position.ToScreen().x - 10,
                                Right = item.Position.ToScreen().x + 10
                            };

                            if (item.isWeapon && !visuals.ItemEsp) continue;
                            if (item.ClassName == "Decoy" && !visuals.GrenadesEsp) continue;
                            if (item.ClassName == "Smoke" && !visuals.GrenadesEsp) continue;
                            if (item.ClassName == "Incendiary" && !visuals.GrenadesEsp) continue;
                            if (item.ClassName == "Molotov" && !visuals.GrenadesEsp) continue;
                            if (item.ClassName == "Grenade" && !visuals.GrenadesEsp) continue;
                            if (item.ClassName == "Flashbang" && !visuals.GrenadesEsp) continue;
                            if (item.ClassName == "HE Grenade" && !visuals.GrenadesEsp) continue;

                            brush.Color = visColors.World_Text.toRawColor4();

                            Device.DrawText(item.WeaponName, txtForm, rect, brush,
                                DrawTextOptions.EnableColorFont | DrawTextOptions.DisableColorBitmapSnapping |
                                DrawTextOptions.NoSnap);
                            continue;
                        }

                    if (item.WeaponName != "-1") continue;

                    if (item.ClassName == "Chicken" && !visuals.ChickenEsp) continue;
                    if (item.ClassName == "Defuser" && !visuals.DefuserEsp) continue;
                    if (item.ClassName == "Planted C4" && !visuals.PlantedC4Esp) continue;
                    if (item.ClassName == "Decoy" && !visuals.GrenadesEsp) continue;
                    if (item.ClassName == "Smoke" && !visuals.GrenadesEsp) continue;
                    if (item.ClassName == "Incendiary" && !visuals.GrenadesEsp) continue;
                    if (item.ClassName == "Grenade" && !visuals.GrenadesEsp) continue;
                    if (item.ClassName == "Flashbang" && !visuals.GrenadesEsp) continue;
                    if (item.ClassName == "HE Grenade" && !visuals.GrenadesEsp) continue;

                    var rect2 = new RawRectangleF
                    {
                        Top = item.Position.ToScreen().y - 10,
                        Bottom = item.Position.ToScreen().y,
                        Left = item.Position.ToScreen().x - 10,
                        Right = item.Position.ToScreen().x + 10
                    };

                    brush.Color = Color.FromArgb(1, 1, 1).toRawColor4();
                    Device.DrawText(item.ClassName, txtForm, rect2, brush, DrawTextOptions.NoSnap);
                    brush.Color = visColors.World_Text.toRawColor4();
                    Device.DrawText(item.ClassName, txtForm, rect2, brush,
                        DrawTextOptions.EnableColorFont | DrawTextOptions.DisableColorBitmapSnapping |
                        DrawTextOptions.NoSnap);
                }

                ;
            }
        }

        public static void Boxes2D(RenderTarget Device, DrawArea drawArea, Entity player)
        {
            var col = Color.Empty;
            if (player.Visible && player.isTeam) col = visColors.Team_Box_Visible;
            else if (!player.Visible && player.isTeam) col = visColors.Team_Box_Hidden;
            else if (player.Visible && !player.isTeam) col = visColors.Enemy_Box_Visible;
            else if (!player.Visible && !player.isTeam) col = visColors.Enemy_Box_Hidden;


            var p1 = new RawVector2(drawArea.x, drawArea.y);
            var p2 = new RawVector2(drawArea.x + drawArea.width, drawArea.y);
            var p3 = new RawVector2(drawArea.x, drawArea.y + drawArea.height);
            var p4 = new RawVector2(p2.X, p3.Y);

            if (visuals.BoxOutline)
                using (var brush = new SolidColorBrush(Device, Color.FromArgb(1, 1, 1).toRawColor4()))
                {
                    Device.DrawLine(p1, p2, brush, 3);
                    Device.DrawLine(p2, p4, brush, 3);
                    Device.DrawLine(p4, p3, brush, 3);
                    Device.DrawLine(p3, p1, brush, 3);
                }

            using (var brush = new SolidColorBrush(Device, col.toRawColor4()))
            {
                Device.DrawLine(p1, p2, brush, 1);
                Device.DrawLine(p2, p4, brush, 1);
                Device.DrawLine(p4, p3, brush, 1);
                Device.DrawLine(p3, p1, brush, 1);
            }
        }

        public static void BoxesEdges(RenderTarget Device, DrawArea drawArea, Entity player)
        {
            var col = Color.Empty;
            if (player.Visible && player.isTeam) col = visColors.Team_Box_Visible;
            else if (!player.Visible && player.isTeam) col = visColors.Team_Box_Hidden;
            else if (player.Visible && !player.isTeam) col = visColors.Enemy_Box_Visible;
            else if (!player.Visible && !player.isTeam) col = visColors.Enemy_Box_Hidden;


            var len = (int) (drawArea.width / 3.5);
            var p1 = new RawVector2(drawArea.x, drawArea.y);
            var p2 = new RawVector2(drawArea.x + drawArea.width, drawArea.y);
            var p3 = new RawVector2(drawArea.x, drawArea.y + drawArea.height);
            var p4 = new RawVector2(p2.X, p3.Y);
            var p11 = new RawVector2(p1.X + len, p1.Y);
            var p12 = new RawVector2(p1.X, p1.Y + len);
            var p21 = new RawVector2(p2.X - len, p2.Y);
            var p22 = new RawVector2(p2.X, p2.Y + len);
            var p31 = new RawVector2(p3.X + len, p3.Y);
            var p32 = new RawVector2(p3.X, p3.Y - len);
            var p41 = new RawVector2(p4.X - len, p4.Y);
            var p42 = new RawVector2(p4.X, p4.Y - len);


            if (visuals.BoxOutline)
                using (var brush = new SolidColorBrush(Device, Color.FromArgb(1, 1, 1).toRawColor4()))
                {
                    Device.DrawLine(p11, p1, brush, 3);
                    Device.DrawLine(p1, p12, brush, 3);
                    Device.DrawLine(p21, p2, brush, 3);
                    Device.DrawLine(p2, p22, brush, 3);
                    Device.DrawLine(p31, p3, brush, 3);
                    Device.DrawLine(p3, p32, brush, 3);
                    Device.DrawLine(p41, p4, brush, 3);
                    Device.DrawLine(p4, p42, brush, 3);
                }

            using (var brush = new SolidColorBrush(Device, col.toRawColor4()))
            {
                Device.DrawLine(p11, p1, brush, 1);
                Device.DrawLine(p1, p12, brush, 1);
                Device.DrawLine(p21, p2, brush, 1);
                Device.DrawLine(p2, p22, brush, 1);
                Device.DrawLine(p31, p3, brush, 1);
                Device.DrawLine(p3, p32, brush, 1);
                Device.DrawLine(p41, p4, brush, 1);
                Device.DrawLine(p4, p42, brush, 1);
            }
        }

        public static void Boxes3D(RenderTarget Device, Entity player)
        {
            var col = Color.Empty;
            if (player.Visible && player.isTeam) col = visColors.Team_Box_Visible;
            else if (!player.Visible && player.isTeam) col = visColors.Team_Box_Hidden;
            else if (player.Visible && !player.isTeam) col = visColors.Enemy_Box_Visible;
            else if (!player.Visible && !player.isTeam) col = visColors.Enemy_Box_Hidden;

            var size = 15;

            var p1 = player.Position;
            var p2 = p1;
            var p3 = p1;
            var p4 = p1;

            p1.x -= size;
            p1.y -= size;
            p2.x -= size;
            p2.y += size;
            p3.x += size;
            p3.y += size;
            p4.x += size;
            p4.y -= size;


            if (p1.ToScreen().x == -1f && p1.ToScreen().y == -1f) return;
            if (p2.ToScreen().x == -1f && p2.ToScreen().y == -1f) return;
            if (p3.ToScreen().x == -1f && p3.ToScreen().y == -1f) return;
            if (p4.ToScreen().x == -1f && p4.ToScreen().y == -1f) return;

            var headOrigin = player.BonePosition(8);
            headOrigin.z += 7;

            var p5 = p1;
            var p6 = p2;
            var p7 = p3;
            var p8 = p4;
            p5.z = headOrigin.z;
            p6.z = headOrigin.z;
            p7.z = headOrigin.z;
            p8.z = headOrigin.z;

            if (p5.ToScreen().x == -1f && p5.ToScreen().y == -1f) return;
            if (p6.ToScreen().x == -1f && p6.ToScreen().y == -1f) return;
            if (p7.ToScreen().x == -1f && p7.ToScreen().y == -1f) return;
            if (p8.ToScreen().x == -1f && p8.ToScreen().y == -1f) return;

            if (visuals.BoxOutline)
                using (var brush = new SolidColorBrush(Device, Color.FromArgb(1, 1, 1).toRawColor4()))
                {
                    Device.DrawLine(p7.ToScreen().ToVector(), p8.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p4.ToScreen().ToVector(), p8.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p3.ToScreen().ToVector(), p4.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p3.ToScreen().ToVector(), p7.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p1.ToScreen().ToVector(), p5.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p6.ToScreen().ToVector(), p7.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p8.ToScreen().ToVector(), p5.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p1.ToScreen().ToVector(), p2.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p4.ToScreen().ToVector(), p1.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p2.ToScreen().ToVector(), p3.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p2.ToScreen().ToVector(), p6.ToScreen().ToVector(), brush, 3);
                    Device.DrawLine(p5.ToScreen().ToVector(), p6.ToScreen().ToVector(), brush, 3);
                }

            using (var brush = new SolidColorBrush(Device, col.toRawColor4()))
            {
                Device.DrawLine(p7.ToScreen().ToVector(), p8.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p4.ToScreen().ToVector(), p8.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p3.ToScreen().ToVector(), p4.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p3.ToScreen().ToVector(), p7.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p1.ToScreen().ToVector(), p5.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p6.ToScreen().ToVector(), p7.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p8.ToScreen().ToVector(), p5.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p1.ToScreen().ToVector(), p2.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p4.ToScreen().ToVector(), p1.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p2.ToScreen().ToVector(), p3.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p2.ToScreen().ToVector(), p6.ToScreen().ToVector(), brush, 1);
                Device.DrawLine(p5.ToScreen().ToVector(), p6.ToScreen().ToVector(), brush, 1);
            }
        }

        public static void HealthBar(RenderTarget Device, DrawArea drawArea, Entity player)
        {
            using (var brush = new SolidColorBrush(Device, Color.FromArgb(1, 1, 1).toRawColor4()))
            {
                var health = player.Health > 100 ? 100 : player.Health;

                var x = drawArea.x - 5;
                var y1 = drawArea.y;
                var y2 = drawArea.y + drawArea.height;
                var y3 = drawArea.y + 1 + (drawArea.height - health * (drawArea.height / 100));
                var y4 = y2 - 1;

                if (visuals.HealthPostion == Settings.HealthDisplay.Right)
                {
                    x = drawArea.x + drawArea.width + 5;
                }
                else if (visuals.HealthPostion != Settings.HealthDisplay.Left)
                {
                    if (visuals.HealthPostion == Settings.HealthDisplay.Top)
                        x = drawArea.y - 5;
                    else
                        x = drawArea.y + drawArea.height + 5;

                    y1 = drawArea.x;
                    y2 = drawArea.x + drawArea.width;
                    y3 = drawArea.x - 1 + health * (drawArea.width / 100);
                    y4 = y1 + 1;
                }

                if (visuals.HealthPostion == Settings.HealthDisplay.Left ||
                    visuals.HealthPostion == Settings.HealthDisplay.Right)
                {
                    Device.DrawLine(new RawVector2(x, y1), new RawVector2(x, y2), brush, 3);
                    brush.Color = player.Health.HealthToColor().toRawColor4();
                    Device.DrawLine(new RawVector2(x, y3), new RawVector2(x, y4), brush, 1);
                }
                else
                {
                    Device.DrawLine(new RawVector2(y1, x), new RawVector2(y2, x), brush, 3);
                    brush.Color = player.Health.HealthToColor().toRawColor4();
                    Device.DrawLine(new RawVector2(y3, x), new RawVector2(y4, x), brush, 1);
                }

                var txtForm = new TextFormat(new Factory(),
                    "Calibri", FontWeight.Black, FontStyle.Normal, 10);


                if (visuals.HealthNumber)
                {
                    txtForm.SetWordWrapping(WordWrapping.NoWrap);
                    var rect = new RawRectangleF(drawArea.x - 30, drawArea.y, drawArea.x, drawArea.y + 10);

                    if (player.isTeam) brush.Color = visColors.Team_Text.toRawColor4();
                    else brush.Color = visColors.Enemy_Text.toRawColor4();

                    Device.DrawText(player.Health + "%", txtForm, rect, brush,
                        DrawTextOptions.EnableColorFont | DrawTextOptions.DisableColorBitmapSnapping |
                        DrawTextOptions.NoSnap);
                }
            }
        }

        public static void Snaplines(RenderTarget Device, Entity player)
        {
            using (var brush = new SolidColorBrush(Device, Color.Empty.toRawColor4()))
            {
                var vec1 = new RawVector2(gvar.OverlaySize.Width / 2, gvar.OverlaySize.Height);
                var vec2 = new RawVector2(player.Position.ToScreen().x, player.Position.ToScreen().y);
                if (vec2.X == -1f && vec2.Y == -1f) return;

                if (player.isTeam && player.Visible) brush.Color = visColors.Team_Snaplines_Visible.toRawColor4();
                else if (player.isTeam && !player.Visible) brush.Color = visColors.Team_Snaplines_Hidden.toRawColor4();
                else if (!player.isTeam && player.Visible)
                    brush.Color = visColors.Enemy_Snaplines_Visible.toRawColor4();
                else if (!player.isTeam && !player.Visible)
                    brush.Color = visColors.Enemy_Snaplines_Hidden.toRawColor4();

                Device.DrawLine(vec1, vec2, brush);
            }
        }

        public static void Name(RenderTarget Device, DrawArea drawArea, Entity player)
        {
            using (var brush = new SolidColorBrush(Device, Color.Empty.toRawColor4()))
            {
                txtForm.SetTextAlignment(TextAlignment.Center);
                txtForm.SetWordWrapping(WordWrapping.NoWrap);

                var rect = new RawRectangleF(
                    drawArea.x,
                    !visuals.Rank ? drawArea.y - 15 : drawArea.y - 25,
                    drawArea.x + drawArea.width,
                    drawArea.y);

                if (visuals.HealthPostion == Settings.HealthDisplay.Top)
                    rect.Top -= 5;

                brush.Color = Color.FromArgb(1, 1, 1).toRawColor4();


                Device.DrawText(player.Name, txtForm, rect, brush, DrawTextOptions.NoSnap);


                if (player.isTeam) brush.Color = visColors.Team_Text.toRawColor4();
                else brush.Color = visColors.Enemy_Text.toRawColor4();
                Device.DrawText(player.Name, txtForm, rect, brush,
                    DrawTextOptions.EnableColorFont | DrawTextOptions.DisableColorBitmapSnapping |
                    DrawTextOptions.NoSnap);
            }

            ;
        }

        public static void Rank(RenderTarget Device, DrawArea drawArea, Entity player)
        {
            using (var brush = new SolidColorBrush(Device, Color.Empty.toRawColor4()))
            {
                txtForm.SetTextAlignment(TextAlignment.Center);
                txtForm.SetWordWrapping(WordWrapping.NoWrap);

                var rect = new RawRectangleF(
                    drawArea.x,
                    drawArea.y - 15,
                    drawArea.x + drawArea.width,
                    drawArea.y);

                if (visuals.HealthPostion == Settings.HealthDisplay.Top)
                    rect.Top -= 5;

                brush.Color = Color.FromArgb(1, 1, 1).toRawColor4();


                Device.DrawText(player.Rank.GetRankName(), txtForm, rect, brush, DrawTextOptions.NoSnap);


                if (player.isTeam) brush.Color = visColors.Team_Text.toRawColor4();
                else brush.Color = visColors.Enemy_Text.toRawColor4();

                Device.DrawText(player.Rank.GetRankName(), txtForm, rect, brush,
                    DrawTextOptions.EnableColorFont | DrawTextOptions.DisableColorBitmapSnapping |
                    DrawTextOptions.NoSnap);
            }

            ;
        }

        public static void ActiveWeapon(RenderTarget Device, DrawArea drawArea, Entity player)
        {
            using (var brush = new SolidColorBrush(Device, Color.Empty.toRawColor4()))
            {
                txtForm.SetTextAlignment(TextAlignment.Center);
                txtForm.SetWordWrapping(WordWrapping.NoWrap);

                var rect = new RawRectangleF(
                    drawArea.x,
                    drawArea.y + drawArea.height,
                    drawArea.x + drawArea.width,
                    drawArea.y + drawArea.height + 30);

                if (visuals.HealthPostion == Settings.HealthDisplay.Bottom)
                    rect.Top += 5;
                rect.Bottom += 5;

                if (player.isTeam) brush.Color = visColors.Team_Text.toRawColor4();
                else brush.Color = visColors.Enemy_Text.toRawColor4();

                Device.DrawText("[" + player.ActiveWeapon.WeaponName + "]", txtForm, rect, brush,
                    DrawTextOptions.EnableColorFont | DrawTextOptions.DisableColorBitmapSnapping |
                    DrawTextOptions.NoSnap);
            }

            ;
        }

        public static void Spectators(RenderTarget Device)
        {
            using (var brush = new SolidColorBrush(Device, Color.Empty.toRawColor4()))
            {
                txtForm.SetTextAlignment(TextAlignment.Trailing);
                txtForm.SetWordWrapping(WordWrapping.NoWrap);

                var rect = new RawRectangleF(
                    0,
                    gvar.OverlaySize.Height / 2,
                    gvar.OverlaySize.Width,
                    gvar.OverlaySize.Height / 2
                );

                brush.Color = Color.FromArgb(1, 1, 1).toRawColor4();


                Device.DrawText(spectatorString, txtForm, rect, brush, DrawTextOptions.NoSnap);

                brush.Color = Color.White.toRawColor4();

                Device.DrawText(spectatorString, txtForm, rect, brush,
                    DrawTextOptions.EnableColorFont | DrawTextOptions.DisableColorBitmapSnapping |
                    DrawTextOptions.NoSnap);
            }

            ;
        }
    }
}