using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Krystra_Mid_Series
{
    internal class Language
    {
        public static string GetMenu(string first,string third)
        {
            string deneme = "";
            try
            {
                using (var steamd = Assembly.GetExecutingAssembly().GetManifestResourceStream("Krystra_Mid_Series.Json.Data.json"))
                {
                    
                    if (steamd != null)
                    {
                        using (var r = new StreamReader(steamd))
                        {
                            string j = r.ReadToEnd();
                           // Console.WriteLine(j);
                            string second = "eng";
                            if (ReadTxt() == "eng")
                            {
                                second = "English";
                            }
                            else if (ReadTxt() == "chy")
                            {
                                second = "Chinese";
                            }
                            JObject json = JObject.Parse(j);
                            deneme = json[first][second][third].ToString();
                            // dynamic data = JsonConvert.DeserializeObject(elements[0].Text);
                            //Console.WriteLine(deneme);

                        }
                    }
                    else
                    {
                        Console.WriteLine("null");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error");
            }
            return deneme;
        }
        public static void WriteTxt(string lang)
        {
            var link = Environment.GetEnvironmentVariable("LocalAppData");
            var filePath = link + @"\AimtecLoader\Data\System\Lang.txt";
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(lang);
            //sw.Flush();
            sw.Close();
            fs.Close();
        }
        public static string ReadTxt()
        {
            var link = Environment.GetEnvironmentVariable("LocalAppData");
            var filePath = link + @"\AimtecLoader\Data\System\Lang.txt";
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            return yazi;
            sw.Close();
            fs.Close();
        }
    }
}
