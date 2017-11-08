using System;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Menu.Config;
using Aimtec.SDK.Util;
using Aimtec.SDK.Util.Cache;
using Aimtec.SDK.Damage;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void DoCombo()
        {
            var globalrange = GeneralMenu.RootM["combo"]["forcer"].As<MenuBool>().Enabled && GeneralMenu.RootM["combo"]["useR"].As<MenuBool>().Enabled&& SpellManager.R.Ready && this.IsR1() ? SpellManager.R.Range - 5 : 1500;
            if (target == null || !target.IsValid)
            {
                return;
            }
            var Wcastpos = new Vector3();

            if (!Rpos.Equals(new Vector3()))
            {
                switch (GeneralMenu.RootM["combo"]["rlogic"].As<MenuList>().Value)
                {
                    case 0:
                        Wcastpos = (target.ServerPosition + (target.ServerPosition - Rpos).Normalized() * 450);
                        break;
                    case 1:
                        Wcastpos = (MyHero.ServerPosition + (SpellManager.R.GetPrediction(target).CastPosition - Rpos).Normalized() * SpellManager.W.Range).To2D().Perpendicular().To3D();
                        break;
                    case 2:
                        Wcastpos = Game.CursorPos;
                        break;
                }
            }
            if (MyHero.Distance(target) < globalrange)
            {
                if (MyHero.Distance(target) < SpellManager.R.Range &&
                    GeneralMenu.RootM["combo"]["useR"].As<MenuBool>().Enabled && !GeneralMenu.RootM["combo"]["blacklist"]["use" + target.ChampionName.ToLower()].As<MenuBool>().Enabled)
                {
                    this.CastR(target);
                }


                if (target.HasBuff("zedrtargetmark"))
                {
                    if (GeneralMenu.RootM["combo"]["useW"].As<MenuBool>().Enabled && MyHero.Distance(target) < SpellManager.W.Range)
                    {
                        this.CastW(Wcastpos);
                    }
                }
                else
                {
                    if (GeneralMenu.RootM["combo"]["useW"].As<MenuBool>().Enabled && MyHero.Distance(target) < SpellManager.W.Range && this.IsW1())
                    {
                         this.CastW(target.ServerPosition);
                    }

                    else if (MyHero.Distance(target) > SpellManager.W.Range && target.IsValidTarget(SpellManager.W.Range + SpellManager.Q.Range / 2) &&
                             GeneralMenu.RootM["combo"]["useW"].As<MenuBool>().Enabled)
                    {
                        var Wposition = MyHero.ServerPosition.Extend(target.ServerPosition, 700f);
                        this.CastW(Wposition);
                        if (GeneralMenu.RootM["combo"]["secondw"].As<MenuBool>().Enabled)
                        {
                            this.CastW2();
                        }
                    }
                }

                if (MyHero.Distance(target) < SpellManager.Q.Range && GeneralMenu.RootM["combo"]["useQ"].As<MenuBool>().Enabled)
                {
                    if (!Wpos.Equals(new Vector3()) && MyHero.SpellBook.GetSpell(SpellSlot.W).Name == "ZedW2")
                    {
                        this.CastQ(target);
                    }
                    else if (!SpellManager.W.Ready || MyHero.SpellBook.GetSpell(SpellSlot.W).Name != "ZedW2" || Wpos.Equals(new Vector3()))
                    {
                        this.CastQ(target);
                    }
                }

                if (GeneralMenu.RootM["combo"]["useE"].As<MenuBool>().Enabled)
                {
                    this.CastE();
                }

                if (SpellManager.W.Ready && GeneralMenu.RootM["combo"]["wgap"].As<MenuBool>().Enabled &&
                    target.IsValidTarget(1400) && !target.IsValidTarget(850)
                )
                {
                    var Wposition = MyHero.ServerPosition.Extend(target.ServerPosition, 700f);
                    SpellManager.W.Cast(Wposition);
                }

                if (SpellManager.IsIgnite)
                {
                    if ( GlobalKeys.ComboKey.Active)
                    {
                        switch (GeneralMenu.RootM["combo"]["useI"].As<MenuList>().Value)
                        {
                            case 0:
                                foreach (var tar in GameObjects.EnemyHeroes.Where(a => a.IsValidTarget(1200) && !a.IsDead))
                                {
                                    var dmgI = (50 + ((MyHero.Level) * 20));
                                    var health = tar.Health;
                                    if (health < dmgI && MyHero.Distance(tar) < 600)
                                    {
                                        if (SpellManager.Ignite.Ready)
                                        {
                                            SpellManager.Ignite.CastOnUnit(tar);
                                        }
                                    }
                                }

                                break;
                            case 1:

                                foreach (var tar in GameObjects.EnemyHeroes.Where(a => a.IsValidTarget(1200) && !a.IsDead))
                                {
                                    var dmgI = (50 + ((MyHero.Level) * 20))*1.3;
                                    var qdmg = MyHero.GetSpellDamage(tar, SpellSlot.Q);
                                    var edmg = MyHero.GetSpellDamage(tar, SpellSlot.E);
                                    var health = tar.Health;
                                    var tt = dmgI + qdmg + edmg;
                                    if (health < tt && MyHero.Distance(tar) < 600)
                                    {
                                        if (SpellManager.Ignite.Ready)
                                        {
                                            SpellManager.Ignite.CastOnUnit(tar);
                                        }
                                    }
                                }
                                break;
                            case 2:

                                break;
                        }
                    }
                }
            }

        }
    }
}
