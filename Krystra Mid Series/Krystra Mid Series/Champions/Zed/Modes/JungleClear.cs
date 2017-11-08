
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using System.Linq;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void JungleClearF()
        {
            bool Qmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost;
            bool Wmana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.W).Cost;
            bool Emana = MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost;
            bool GCheck = MyHero.Mana > (MyHero.SpellBook.GetSpell(SpellSlot.E).Cost + MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost);
            var poscheck = !Wpos.Equals(new Vector3()) ? Wpos : MyHero.ServerPosition;
            var creep = GameObjects.Jungle.OrderBy(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(SpellManager.Q.Range) && x.MaxHealth > 15);
           if(creep != null)
            {
                if (GeneralMenu.RootM["farm"]["jungleclear"]["useW"].As<MenuBool>().Enabled && SpellManager.W.Ready && GCheck)
                {
                    CastW(creep.ServerPosition);
                }

                if (GeneralMenu.RootM["farm"]["jungleclear"]["useQ"].As<MenuBool>().Enabled && SpellManager.Q.Ready && Qmana)
                {
                    CastQ(creep);
                }
                if (GeneralMenu.RootM["farm"]["jungleclear"]["useE"].As<MenuBool>().Enabled && SpellManager.E.Ready && Emana && creep.Distance(poscheck) < SpellManager.E.Range)
                {
                    SpellManager.E.Cast();
                }
            }
        }
    }
}
