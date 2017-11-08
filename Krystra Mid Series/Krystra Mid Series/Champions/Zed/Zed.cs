
using System;
using System.Drawing;
using Aimtec.SDK.Extensions;
using Aimtec;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Menu.Config;
using Aimtec.SDK.TargetSelector;
using Aimtec.SDK.Util;
using System.IO;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {

        public Zed()
        {
            Console.WriteLine("Language menu is =" +Language.GetMenu("General Menu","0"));
            this.LoadSpells();
            this.LoadMenu();
            this.LoadEvents();
            LevelUpLevel1();
            
        }

        private void UnLoad(GameObjectTeam team)
        {
 
             string mod;
            switch (RootM["lang"].As<MenuList>().Value)
            {
                case 0:
                    mod = "eng";
                    break;

                case 1:
                    mod = "chy";
                    break;
                default:
                    mod = "eng";
                    break;
            }
            Language.WriteTxt(mod);
            Console.WriteLine("unloading");
        }
        private void OnTick()
        {
            if (Constants.MyHero.IsDead)
            {
                return;
            }
            target = TargetSelector.GetTarget(1600);
            if (GlobalKeys.ComboKey.Active)
            {
                this.DoCombo();
                
            }
            if (GlobalKeys.MixedKey.Active)
            {
                this.DoHarass();
            }
            if (RootM["keys"]["escape"].As<MenuKeyBind>().Enabled)
            {
                this.DoEscape();
            }
            if (GlobalKeys.LastHitKey.Active || RootM["farm"]["lasthit"]["autolasthit"].As<MenuBool>().Enabled)
            {
                this.DoLastHit();
            }
            if (GlobalKeys.WaveClearKey.Active)
            {
                this.DoLaneClear();
            }
            if (GlobalKeys.WaveClearKey.Active)
            {
                this.JungleClearF();
            }
            this.DoKillSteal();
           // this.DoOneShotCombo(target);
            this.LoadAutos();
            this.ReturnSettings();
            ReturnSpecialNew();
        }

        private void OnDraw()
        {
            if (MyHero.IsDead)
            {
                return;
            }
            this.DrawSpells();

        }

        private void ProcessSpell(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs e)
        {
            if (!sender.IsMe)
            {
                return;
            }


            if (e.SpellData.Name == "ZedW")
            {
                Wpos = e.End;
                Wtimer = 5;
                Wdmgp = true;
                StartTime = Game.ClockTime + 5f;

            }
            else if (e.SpellData.Name == "ZedW2")
            {
                Wpos = e.Start;
            }
            if (e.SpellData.Name == "ZedR")
            {
                Rpos = e.Start;
                Rtimer = 7.5f;
                Rdmgp = true;
                Rdmgcheck = true;
                DelayAction.Queue(3750, () => Rdmgcheck = false);
                StartTimeR = Game.ClockTime + 7.5f;

            }
            else if (e.SpellData.Name == "ZedR2")
            {
                Rpos = MyHero.ServerPosition;
            }
            var asd = Game.TickCount;
            if ((GR + 10) < asd)
            {
                if (Rtimer == 7.5)
                {
                    DelayAction.Queue((int)Rtimer * 1000, () =>
                    {
                        Rtimer = 0f;
                        Rdmgcheck = false;
                        Rpos = new Vector3();
                        StartTimeR = 0;
                    }

                    );
                }
                GR = asd;
            }
            var W = Game.TickCount;
            if ((GW + 10) < W)
            {
                if (Wtimer == 5)
                {
                    DelayAction.Queue((int)Wtimer * 1000, () =>
                    {
                        Wtimer = 0;
                        Wdmgp = false;
                        Wpos = new Vector3();
                        StartTime = 0;
                    }

                    );
                }
                GW = W;
            }
        }

        private void ClickEvent(WndProcEventArgs e)
        {
            if (RootM["keys"]["combomode"].As<MenuKeyBind>().Enabled)
            {
                RootM["combo"]["rlogic"].As<MenuList>().Value += 1;
                RootM["keys"]["combomode"].As<MenuKeyBind>().Value =
                    !RootM["keys"]["combomode"].As<MenuKeyBind>().Enabled;
                if (RootM["combo"]["rlogic"].As<MenuList>().Value > 2)
                {
                    RootM["combo"]["rlogic"].As<MenuList>().Value = 0;
                }
            }
        }
    }
}
