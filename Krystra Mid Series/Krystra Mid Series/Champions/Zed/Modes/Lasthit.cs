using System;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void DoLastHit()
        {
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            foreach (var minion in GameObjects.EnemyMinions.Where(m => m.IsValidTarget(900) && m.IsMinion))
            {
                double Qdamage = MyHero.GetSpellDamage(minion, SpellSlot.Q);
                double Edamage = MyHero.GetSpellDamage(minion, SpellSlot.E);
                if (GeneralMenu.RootM["farm"]["lasthit"]["useQ"].As<MenuBool>().Enabled && Qdamage > minion.Health && SpellManager.Q.Ready && Qmana)
                {
                    SpellManager.Q.Cast(minion);
                }
                if (GeneralMenu.RootM["farm"]["lasthit"]["useE"].As<MenuBool>().Enabled && Edamage > minion.Health && SpellManager.E.Ready && minion.IsValidTarget(SpellManager.E.Range) && Emana)
                {
                    SpellManager.E.Cast();
                }
            }
        }
    }
}

