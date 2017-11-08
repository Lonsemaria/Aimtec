using Aimtec;
using Aimtec.SDK.TargetSelector;
using System.Collections.Generic;

namespace Krystra_Mid_Series.Champions
{
    internal partial class Zed

    {
        public Vector3 Wpos;
        public Vector3 Rpos;
        public int Wtimer;
        public int GR = 0;
        public int GW = 0;
        public double Rtimer;
        public int global_ticks = 0;
        public float StartTime = 0f;
        public int LastCastTime = 0;
        public float StartTimeR = 0f;
        public bool Wdmgp = false;
        public bool Rdmgcheck = false;
        public bool Rdmgp = false;
        public bool wgapclose = false;
        public Obj_AI_Hero target = Constants.target;
        public Obj_AI_Hero MyHero = Constants.MyHero;
        private static SpellSlot _Q = SpellSlot.Q;
        private static SpellSlot _W = SpellSlot.W;
        private static SpellSlot _E = SpellSlot.E;
        private static SpellSlot _R = SpellSlot.R;
        public static SpellSlot[] Level1 = { _Q, _W, _E, _Q, _Q, _R, _Q, _W, _Q, _W, _R, _W, _W, _E, _E, _R, _E, _E };
        public static SpellSlot[] Level2 = { _Q, _W, _E, _Q, _Q, _R, _Q, _E, _Q, _E, _R, _E, _E, _W, _W, _R, _W, _W };
        public static SpellSlot[] Level3 = { _W, _E, _Q, _W, _W, _R, _W, _Q, _W, _Q, _R, _Q, _Q, _E, _E, _R, _E, _E };
        public static SpellSlot[] Level4 = { _W, _E, _Q, _W, _W, _R, _W, _E, _W, _E, _R, _E, _E, _Q, _Q, _R, _Q, _Q };
        public static SpellSlot[] Level5 = { _E, _Q, _W, _E, _E, _R, _E, _W, _E, _W, _R, _W, _W, _Q, _Q, _R, _Q, _Q };
        public static SpellSlot[] Level6 = { _E, _Q, _W, _E, _E, _R, _E, _Q, _E, _Q, _R, _Q, _Q, _W, _W, _R, _W, _W };
        public static SpellSlot[] LevelSpecial = { _Q, _W, _E, _Q, _Q, _R, _Q, _E, _Q, _E, _R, _E, _E, _W, _W, _R, _W, _W};

    }

}

