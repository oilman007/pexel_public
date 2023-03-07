using Newtonsoft.Json;
using Pexel.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.SCAL
{
    public class CoreySet
    {
        public CoreySet() { }
        public CoreySet(int ntables, bool swof, bool sgof, bool export_corey = false)
        {
            Tables.Clear();
            for (int i = 0; i < ntables; i++)
                Tables.Add(new CoreyTable());
            SWOF = swof;
            SGOF = sgof;
            ExportAsCorey = export_corey;
        }

        [CategoryAttribute("Export Settings"), DescriptionAttribute("Export SWOF/COREYWO table"), BrowsableAttribute(false), ReadOnlyAttribute(false)]
        public List<CoreyTable> Tables { set; get; } = new List<CoreyTable>();

        [CategoryAttribute("Export Settings"), DescriptionAttribute("Export SWOF/COREYWO table"), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public bool SWOF { set; get; } = true;

        [CategoryAttribute("Export Settings"), DescriptionAttribute("Export SGOF/COREYGO table"), BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public bool SGOF { set; get; } = true;

        [CategoryAttribute("Export Settings"), 
            DescriptionAttribute("Export SWOF/SGOF keywords (if 'false'), export COREYWO/COREYGO keywords (if 'true')"),
            BrowsableAttribute(true), ReadOnlyAttribute(false)]
        public bool ExportAsCorey { set; get; } = false;


        public const string SAVE_EXT = "COREY";
        public const string EXPORT_EXT = "TXT";



        static public bool Load(string filename, out CoreySet result)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto
                };
                CoreySet temp = new CoreySet();
                using (StreamReader r = new StreamReader(filename))
                {
                    string json = r.ReadToEnd();
                    temp = JsonConvert.DeserializeAnonymousType(json, temp, settings);
                }
                result = temp;
            }
            catch (Exception ex)
            {
                result = new CoreySet();
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


        public bool Export(string file, params string[] comments)
        {
            if(ExportAsCorey)
                return ExportTnavCorey(file, comments);
            return ExportTables(file, comments);
        }




        bool ExportTables(string file, params string[] comments)
        {
            const int w = 20;
            try
            {
                List<string> result = new List<string>();
                if (comments.Length > 0)
                    result.Add("-- " + comments);
                if (SWOF)
                {
                    result.Add("");
                    result.Add("SWOF");
                    foreach (CoreyTable table in Tables)
                    {
                        double[][] swof = table.SWOF(out int ncol, out int nrow);
                        for (int r = 0; r < nrow; ++r)
                        {
                            string[] values = new string[ncol];
                            for (int c = 0; c < ncol; ++c)
                                values[c] = $"{Helper.ShowDouble(swof[c][r]),w}";
                            string line = string.Join("\t", values);
                            result.Add(line);
                        }
                        result.Add("/");
                    }
                }
                if (SGOF)
                {
                    result.Add("");
                    result.Add("SGOF");
                    foreach (CoreyTable table in Tables)
                    {
                        double[][] sgof = table.SGOF(out int ncol, out int nrow);
                        for (int r = 0; r < nrow; ++r)
                        {
                            string[] values = new string[ncol];
                            for (int c = 0; c < ncol; ++c)
                                values[c] = $"{Helper.ShowDouble(sgof[c][r]),w}";
                            string line = string.Join("\t", values);
                            result.Add(line);
                        }
                        result.Add("/");
                    }
                }
                System.IO.File.WriteAllLines(file, result.ToArray());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }




        bool ExportTnavCorey(string file, params string[] comments)
        {
            try
            {
                List<string> result = new List<string>();
                if (comments.Length > 0)
                    result.Add("-- " + comments);
                if (SWOF)
                {
                    result.Add("");
                    result.Add("--SWL\tSWU\tSWCR\tSOWCR\tKROLW\tKRORW\tKRWR\tKRWU\tPCW\tNOW\tNW\tNPW\t1-SOWCR-SGL");
                    result.Add("COREYWO");
                    foreach (CoreyTable table in Tables)
                    {
                        double[] coreywo = table.COREYWO();
                        string line = string.Join("\t", coreywo.Select(v => Helper.ShowDouble(v))) + "\t/";
                        result.Add(line);
                    }
                    result.Add("/");
                }
                if (SGOF)
                {
                    result.Add("");
                    result.Add("--SGL\tSGU\tSGCR\tSOGCR\tKROLG\tKRORG\tKRGR\tKRGU\tPCG\tNOG\tNG\tNPOG\tSGCR");
                    result.Add("COREYGO");
                    foreach (CoreyTable table in Tables)
                    {
                        double[] coreygo = table.COREYGO();
                        string line = string.Join("\t", coreygo.Select(v => Helper.ShowDouble(v))) + "\t/";
                        result.Add(line);
                    }
                    result.Add("/");
                }
                System.IO.File.WriteAllLines(file, result.ToArray());
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }





        public bool ExportAndSave(string export_file)
        {
            return Export(export_file) && Save(Path.ChangeExtension(export_file, SAVE_EXT));
        }





        public static bool ImportCOREYWO(string file, out CoreySet result)
        {
            EclFile eclFile = new EclFile(file);
            eclFile.GetWordsArrays("COREYWO", out string[][] values, ContentType.KEYWORD, ContentType.END_OF_FILE);
            result = new CoreySet();
            try
            {
                foreach (string[] line in values)
                    result.Tables.Add(new CoreyTable(line.Select(x => Helper.ParseDouble(x)).ToArray()));
            }
            catch
            {
                return false;
            }
            return true;
        }





    }
}
