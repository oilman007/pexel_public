using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.HM
{
    public class HistMatchingProject
    {
        public string[] PXLHMFiles { get; set; } = Array.Empty<string>();   

        public const string Identifier = "PXLPT";

        static public bool Load(string filename, out HistMatchingProject result)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
                HistMatchingProject temp = new HistMatchingProject();
                using (StreamReader r = new StreamReader(filename))
                {
                    string json = r.ReadToEnd();
                    temp = JsonConvert.DeserializeAnonymousType(json, temp, settings);
                }
                result = temp;
            }
            catch (Exception ex)
            {
                result = new HistMatchingProject();
                return false;
            }
            return true;
        }




        public bool Save(string filename)
        {
            try
            {
                using (StreamWriter file = File.CreateText(filename))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.Auto
                    };
                    serializer.Serialize(file, this);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }





    }
}
