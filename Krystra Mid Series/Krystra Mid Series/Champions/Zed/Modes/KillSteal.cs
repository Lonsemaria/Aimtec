using System;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;

using Aimtec.SDK.Util.Cache;
using System.Linq;
using Aimtec.SDK.Menu.Config;
using Aimtec.SDK.Util;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        #region Killstea part
        public void DoKillSteal()
        {
            if (!RootM["killsteal"]["ks"].As<MenuBool>().Enabled)
            {
                return;
            }
            foreach (var hptarget in GameObjects.EnemyHeroes.Where(a => a.IsValidTarget(1200) && !a.IsDead))
            {
                if (!hptarget.IsValid || hptarget.IsDead || hptarget ==null)
                {
                    return;
                }
                var Health = hptarget.Health;
                var dmgE = MyHero.GetSpellDamage(hptarget, SpellSlot.E);
                if (MyHero.Distance(hptarget) < SpellManager.E.Range && Health < dmgE && !GlobalKeys.ComboKey.Active &&
                    RootM["killsteal"]["useE"].As<MenuBool>().Enabled)
                {
                    this.CastE();
                }
                var dmgQ = MyHero.GetSpellDamage(hptarget, SpellSlot.Q);
                if (MyHero.Distance(hptarget) < SpellManager.Q.Range && Health < dmgQ && !GlobalKeys.ComboKey.Active &&
                    RootM["killsteal"]["useQ"].As<MenuBool>().Enabled)
                {
                    this.CastQ(hptarget);
                }
                if (!SpellManager.IsIgnite) continue;
                var dmgI = (50 + ((MyHero.Level) * 20));
                if (MyHero.Distance(hptarget) < SpellManager.Q.Range && Health < dmgI && !GlobalKeys.ComboKey.Active &&
                    RootM["killsteal"]["useI"].As<MenuBool>().Enabled)
                {
                    SpellManager.Ignite.CastOnUnit(hptarget);
                }
               this.DoOneShotCombo(hptarget);
            }


        }
        #endregion
        #region oneshotcombo
        private Vector3 ShadowPosOneShotCombo;
        private double totaldamage;
        
        public void DoOneShotCombo(Obj_AI_Hero unit)
        {
            if (unit == null || !unit.IsValid || unit.IsDead || unit.Type !=MyHero.Type || !unit.IsEnemy)
            {
                return;
            }
            if (!Rpos.Equals(new Vector3()))
            {
                ShadowPosOneShotCombo = (target.ServerPosition + (target.ServerPosition - Rpos).Normalized() * 450);
            }
            double dmgQ = MyHero.GetSpellDamage(unit, SpellSlot.Q);
            double dmgW = MyHero.GetSpellDamage(unit, SpellSlot.W);
            double dmgE = MyHero.GetSpellDamage(unit, SpellSlot.E);
            double dmgR = MyHero.TotalAttackDamage +( (dmgQ+dmgW+dmgE)*0.25+ (dmgQ + dmgW + dmgE));
            totaldamage = dmgQ + dmgW + dmgE + dmgR;
            if (SpellManager.IsIgnite)
            {
                if (SpellManager.Ignite.Ready)
                {
                    var dmgI = (50 + ((MyHero.Level) * 20));
                    totaldamage += dmgI;
                }
            }
         //   Console.WriteLine(RootM["killsteal"]["oneshot"]["oto"]["use" + unit.ChampionName.ToLower()].As<MenuBool>().Enabled);
            if (totaldamage > unit.Health && MyHero.Mana > (MyHero.SpellBook.GetSpell(SpellSlot.Q).Cost + MyHero.SpellBook.GetSpell(SpellSlot.W).Cost) +
                MyHero.SpellBook.GetSpell(SpellSlot.E).Cost + MyHero.SpellBook.GetSpell(SpellSlot.R).Cost &&
                RootM["killsteal"]["oneshot"]["oto"].As<MenuBool>().Enabled)
            {
                if (SpellManager.Q.Ready && SpellManager.W.Ready && SpellManager.E.Ready && SpellManager.R.Ready)
                {
                    if (true)
                    {
                        if (MyHero.Distance(unit) < SpellManager.R.Range)
                        {
                            CastR(unit);
                        }
                        if (MyHero.Distance(unit) < SpellManager.W.Range )
                        {
                            if (!Rpos.Equals(new Vector3()) )
                            {
                                 this.CastW(ShadowPosOneShotCombo);
                            }
                        }
                        if (!Wpos.Equals(new Vector3()))
                        {
                            if (MyHero.Distance(unit) < SpellManager.Q.Range)
                            {
                                this.CastQ(unit);
                            }
                        }
                        if (!SpellManager.Q.Ready && !SpellManager.W.Ready)
                        {
                            SpellManager.E.Cast();
                        }
                        if (SpellManager.IsIgnite && SpellManager.Ignite.Ready)
                        {
                            foreach (var tar in GameObjects.EnemyHeroes.Where(a => a.IsValidTarget(1200) && !a.IsDead))
                            {
                                var dmgI = (50 + ((MyHero.Level) * 20));
                                var health = tar.Health;
                                if (health < dmgI && MyHero.Distance(tar) < 600)
                                {
                                    SpellManager.Ignite.CastOnUnit(tar);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
