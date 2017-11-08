using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void DoHarass()
        {
            if (MyHero.ManaPercent() < (GeneralMenu.RootM["harass"]["Mana"].As<MenuSliderBool>().Value) || target == null || !target.IsValid)
            {
                return;
            }
            if (GeneralMenu.RootM["harass"]["useW"].As<MenuBool>().Enabled)
            {
                if (MyHero.Distance(target) < SpellManager.W.Range)
                {
                    var Wposition = target.ServerPosition;
                    this.CastW(Wposition);
                }
                else if (MyHero.Distance(target) > SpellManager.W.Range && target.IsValidTarget(SpellManager.W.Range + SpellManager.Q.Range / 2))
                {
                    var Wposition = MyHero.ServerPosition.Extend(target.ServerPosition, 700);
                    this.CastW(Wposition);
                }
            }

            if ((GeneralMenu.RootM["harass"]["useQ"].As<MenuBool>().Enabled))
            {
                CastQ(target);
            }

            if (GeneralMenu.RootM["harass"]["useE"].As<MenuBool>().Enabled)
            {
                this.CastE();
            }
        }
    }
}
