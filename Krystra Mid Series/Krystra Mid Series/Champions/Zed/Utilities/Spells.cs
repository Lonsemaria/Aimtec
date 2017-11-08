namespace Krystra_Mid_Series.Champions
{

    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;
    using Spell = Aimtec.SDK.Spell;


    internal partial class Zed
    {

        public void LoadSpells()
        {
            SpellManager.Q = new Spell(SpellSlot.Q, 900f);
            SpellManager.W = new Spell(SpellSlot.W, 700f);
            SpellManager.E = new Spell(SpellSlot.E, 290f);
            SpellManager.R = new Spell(SpellSlot.R, 625f);
            SpellManager.Q.SetSkillshot(0.250f, 50f, 1700f, false, SkillshotType.Line);
            SpellManager.W.SetSkillshot(0.250f, 50f, 1750f, false, SkillshotType.Circle);
            
        }
    }
}
