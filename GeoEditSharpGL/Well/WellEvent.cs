using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Pexel
{

    [Serializable]
    public class WellEvent
    {

        public WellEvent()
        {
            WellName = string.Empty;
            Date = DateTime.Parse("01.01.1900");
            Type = string.Empty;
            Top = 0;
            Bottom = 0;
            Diameter = 0;
            Skin = 0;
            Mult = 1;
        }


        public WellEvent(string wellname, DateTime date, string type, double top, double bottom, double diametr, double skinFactor, double mult)
        {
            WellName = wellname;
            Date = date;
            Type = type;
            Top = top;
            Bottom = bottom;
            Diameter = diametr;
            Skin = skinFactor;
            Mult = mult;
        }


        


        public string WellName { set; get; }
        public DateTime Date { set; get; }
        public string Type { set; get; }
        public double Top { set; get; }
        public double Bottom { set; get; }
        public double Diameter { set; get; }
        public double Skin { set; get; }
        public double Mult { set; get; }
        




        public void Write(BinaryWriter writer)
        {
            writer.Write(WellName);
            writer.Write(Date.ToString());
            writer.Write(Type.ToString());
            writer.Write(Top);
            writer.Write(Bottom);
            writer.Write(Diameter);
            writer.Write(Skin);
        }


        public static WellEvent Read(BinaryReader reader)
        {
            return new WellEvent(reader.ReadString(), Helper.ParseDateTime(reader.ReadString()), reader.ReadString(), reader.ReadDouble(),
                                 reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }



        static public List<WellEvent> Read(string file)
        {
            List<WellEvent> result = new List<WellEvent>();
            try
            {
                string[] lines = Helper.ClearLines(file);
                foreach (string line in lines)
                {
                    string[] split = line.Split();
                    int count = split.Length;
                    string wellname = split[0];
                    DateTime date = Helper.ParseDateTime(split[1]);
                    string ev = split[2];
                    double dl = Helper.ParseDouble(split[3]);
                    double du = Helper.ParseDouble(split[4]);
                    double diam = Helper.ParseDouble(split[5]);
                    double skin = Helper.ParseDouble(split[6]);
                    double mult = Helper.ParseDouble(split[7]);
                    result.Add(new WellEvent(wellname, date, ev, dl, du, diam, skin, mult));
                }
            }
            catch { return result; }
            return result;
        }




    }
}
