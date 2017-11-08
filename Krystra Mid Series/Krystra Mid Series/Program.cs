

using Aimtec;
using Aimtec.SDK.Events;
using System;
using System.Linq;

/*namespace Krystra_Mid_Series
{
   public static class Program
    {
       public static void Main()
        {
            if (Constants.ChampionNames.Contains(ObjectManager.GetLocalPlayer().ChampionName) && Game.Version.Substring(8, 4) == "7.20")
            {
                Console.WriteLine("Krystra Premium Series Succesfully Loaded.");
                Bootstrap.LoadOthers();
                Bootstrap.LoadChamps();

            }
            else
            {
                Console.WriteLine("Wrong Patch Version, Please contact Krystra");
            }
        }
       
    }
}*/
namespace Krystra_Mid_Series
{
    public static class Program
    {
        public static void Main()
        {
            GameEvents.GameStart += OnLoad;
        }

        private static void OnLoad()
        {
            if (Constants.ChampionNames.Contains(ObjectManager.GetLocalPlayer().ChampionName) && Game.Version.Substring(8, 4) == "7.21")
            {
                Console.WriteLine("Krystra Premium Series Succesfully Loaded.");
                Bootstrap.LoadOthers();
                Bootstrap.LoadChamps();

            }
            else
            {
                Console.WriteLine("Wrong Patch Version, Please contact Krystra");
            }
        }

    }
}