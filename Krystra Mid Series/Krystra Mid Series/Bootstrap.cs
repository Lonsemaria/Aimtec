using Krystra_Mid_Series.Champions;
using System;
using System.IO;

namespace Krystra_Mid_Series
{
   internal class Bootstrap
    {
        #region Champion Loading
        public static void LoadChamps()
        {
            switch (Constants.MyHero.ChampionName)
            {
                case "Leblanc":
                   // new Leblanc();
                    break;
                case "Orianna":
                    //new Orianna();
                    break;
                case "Diana":
                    //new Diana();
                    break;
                case "Annie":
                    //new Annie();
                    break;
                case "Zed":
                    new Zed();
                    break;
                case "Lissandra":
                    //new Lissandra();
                    break;
                default:
                    Console.WriteLine("Champion Selection Error");
                    break;
            }
        }
        #endregion

        #region NecesseryLoadings
        public static void LoadOthers()
        {
            var link = Environment.GetEnvironmentVariable("LocalAppData");
            var filePath = link + @"\AimtecLoader\Data\System\Lang.txt";
            if (!File.Exists(filePath))
            {
                Language.WriteTxt("eng");
            }
            SpellManager.SpellLoading();
            GeneralMenu.Loading();
            
        }

#endregion
    }
}
