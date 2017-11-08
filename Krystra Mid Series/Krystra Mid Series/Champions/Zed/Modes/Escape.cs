using Aimtec;
using Aimtec.SDK.Menu.Components;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed
    {
        public void DoEscape()
        {

            if (GeneralMenu.RootM["escape"]["useW"].As<MenuBool>().Enabled && SpellManager.W.Ready)
            {
                SpellManager.W.Cast(Game.CursorPos);
            }
            else
            {
                MyHero.IssueOrder(OrderType.MoveTo, Game.CursorPos);
            }
        }
    }
}
