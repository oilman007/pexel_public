using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;

namespace Pexel.HM
{
    public class ResultsViewSettings
    {
        public double OPRDeltaPercent { set; get; } = 10;
        public double WPRDeltaPercent { set; get; } = 10;
        public double GPRDeltaPercent { set; get; } = 10;
        public double LPRDeltaPercent { set; get; } = 10;

        public double OPTDeltaPercent { set; get; } = 10;
        public double WPTDeltaPercent { set; get; } = 10;
        public double GPTDeltaPercent { set; get; } = 10;
        public double LPTDeltaPercent { set; get; } = 10;

        public double OIRDeltaPercent { set; get; } = 10;
        public double WIRDeltaPercent { set; get; } = 10;
        public double GIRDeltaPercent { set; get; } = 10;

        public double OITDeltaPercent { set; get; } = 10;
        public double WITDeltaPercent { set; get; } = 10;
        public double GITDeltaPercent { set; get; } = 10;

        public double WBHPDeltaPercent { set; get; } = 20;
        public double ResPressDeltaPercent { set; get; } = 20;


        public double TargetWellsPercent { set; get; } = 80;        
        public string WellSufix { set; get; } = "";

        public Color OPRColor { set; get; } = Color.Red;
        public Color WPRColor { set; get; } = Color.Blue;
        public Color GPRColor { set; get; } = Color.Orange;
        public Color LPRColor { set; get; } = Color.Green;

        public Color OPTColor { set; get; } = Color.Red;
        public Color WPTColor { set; get; } = Color.Blue;
        public Color GPTColor { set; get; } = Color.Orange;
        public Color LPTColor { set; get; } = Color.Green;

        public Color OIRColor { set; get; } = Color.MediumVioletRed;
        public Color WIRColor { set; get; } = Color.DarkBlue;
        public Color GIRColor { set; get; } = Color.GreenYellow;

        public Color OITColor { set; get; } = Color.MediumVioletRed;
        public Color WITColor { set; get; } = Color.DarkBlue;
        public Color GITColor { set; get; } = Color.GreenYellow;
        
        public Color WCTColor { set; get; } = Color.DarkGray;
        public Color GORColor { set; get; } = Color.Orange;
        public Color WBHPColor { set; get; } = Color.Orange;
        public Color WBP9Color { set; get; } = Color.Violet;
        public Color HMDevColor { set; get; } = Color.Orchid;

        public int DecimalPlaces { set; get; } = 1;

        public bool ShowWellNamesCP { set; get; } = true;

        public float CrossPlotFontSpec { set; get; } = 11f;

        public float GraphFontSpec { set; get; } = 11f;


        static public bool Load(string filename, out ResultsViewSettings result)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
                ResultsViewSettings temp = new ResultsViewSettings();
                using (StreamReader r = new StreamReader(filename))
                {
                    string json = r.ReadToEnd();
                    temp = JsonConvert.DeserializeAnonymousType(json, temp, settings);
                }
                result = temp;
            }
            catch (Exception ex)
            {
                result = new ResultsViewSettings();
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
