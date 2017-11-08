using Aimtec;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {

        public void LoadEvents()
        {
            Render.OnPresent += OnDraw;
            Game.OnUpdate += OnTick;
            Game.OnEnd += UnLoad;
            Obj_AI_Base.OnProcessSpellCast += ProcessSpell;
            Game.OnWndProc += ClickEvent;
            Game.OnNotifyAway += AntiAfk;
            Obj_AI_Base.OnLevelUp += AutoLevelUp;
            Gapcloser.OnGapcloser += AntiGapCloser;
        }
    }
}