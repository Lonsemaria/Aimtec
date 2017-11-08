

using Aimtec;
using Aimtec.SDK.Orbwalking;

namespace Krystra_Mid_Series
{
   internal class Constants
    {
        public static string[] ChampionNames = new string[] { "Leblanc", "Zed" ,"Lissandra","Orianna","Annie","Diana"};
        public static Obj_AI_Hero MyHero => ObjectManager.GetLocalPlayer();
        public static Obj_AI_Hero target;
        public static string ScriptName = "Krystra Premium Series";
        public static string ScriptVersion = "Free Trial";
        public static IOrbwalker IOrbwalker => Orbwalker.Implementation;

    }
}
