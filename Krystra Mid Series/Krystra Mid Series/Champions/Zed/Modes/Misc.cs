using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void ReturnSettings()
        {
            if (target != null && target.IsValid)
            {
                return;
            }
            if (GeneralMenu.RootM["combo"]["turnback"]["swaphp"].As<MenuSliderBool>().Enabled &&
                GeneralMenu.RootM["combo"]["useW"].As<MenuBool>().Enabled)
            {
                if (MyHero.HealthPercent() <= GeneralMenu.RootM["combo"]["turnback"]["swaphp"].As<MenuSliderBool>().Value && MyHero.Distance(target) < 600)
                {
                    this.CastW2();
                    SpellManager.R.Cast();
                }
            }

           
        }
        public void ReturnSpecialNew()
        {
            switch (GeneralMenu.RootM["combo"]["turnback"]["turnbacklogic"].As<MenuList>().Value)
            {
                case 0:
                    // Always
                    break;
                case 1:
                    // Depends On The Enemy
                    break;
                case 2:
                    // Always Option
                    Return2();
                    break;
                case 3:
                    // Never Option
                    break;

            }
        }
        private void Return2()
        {
            if (GeneralMenu.RootM["combo"]["turnback"]["turnbacklogic"].As<MenuBool>().Enabled)
            {
                if (!IsR1() && IsTargetDie() )
                {
                    if (MyHero.SpellBook.GetSpell(SpellSlot.W).Name.ToLower() == "zedw2")
                    {
                        this.CastW2();
                    }
                    else if (MyHero.SpellBook.GetSpell(SpellSlot.R).Name.ToLower() == "zedr2")
                    {
                        SpellManager.R.Cast();
                    }
                }
            }
        }
       private bool IsTargetDie()
        {
            if (target != null && target.IsValid)
            {
                return false;
            }
            return true;
        }
        public bool IsW1()
        {
            return MyHero.SpellBook.GetSpell(SpellSlot.W).Name == "ZedW";
        }
        public bool IsW2()
        {
            return (MyHero.SpellBook.GetSpell(SpellSlot.W).Name == "ZedW2");
        }
        public bool IsR1()
        {
            return (MyHero.SpellBook.GetSpell(SpellSlot.R).Name == "ZedR");
        }
        public void AntiAfk(GameNotifyAwayEventArgs e)
        {
            e.IgnoreDialog = true;
            Orbwalker.Implementation.Move(ObjectManager.GetLocalPlayer().ServerPosition);
        }
        public void AutoLevelUp(Obj_AI_Base obj, Obj_AI_BaseLevelUpEventArgs e)
        {
            if (!obj.IsMe || !e.Source.IsMe )
            {
                return;
            }
            if (GeneralMenu.RootM["misc"]["autolevel"]["uselevel"].As<MenuBool>().Enabled)
            {
                switch (GeneralMenu.RootM["misc"]["autolevel"]["logic"].As<MenuList>().Value)
                {
                    case 0:
                        MyHero.SpellBook.LevelSpell(Level1[MyHero.Level]);
                        break;
                    case 1:
                        MyHero.SpellBook.LevelSpell(Level2[MyHero.Level]);
                        break;
                    case 2:
                        MyHero.SpellBook.LevelSpell(Level3[MyHero.Level]);
                        break;
                    case 3:
                        MyHero.SpellBook.LevelSpell(Level4[MyHero.Level]);
                        break;
                    case 4:
                        MyHero.SpellBook.LevelSpell(Level5[MyHero.Level]);
                        break;
                    case 5:
                        MyHero.SpellBook.LevelSpell(Level6[MyHero.Level]);
                        break;
                    case 6:
                        MyHero.SpellBook.LevelSpell(LevelSpecial[MyHero.Level]);
                        break;
                }
            }
        }
        public void LevelUpLevel1()
        {
            if(MyHero.Level != 1)
            {
                return;
            }

            switch (GeneralMenu.RootM["misc"]["autolevel"]["logic"].As<MenuList>().Value)
            {
                case 0:
                    MyHero.SpellBook.LevelSpell(_Q);
                    break;
                case 1:
                    MyHero.SpellBook.LevelSpell(_Q);
                    break;
                case 2:
                    MyHero.SpellBook.LevelSpell(_W);
                    break;
                case 3:
                    MyHero.SpellBook.LevelSpell(_W);
                    break;
                case 4:
                    MyHero.SpellBook.LevelSpell(_E);
                    break;
                case 5:
                    MyHero.SpellBook.LevelSpell(_E);
                    break;
                case 6:
                    MyHero.SpellBook.LevelSpell(_Q);
                    break;
            }
               
        }
    }
}
