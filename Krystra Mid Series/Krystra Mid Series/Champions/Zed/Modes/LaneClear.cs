using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using System;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void DoLaneClear()
        {
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool GCheck = MyHero.Mana > (MyHero.SpellBook.GetSpell(SpellSlot.E).Cost + MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost);
            var poscheck = !Wpos.Equals(new Vector3()) ? Wpos : MyHero.ServerPosition;
 
            foreach (var minion in GameObjects.EnemyMinions.Where(m => m.IsValidTarget(900) && m.IsMinion))
            {
                if (minion != null)
                {
                    if (GeneralMenu.RootM["farm"]["laneclear"]["useW"].As<MenuBool>().Enabled && SpellManager.W.Ready && GCheck)
                    {
                        if (GameObjects.EnemyMinions.Count(t => t.IsValidTarget(SpellManager.E.Range, false, false, minion.ServerPosition)) > 2)
                        {
                            this.CastW(minion.ServerPosition);
                        }
                    }
                    if (GeneralMenu.RootM["farm"]["laneclear"]["useE"].As<MenuBool>().Enabled && SpellManager.E.Ready && Emana 
                        )
                    {
                        if (GameObjects.EnemyMinions.Count(t => t.IsValidTarget(SpellManager.E.Range, false, false, poscheck)) >= GeneralMenu.RootM["farm"]["laneclear"]["ecount"].As<MenuSlider>().Value)
                        {
                            SpellManager.E.Cast();
                        }
                    }

                    if (GeneralMenu.RootM["farm"]["laneclear"]["useQ"].As<MenuBool>().Enabled && SpellManager.Q.Ready && Qmana  )
                    {
        
                            CastQ(minion);
   
                    }
                }

            }
        }
    }
}
