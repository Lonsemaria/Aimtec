using Aimtec;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util;
using Aimtec.SDK.Util.Cache;
using System;

namespace Krystra_Mid_Series
{
    internal class GeneralMenu
    {
        #region Static Operations
        public static Menu RootM, Combo, ComboBack, ComboLogics, ComboBlacklist, Harass, AutoHarass, Farm, LastHit, LaneClear, JungleClear, OneShot,
            KillSteal, Escape, Draw, DrawOptions, Misc, TurretDive, SkinHack, AutoLevel, AntiAfk, Key, RShadow,WShadow,SK,Eloc,Rloc, EvadeMenu;
        #endregion

        #region LoadingMenu
        public static void Loading()
        {
            RootM = new Menu(Constants.MyHero.ChampionName.ToLower(), Constants.ScriptName + "[" + Constants.MyHero.ChampionName + "]", true);
            Constants.IOrbwalker.Attach(RootM);
            #region combo
            {
                Combo = new Menu("combo", Language.GetMenu("General Menu","0"));
                RootM.Add(Combo);
            }
            #endregion
            #region harass
            {
                Harass = new Menu("harass", "Harass Settings");
                RootM.Add(Harass);
            }

            #endregion

            #region SkillLogics

            if(Constants.MyHero.ChampionName == "Lissandra")
            {
                SK = new Menu("skill", "Skill Settings");
                Eloc = new Menu("Elogic", "E Skill Settings");
                SK.Add(Eloc);
                Rloc = new Menu("Rlogic", "R Skill Settings");
                SK.Add(Rloc);
            }
            RootM.Add(SK);
            #endregion
            #region farm
            Farm = new Menu("farm", "Farm Settings");
            {
                LaneClear = new Menu("laneclear", "LaneClear Settings");
                Farm.Add(LaneClear);
                JungleClear = new Menu("jungleclear", "JungleClear Settings");
                Farm.Add(JungleClear);
                LastHit = new Menu("lasthit", "LastHit Settings");
                Farm.Add(LastHit);
            }
            RootM.Add(Farm);

            #endregion
            #region Killsteal
            {
                KillSteal = new Menu("killsteal", "KillSteal Settings");
                    RootM.Add(KillSteal);
             }

         /*   if (ObjectManager.GetLocalPlayer().ChampionName == "Zed")
            {
                var EvadeMenu = new Menu("evade", "Evade Settings");
                Champions.Zedd.EvadeManager.Attach(EvadeMenu);
                Champions.Zedd.EvadeOthers.Attach(EvadeMenu);
                Champions.Zedd.EvadeTargetManager.Attach(EvadeMenu);
                RootM.Add(EvadeMenu);
            }*/
            #endregion
            #region Escape Menu
            {
                Escape = new Menu("escape", "Escape Settings");
                RootM.Add(Escape);
            }
            #endregion
            #region Misc Menu
            Misc = new Menu("misc", "Misc Settings");
            {
                TurretDive = new Menu("turretdive", "Turret Dive Settings");
                {
                    TurretDive.Add(new MenuBool("use", "Use Turret Dive Settings", false));
                    TurretDive.Add(new MenuList("turretdivelogic", "Turret Dive Mode", new[] { "Normal Mode", "Krystra Mode" }, 0));
                    TurretDive.Add(new MenuBool("Drawturret", "Draw Turret Range", false));
                    TurretDive.Add(new MenuSeperator("blank1"));
                    TurretDive.Add(new MenuSeperator("nmb", "Normal Mode Settings >"));
                    TurretDive.Add(new MenuSlider("normalmode", "Minimum Number of Ally Minions", 3, 1, 8));
                    TurretDive.Add(new MenuSlider("health", "Do not dive if my health > % ",  30, 10, 99));
                    TurretDive.Add(new MenuSeperator("blank2"));
                    TurretDive.Add(new MenuSeperator("knmb", "Krystra  Mode Settings >"));
                    TurretDive.Add(new MenuSlider("krystramode", "Minimum Number of Ally Minions",  3, 1, 8));
                    TurretDive.Add(new MenuSlider("krystramode2", "Minimum Number of Ally Champions",  2, 1, 4));
                    TurretDive.Add(new MenuSlider("health1", "Do not dive if my health > % ",  30, 10, 99));
                }
                Misc.Add(TurretDive);
                SkinHack = new Menu("skinhack", "Skinhack Settings");
                {
                    SkinHack.Add(new MenuSeperator("b", "Currently no api supported"));
                }
                Misc.Add(SkinHack);
                AutoLevel = new Menu("autolevel", "AutoLevel Settings");
                {
                    AutoLevel.Add(new MenuBool("uselevel", "Use Auto Level", false));
                    AutoLevel.Add(new MenuList("logic", "Select Skill Order", new[] { "Focus Q>W>E", "Focus Q>E>W", "Focus W>Q>E", "Focus W>E>Q", "Focus E>W>Q", "Focus E>Q>W", "Smart" }, 6));
                }
                Misc.Add(AutoLevel);
                AntiAfk = new Menu("antiafk", "AntiAfk Settings");
                {
                    AntiAfk.Add(new MenuBool("antiafk", "Use Anti Afk", false));
                }
                Misc.Add(AntiAfk);
            }
            RootM.Add(Misc);
            #endregion
            #region Drawing Menu
            {
                Draw = new Menu("draw", "Drawing Settings");
                DrawOptions = new Menu("drawS", "Skill Drawing Settings");
                Draw.Add(DrawOptions);
                RootM.Add(Draw);
            }
            #endregion
            #region Key Menu
            {
                Key = new Menu("keys", "Key Settings");
                RootM.Add(Key);
            }
            #endregion
            var lang = new MenuList("lang", "Select Language", new[] { "English", "Chiniese" }, 0);
            RootM.Add(lang);
            lang.OnValueChanged += ChangeLang;
       
            RootM.Add(new MenuSeperator("1", Constants.ScriptName));
            RootM.Add(new MenuSeperator("2", "Script Version :"+Constants.ScriptVersion));
            RootM.Add(new MenuSeperator("3", "Script was made by Krystra"));
            RootM.Add(new MenuSeperator("4", "Leauge Of Legends Version: "+Game.Version.Substring(8,4)));
            RootM.Attach();
        }
        private static void ChangeLang(MenuComponent sender, ValueChangedArgs args)
        {
            if(sender.InternalName != "lang")
            {
                return;
            }
            switch (sender.Value)
            {
                case 0:
                    Language.WriteTxt("eng");
                    Console.WriteLine("eng");
                    break;
                case 1:
                    Language.WriteTxt("chy");
                    Console.WriteLine("chy");
                    break;
            }
        }
        #endregion
    }
}
