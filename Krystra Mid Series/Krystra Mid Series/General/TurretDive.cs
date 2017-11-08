

using System;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util.Cache;
using System.Linq;

namespace Krystra_Mid_Series
{
    internal class TurretDive
    {
        public static bool DiveLogic(Vector3 pos)
        {
            if (!GeneralMenu.RootM["misc"]["turretdive"]["use"].As<MenuBool>().Value || !pos.PointUnderEnemyTurret()  )
            {
                return true;
            }
            foreach (var turret in GameObjects.AllGameObjects.Where(a => a.IsTurret))
            {
                switch (GeneralMenu.RootM["misc"]["turretdive"]["turretdivelogic"].As<MenuList>().Value)
                {
                    case 0:
                        if (CountAllyMinionsInTurret(pos,950) <= GeneralMenu.RootM["misc"]["turretdive"]["normalmode"].As<MenuSlider>().Value || Constants.MyHero.HealthPercent() <
                            GeneralMenu.RootM["misc"]["turretdive"]["normalmode"].As<MenuSlider>().Value)
                        {
                            return false;
                        }
                        break;
                    case 1:
                        if (CountAllyMinionsInTurret(pos, 950) <= GeneralMenu.RootM["misc"]["turretdive"]["krystramode"].As<MenuSlider>().Value || Constants.MyHero.HealthPercent() <
                            GeneralMenu.RootM["misc"]["turretdive"]["normalmode"].As<MenuSlider>().Value || CountAllyHeroesInRange(turret.ServerPosition, 950) <
                            GeneralMenu.RootM["misc"]["turretdive"]["krystramode2"].As<MenuSlider>().Value)
                        {

                            return false;
                        }
                        break;
                    
                }
            }
            return true;
        }
        public static int CountEnemyMinions(Vector3 pos, float range)
        {
            return GameObjects.EnemyMinions.Count(h =>
                h.IsValidTarget(range, false, false, pos));
        }
        public static int CountAllyMinionsInTurret(Vector3 pos,float range)
        {
            return GameObjects.AllyMinions.Count(h =>
                 pos.Distance(h)<range);
        }
        public static int CountAllyHeroesInRange(Vector3 pos, float range)
        {
            return pos.CountAllyHeroesInRange(range);
        }
    }
}
