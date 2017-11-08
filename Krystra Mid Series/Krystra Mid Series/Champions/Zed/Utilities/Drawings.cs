using System;
using Aimtec;
using Aimtec.SDK.Menu.Components;
using System.Drawing;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Util.Cache;
using System.Linq;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void DrawSpells()
        {
            #region Draw Spells

            if (GeneralMenu.RootM["draw"]["disable"].As<MenuBool>().Enabled)
            {
                return;
            }
            if (GeneralMenu.RootM["draw"]["damage"].As<MenuBool>().Enabled)
            {
                DamageIndicator.Draw(true,false);
            }
            if (GeneralMenu.RootM["draw"]["targetcal"].As<MenuBool>().Enabled)
            {
                DamageIndicator.Draw(false, true);
            }
            if (GeneralMenu.RootM["draw"]["drawS"]["qdraw"].As<MenuBool>().Enabled && SpellManager.Q.Ready)
            {
                Render.Circle(MyHero.Position, SpellManager.Q.Range, 50, Color.Aquamarine);
            }
            if (GeneralMenu.RootM["draw"]["drawS"]["wdraw"].As<MenuBool>().Enabled && SpellManager.W.Ready)
            {
                Render.Circle(MyHero.Position, SpellManager.W.Range, 50, Color.Cornsilk);
            }
            if (GeneralMenu.RootM["draw"]["drawS"]["edraw"].As<MenuBool>().Enabled && SpellManager.E.Ready)
            {
                Render.Circle(MyHero.Position, SpellManager.E.Range, 50, Color.LightGreen);
            }
            if (GeneralMenu.RootM["draw"]["drawS"]["rdraw"].As<MenuBool>().Enabled && SpellManager.R.Ready)
            {
                Render.Circle(MyHero.Position, SpellManager.R.Range, 50, Color.Brown);
            }

            if (!Wpos.Equals(new Vector3()) && GeneralMenu.RootM["draw"]["drawshadowW"]["enable"].As<MenuBool>().Enabled)
            {
                Render.Circle(Wpos, 100, 50, Color.DarkRed);
            }
            if (!Rpos.Equals(new Vector3()) && GeneralMenu.RootM["draw"]["drawshadowR"]["enable"].As<MenuBool>().Enabled)
            {
                Render.Circle(Rpos, 100, 50, Color.DarkRed);
            }
            if (GeneralMenu.RootM["misc"]["turretdive"]["Drawturret"].As<MenuBool>().Enabled)
            {
                foreach (var turret in GameObjects.AllGameObjects.Where(a => a.IsTurret))
                {
                    Render.Circle(turret.ServerPosition, 950, 50, Color.White);
                }
            }

            if (GeneralMenu.RootM["draw"]["drawshadowW"]["timer"].As<MenuBool>().Enabled)
            {
                if (!Wpos.Equals(new Vector3()))
                {
                    ;
                    var pos = Wpos.ToScreenPosition();
                    Render.Text(pos, Color.White, "Shadow Duration :" + Math.Round(StartTime - Game.ClockTime) + "s");
                }
            }
            if (GeneralMenu.RootM["draw"]["drawshadowR"]["timer"].As<MenuBool>().Enabled)
            {
                if (!Rpos.Equals(new Vector3()))
                {
                    ;
                    var pos = Rpos.ToScreenPosition();
                    Render.Text(pos, Color.White, "Shadow Duration :" + Math.Round(StartTimeR - Game.ClockTime) + "s");
                }
            }
            var drawpos = "Default";
            if (GeneralMenu.RootM["draw"]["combomode"].As<MenuBool>().Enabled)
            {
                switch (GeneralMenu.RootM["combo"]["rlogic"].As<MenuList>().Value)
                {
                    case 0:
                        drawpos = "Current Combo Mode: Line";
                        break;
                    case 1:
                        drawpos = "Current Combo Mode: Rectangle";
                        break;
                    case 2:
                        drawpos = "Current Combo Mode: Mouse Position";
                        break;
                }
                var pos = MyHero.FloatingHealthBarPosition;
                pos.X += 43;
                pos.Y += 24;
                Render.Text(pos, Color.White, drawpos);
            }
            #endregion
        }
    }
}