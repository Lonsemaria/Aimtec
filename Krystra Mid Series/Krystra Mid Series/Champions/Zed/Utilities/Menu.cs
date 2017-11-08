using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util;
using Aimtec.SDK.Util.Cache;

namespace Krystra_Mid_Series.Champions
{

    internal partial class Zed
    {
        public Menu RootM = GeneralMenu.RootM;

        public void LoadMenu()
        {  
            {
                #region  Combo Menu
                {

                        GeneralMenu.ComboBack = new Menu("turnback", "W/R Turn back settings");
                        {
                        //GeneralMenu.ComboBack.Add(new MenuBool("targetdied", "Turn Back if target died.", false));
                        GeneralMenu.ComboBack.Add(new MenuList("turnbacklogic", "Turn Back if target died.", new[] { "Smart", "Depends On Enemy Number", "Always","Never" }, 0));
                        GeneralMenu.ComboBack.Add(new MenuSliderBool("swaphp", "Swap W/R if my hp % <= ", true, 15, 10, 99));
                        }
                    GeneralMenu.Combo.Add(GeneralMenu.ComboBack);

                    GeneralMenu.Combo.Add(new MenuBool("useQ", "Use Q in Combo", true));
                    GeneralMenu.Combo.Add(new MenuBool("useW", "Use W in Combo", true));
                    GeneralMenu.Combo.Add(new MenuBool("useE", "Use E in Combo", true));
                    GeneralMenu.Combo.Add(new MenuBool("useR", "Use R in Combo", true));
                    GeneralMenu.Combo.Add(new MenuList("rlogic", "Combo Logic", new[] { "Use Line Mode", "Use Rectangle Mode", "Use Mouse Position" }, 0));
                        if (SpellManager.IsIgnite)
                        {
                        GeneralMenu.Combo.Add(new MenuList("useI", "Use Ignite in Combo", new[] { "Agressive Mode", "Safe Mode","Never" }, 1));
                    }
                    GeneralMenu.Combo.Add(new MenuSeperator("blank1"));
                    GeneralMenu.Combo.Add(new MenuSeperator("extra", "Extra Settings"));
                    GeneralMenu.Combo.Add(new MenuBool("forcer", "Force to start with Ulti"));
                    GeneralMenu.Combo.Add(new MenuBool("secondw", "Use second W to Chase", false));
                    // Combo.Add(new MenuBool("useitem", "Use Items After R", true));
                    GeneralMenu.Combo.Add(new MenuBool("wgap", "Use W to Gap Close", false));
                    GeneralMenu.Combo.Add(new MenuSliderBool("Mana", "Energy Manager %", true, 15, 10, 99));

                    GeneralMenu.ComboBlacklist = new Menu("blacklist", "Blacklist For Ulti");
                    foreach (var tar in GameObjects.EnemyHeroes)
                        {
                        GeneralMenu.ComboBlacklist.Add(new MenuBool("use" + tar.ChampionName.ToLower(), "Don't use on :" + tar.ChampionName, false));
                        }
                    GeneralMenu.Combo.Add(GeneralMenu.ComboBlacklist);

                }
                #endregion

                #region Harass Menu
                {
                    GeneralMenu.AutoHarass = new Menu("autoharass", "Auto Harass Settings");
                        {
                        GeneralMenu.AutoHarass.Add(new MenuBool("use", "Use Special Auto Harass", false));
                        GeneralMenu.AutoHarass.Add(new MenuList("harasslogic", "Special Harass Mode", new[] { "WQ", "WQE", "WE" }, 0));
                        GeneralMenu.AutoHarass.Add(new MenuBool("useQ", "Use Auto Q Harass", false));
                        GeneralMenu.AutoHarass.Add(new MenuBool("useE", "Use Auto E Harass", false));
                        }
                    GeneralMenu.Harass.Add(GeneralMenu.AutoHarass);

                    GeneralMenu.Harass.Add(new MenuBool("useQ", "Use Q in Harass", true));
                    GeneralMenu.Harass.Add(new MenuBool("useW", "Use W in Harass", true));
                    GeneralMenu.Harass.Add(new MenuBool("useE", "Use E in Harass", true));
                    GeneralMenu.Harass.Add(new MenuSliderBool("Mana", "Energy Manager %", true, 15, 10, 99));
                    }
                #endregion

                #region Farm Menu
                {
                        {
                        GeneralMenu.LaneClear.Add(new MenuBool("useQ", "Use Q in LaneClear", true));
                        //  LaneClear.Add(new MenuSliderBool("qcount", "Minimum minion to Q", true, 2, 1, 10));
                        GeneralMenu.LaneClear.Add(new MenuBool("useW", "Use W in LaneClear", true));
                        GeneralMenu.LaneClear.Add(new MenuBool("useE", "Use E in LaneClear", true));
                        GeneralMenu.LaneClear.Add(new MenuSlider("ecount", "Minimum minion to E", 2, 1, 10));

                        GeneralMenu.LaneClear.Add(new MenuSeperator("blank1"));
                        GeneralMenu.LaneClear.Add(new MenuSeperator("energylane", "Energy Manager"));
                        GeneralMenu.LaneClear.Add(new MenuSlider("QMana", "Q Skill Energy Manager  %",  30, 10, 100));
                        GeneralMenu.LaneClear.Add(new MenuSlider("WMana", "W Skill Energy Manager  %",  30, 10, 100));
                        GeneralMenu.LaneClear.Add(new MenuSlider("Emana", "E Skill Energy Manager  %",  30, 10, 100));
                        }

                         {
                        GeneralMenu.JungleClear.Add(new MenuBool("useQ", "Use Q in JungleClear", true));
                        GeneralMenu.JungleClear.Add(new MenuBool("useW", "Use W in JungleClear", true));
                        GeneralMenu.JungleClear.Add(new MenuBool("useE", "Use E in JungleClear", true));
                        GeneralMenu.JungleClear.Add(new MenuSeperator("blank1"));
                        GeneralMenu.JungleClear.Add(new MenuSeperator("energylane", "Energy Manager"));
                        GeneralMenu.JungleClear.Add(new MenuSliderBool("QMana", "Q Skill Energy Manager  %", true, 30, 10, 99));
                        GeneralMenu.JungleClear.Add(new MenuSliderBool("WMana", "W Skill Energy Manager  %", true, 30, 10, 99));
                        GeneralMenu.JungleClear.Add(new MenuSliderBool("Emana", "E Skill Energy Manager  %", true, 30, 10, 99));
                         }

                        {
                        GeneralMenu.LastHit.Add(new MenuBool("autolasthit", "Use Auto LastHit", false));
                        GeneralMenu.LastHit.Add(new MenuBool("useQ", "Use Q in LastHit", true));
                        GeneralMenu.LastHit.Add(new MenuBool("useE", "Use E in LastHit", true));
                        GeneralMenu.LastHit.Add(new MenuSeperator("blank1"));
                        GeneralMenu.LastHit.Add(new MenuSeperator("energylane", "Energy Manager"));
                        GeneralMenu.LastHit.Add(new MenuSliderBool("QMana", "Q Skill Energy Manager  %", true, 30, 10, 99));
                        GeneralMenu.LastHit.Add(new MenuSliderBool("Emana", "E Skill Energy Manager  %", true, 30, 10, 99));
                        }

                    }
                #endregion

                #region Escape Menu
                {
                    GeneralMenu.Escape.Add(new MenuBool("useW", "Use W While Escape", true));
                }
                #endregion
                #region Killsteal Menu
                {
                    {
                        GeneralMenu.OneShot = new Menu("oneshot", "OneShotCombo Settings");
                        GeneralMenu.OneShot.Add(new MenuBool("oto", "Use One Shot Combo", true));
  
                          foreach (var tar in GameObjects.EnemyHeroes)
                          {
                            GeneralMenu.OneShot.Add(new MenuBool("use" +tar.ChampionName.ToLower(), "Use on :" + tar.ChampionName, false));
                          }
                      }
                    GeneralMenu.KillSteal.Add(GeneralMenu.OneShot);

                    GeneralMenu.KillSteal.Add(new MenuBool("ks", "Use KillSteal", true));
                    GeneralMenu.KillSteal.Add(new MenuBool("useQ", "Use Q in Killsteal", true));
                    GeneralMenu.KillSteal.Add(new MenuBool("useE", "Use E in Killsteal", true));
                    if (SpellManager.IsIgnite)
                    {
                        GeneralMenu.KillSteal.Add(new MenuBool("useI", "Use Ignite Killsteal", true));
                    }
                }

                #endregion

                #region Drawing Menu
                {
                    GeneralMenu.WShadow = new Menu("drawshadowW", "Circle for W Shadow");
                    {
                        GeneralMenu.WShadow.Add(new MenuBool("enable", "Enable", true));
                        GeneralMenu.WShadow.Add(new MenuBool("timer", "Draw Shadow Time", true));
                    }
                    GeneralMenu.Draw.Add(GeneralMenu.WShadow);
                    GeneralMenu.RShadow = new Menu("drawshadowR", "Circle for R Shadow");
                    {
                        GeneralMenu.RShadow.Add(new MenuBool("enable", "Enable", true));
                        GeneralMenu.RShadow.Add(new MenuBool("timer", "Draw Shadow Time", true));
                    }
                    GeneralMenu.Draw.Add(GeneralMenu.RShadow);
                    {
                        GeneralMenu.DrawOptions.Add(new MenuBool("qdraw", "Draw Q Range", true));
                        GeneralMenu.DrawOptions.Add(new MenuBool("wdraw", "Draw W Range", false));
                        GeneralMenu.DrawOptions.Add(new MenuBool("edraw", "Draw E Range", false));
                        GeneralMenu.DrawOptions.Add(new MenuBool("rdraw", "Draw R Range", false));
                    }

                    GeneralMenu.Draw.Add(new MenuBool("combomode", "Draw Combo Mode", true));
                    GeneralMenu.Draw.Add(new MenuBool("damage", "Draw Damage Indicator", false));
                    GeneralMenu.Draw.Add(new MenuBool("targetcal", "Target Calculation Text", false));
                    GeneralMenu.Draw.Add(new MenuBool("disable", "Disable All Drawings", false));

                }
                
                Gapcloser.Attach(GeneralMenu.Misc);
                #endregion
                #region Key Menu
                {
                    GeneralMenu.Key.Add(new MenuSeperator("combo1", "Combo Key Settings"));
                    GeneralMenu.Key.Add(new MenuKeyBind("combomode", "Combo Mode Key", KeyCode.G, KeybindType.Press));
                    //  Key.Add(new MenuKeyBind("jungleclearkey", "JungleClear Key", KeyCode.V, KeybindType.Press));
                    GeneralMenu.Key.Add(new MenuSeperator("other", "Other Key Settings"));
                    GeneralMenu.Key.Add(new MenuKeyBind("escape", "Escape Key", KeyCode.Y, KeybindType.Press));
                    GeneralMenu.Key.Add(new MenuSeperator("b","Combo,Harass,Clear keys are the same with orbwalker"));

                }
                #endregion
            }
        }
    }
}