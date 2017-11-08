using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Damage.JSON;
using Aimtec.SDK.Events;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using Krystra_Mid_Series;


namespace Krystra_Mid_Series
{
    internal class DamageIndicator
    {
        internal static int Height => 8;
        internal static int Width => 103;

        internal static int XOffset => 11;
        internal static int YOffset => 10;

        public static void Draw(Boolean Damage,Boolean Texto)
        {
         //   if (!Menu._menu["enabled"].As<MenuBool>().Enabled)
               // return;

            foreach (var enemy in GameObjects.EnemyHeroes.Where(e => e.IsValid && e.IsFloatingHealthBarActive && e.IsVisible))
            {
                var barPos = enemy.FloatingHealthBarPosition;

                if (Math.Abs(barPos.X) < 0.0000001) continue;
                if (Math.Abs(barPos.Y) < 0.0000001) continue;

                var Text = "";
                double qDamage = 0;
                double wDamage = 0;
                double eDamage = 0;
                double rDamage = 0;
                double aaDamage = 0;
                double igniteDamage = 0;
                double totalDamage = 0;
                var longcheck = (!SpellManager.Q.Ready && !SpellManager.W.Ready && !SpellManager.E.Ready && !SpellManager.R.Ready);
                var aacount = Constants.MyHero.Level < 6 ? 2 : 4;

                if (SpellManager.Q.Ready)
                {
                    qDamage = Constants.MyHero.GetSpellDamage(enemy, SpellSlot.Q);
                    totalDamage += qDamage;
                }

                if (SpellManager.W.Ready)
                {
                    wDamage = Constants.MyHero.GetSpellDamage(enemy, SpellSlot.W);
                    totalDamage += wDamage;

                }

                if (SpellManager.E.Ready)
                {
                    eDamage = Constants.MyHero.GetSpellDamage(enemy, SpellSlot.E);
                    totalDamage += eDamage;

                }

                if (SpellManager.R.Ready)
                {
                    if(Constants.MyHero.ChampionName == "Zed")
                    {
                        rDamage = Constants.MyHero.TotalAttackDamage + ((qDamage + wDamage + eDamage) * 0.25 + (qDamage + wDamage + eDamage));
                    }
                    else
                    {
                        rDamage = Constants.MyHero.GetSpellDamage(enemy, SpellSlot.R);
                    }
                    totalDamage += rDamage;
                }

                if (SpellManager.Ignite.Ready)
                {
                    igniteDamage = 50 + (20 * Constants.MyHero.Level) - enemy.HPRegenRate / 5 * 3;
                    totalDamage += igniteDamage;
 
                }
                if (longcheck)
                {
                    aaDamage = 0;
                }
                else
                {
                    aaDamage = Constants.MyHero.GetAutoAttackDamage(enemy) * aacount;

                }
                totalDamage += aaDamage;

                var percentHealthAfterDamage = Math.Max(0, enemy.Health - totalDamage) / enemy.MaxHealth;
                var posY = barPos.Y + YOffset;
                var posDamageX = barPos.X + YOffset + Width * percentHealthAfterDamage;
                var posCurrentHealthX = barPos.X + YOffset + Width * enemy.Health / enemy.MaxHealth;
                var difference = posCurrentHealthX - posDamageX;
                var xPos = barPos.X + XOffset + (Width * percentHealthAfterDamage);
                var pos = enemy.FloatingHealthBarPosition;
                pos.X += 43;
                pos.Y += 33;
                if (Damage)
                {
                    Render.Rectangle(new Vector2((float)xPos, posY), (float)difference, Height, Color.FromArgb(222, Color.DarkOrange));
                }

                if (Texto)
                {
                    if (totalDamage < enemy.Health)
                    {
                        Render.Text(pos, Color.White, "Not Killable Yet");
                    }
                    else
                    {
                        Render.Text(pos, Color.White, "Target Killable");
                    }
                }
            }
        }

    }
}