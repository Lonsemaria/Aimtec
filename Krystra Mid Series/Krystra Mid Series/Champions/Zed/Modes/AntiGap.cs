using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        private Vector3 newBase;
        private GameObject t;
        public void AntiGapCloser(Obj_AI_Hero sender, Gapcloser.GapcloserArgs args)
        {
            if (MyHero.IsDead)
            {
                return;
            }

            if (sender == null || !sender.IsEnemy || !sender.IsMelee)
            {
                return;
            }
            if (SpellManager.W.Ready)
            {
                var enabledOption = GeneralMenu.RootM["misc"]["gapcloser"]["enabled"];
                if (enabledOption == null || !enabledOption.As<MenuBool>().Enabled)
                {
                    return;
                }
                var enabledOption2 = GeneralMenu.RootM["misc"]["gapcloser"]["wsk"]["enabled"];
                if (enabledOption2 == null || !enabledOption2.As<MenuBool>().Enabled)
                {
                    return;
                }

                var spellOption = GeneralMenu.RootM["misc"]["gapcloser"][$"{sender.ChampionName.ToLower()}.{args.SpellName.ToLower()}"];
                if (spellOption == null || !spellOption.As<MenuBool>().Enabled)
                {
                    return;
                    
                }
                var other = GeneralMenu.RootM["misc"]["gapcloser"]["wsk"]["wlogic"].As<MenuList>().Value;
                
                switch (other)
                {
                    case 0:
                        newBase = Game.CursorPos;
                        break;
                    case 1:
                        newBase = GetClosestAllyTurret(MyHero.ServerPosition).ServerPosition;
                        break;
                }

                switch (args.Type)
                {
                    case Gapcloser.Type.Targeted:

                        if (args.Target.IsMe)
                        {
                            SpellManager.W.Cast(newBase);
                        }
                    break;

                    default:
                        if(args.EndPosition.Distance(MyHero.ServerPosition) <= MyHero.AttackRange)
                        {
                            SpellManager.W.Cast(newBase);
                        }
                        break;
                }
            }
        }

        private GameObject GetClosestAllyTurret(Vector3 pos)
        {
            var c = (float)10000;
            foreach (var turret in GameObjects.AllGameObjects.Where(a => a.IsTurret && a.Team == MyHero.Team))
            {
                if(MyHero.Distance(turret) <= c)
                {
                    c = MyHero.Distance(turret);
                    t = turret;
                }
            }
            return t;
        }
        private GameObject GetClosestEnemyTurret(Vector3 pos)
        {
            var c = (float)10000;
            foreach (var turret in GameObjects.AllGameObjects.Where(a => a.IsTurret && a.Team != MyHero.Team))
            {
                if (MyHero.Distance(turret) <= c)
                {
                    c = MyHero.Distance(turret);
                    t = turret;
                }
            }
            return t;
        }
    }
}
