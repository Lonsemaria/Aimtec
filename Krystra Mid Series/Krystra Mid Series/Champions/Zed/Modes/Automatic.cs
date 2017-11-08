using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util;


namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {

        #region AutoQ
        private void AutoQ()
        {
            if (target == null || !target.IsValid)
            {
                return;
            }
            if (MyHero.Distance(target) < SpellManager.Q.Range && GeneralMenu.RootM["harass"]["useQ"].As<MenuBool>().Enabled)
            {
                this.CastQ(target);
            }
        }
        #endregion
        #region AutoE
        private void AutoE()
        {
            if (target == null || !target.IsValid)
            {
                return;
            }
            if (GeneralMenu.RootM["harass"]["useE"].As<MenuBool>().Enabled)
            {
                this.CastE();
            }
        }
        #endregion  
        #region AutoHarass
        private void AutoHarassF()
        {
            if (target == null || !target.IsValid || MyHero.ManaPercent() < GeneralMenu.RootM["harass"]["Mana"].As<MenuSliderBool>().Value)
            {
                return;
            }
            var Wposition = MyHero.ServerPosition.Extend(target.ServerPosition, 700);
            switch (GeneralMenu.RootM["harass"]["autoharass"]["harasslogic"].As<MenuList>().Value)
            {
                case 0:
                    if (SpellManager.Q.Ready && SpellManager.W.Ready && MyHero.Mana > (Constants.MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost + MyHero.SpellBook.GetSpell(SpellSlot.W).Cost))
                    {
                        if (GeneralMenu.RootM["harass"]["useW"].As<MenuBool>().Enabled)
                        {
                            if (MyHero.Distance(target) < SpellManager.W.Range)
                            {
                                this.CastW(target.ServerPosition);
                            }
                            else if (MyHero.Distance(target) > SpellManager.W.Range && target.IsValidTarget(SpellManager.W.Range + SpellManager.Q.Range / 2))
                            {
                                this.CastW(Wposition);
                            }
                        }
                        if ((!Wpos.Equals(new Vector3()) || IsW2()) && SpellManager.Q.Ready && GeneralMenu.RootM["harass"]["useQ"].As<MenuBool>().Enabled)
                        {
                            if (MyHero.Distance(target) < SpellManager.Q.Range)
                            {
                                this.CastQ(target);
                            }
                            else if (MyHero.Distance(target) > SpellManager.W.Range && target.IsValidTarget(SpellManager.W.Range + SpellManager.Q.Range / 2))
                            {
                                this.CastQ(target);
                            }
                        }

                    }
                    break;
                case 1:
                    if (SpellManager.Q.Ready && SpellManager.W.Ready && SpellManager.E.Ready && MyHero.Mana > (MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost + MyHero.SpellBook.GetSpell(SpellSlot.W).Cost + MyHero.SpellBook.GetSpell(SpellSlot.E).Cost))
                    {
                        if (MyHero.Distance(target) < SpellManager.W.Range && GeneralMenu.RootM["harass"]["useW"].As<MenuBool>().Enabled && SpellManager.W.Ready)
                        {
                            this.CastW(Wposition);
                        }
                        if ((!Wpos.Equals(new Vector3()) || IsW2()) && MyHero.Distance(target) < SpellManager.Q.Range && GeneralMenu.RootM["harass"]["useQ"].As<MenuBool>().Enabled)
                        {
                            this.CastQ(target);
                        }
                        if (SpellManager.E.Ready && GeneralMenu.Harass["useE"].As<MenuBool>().Enabled && (MyHero.Distance(target) < SpellManager.E.Range || ((!Wpos.Equals(new Vector3()) && Wpos.Distance
                            (target) < SpellManager.E.Range))))
                        {
                            this.CastE();
                        }
                    }
                    break;
                case 2:
                    if (SpellManager.E.Ready && SpellManager.W.Ready && MyHero.Mana > MyHero.SpellBook.GetSpell(SpellSlot.E).Cost + MyHero.SpellBook.GetSpell(SpellSlot.W).Cost)
                    {
                        if (MyHero.Distance(target) < SpellManager.W.Range && GeneralMenu.RootM["harass"]["useW"].As<MenuBool>().Enabled)
                        {
                            this.CastW(Wposition);
                        }
                        if (GeneralMenu.Harass["useE"].As<MenuBool>().Enabled && (MyHero.Distance(target)<SpellManager.E.Range || ((!Wpos.Equals(new Vector3()) && Wpos.Distance
                            (target)<SpellManager.E.Range))))
                        {
                            this.CastE();
                        }
                    }
                    break;
            }

        }
        #endregion
        public void LoadAutos()
        {
            if (GeneralMenu.RootM["harass"]["autoharass"]["useQ"].As<MenuBool>().Enabled)
            {
                this.AutoQ();
            }
            if (GeneralMenu.RootM["harass"]["autoharass"]["useE"].As<MenuBool>().Enabled)
            {
                this.AutoE();
            }
            if (GeneralMenu.RootM["harass"]["autoharass"]["use"].As<MenuBool>().Enabled)
            {
                this.AutoHarassF();
            }
        }
    }
}
