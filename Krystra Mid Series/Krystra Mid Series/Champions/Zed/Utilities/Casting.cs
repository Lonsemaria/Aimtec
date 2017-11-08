using System;
using Aimtec;
using Aimtec.SDK.Extensions;


namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void CastQ(Obj_AI_Base unit)
        {
            if (!SpellManager.Q.Ready)
            {
                return;
            }
            if (!Wpos.Equals(new Vector3()))
            {
                if (Wpos.Distance(unit.ServerPosition) < SpellManager.Q.Range)
                {
                    SpellManager.Q.Cast(unit.ServerPosition);
                }
            }
            else if (MyHero.Distance(unit) < SpellManager.Q.Range)
            {
                SpellManager.Q.Cast(unit.ServerPosition);
            }
        }

        public void CastR(Obj_AI_Hero unit)
        {
            if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name != "ZedR" && TurretDive.DiveLogic(SpellManager.R.GetPrediction(unit).CastPosition) == true)
            {
                return;
            }
            if (SpellManager.R.Ready)
            {
                SpellManager.R.CastOnUnit(unit);

            }
        }
        public void CastW(Vector3 pos)
        {
            if (MyHero.SpellBook.GetSpell(SpellSlot.W).Name == "ZedW2")
            {
                return;
            }
            if (SpellManager.W.Ready && this.IsW1() && Game.TickCount - LastCastTime > 175)
            {
                SpellManager.W.Cast(pos);
                LastCastTime = Game.TickCount;


            }
        }
        public void CastW2()
        {
            if (MyHero.SpellBook.GetSpell(SpellSlot.W).Name != "ZedW2")
            {
                return;

            }
            if (SpellManager.W.Ready)
            {
                SpellManager.W.Cast();
            }
        }
        public void CastE()
        {
            if (!SpellManager.E.Ready)
            {
                return;
            }
            if (!Rpos.Equals(new Vector3()))
            {
                if (target.Distance(Rpos) < SpellManager.E.Range)
                {
                    SpellManager.E.Cast();
                }
            }
            if (!Wpos.Equals(new Vector3()))
            {
                if (target.Distance(Wpos) < SpellManager.E.Range)
                {
                    SpellManager.E.Cast();
                }
            }
            if (MyHero.Distance(target) < SpellManager.E.Range)
            {
                SpellManager.E.Cast();

            }
        }
    }
}
