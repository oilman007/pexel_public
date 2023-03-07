using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace Pexel
{
    [Serializable]
    public class SimpleWell
    {
        public SimpleWell()
        {
            Name = "Well_" + (Namber++).ToString();
            Location = new PointF();
            Checked = true;
        }
        public SimpleWell(string name, PointF location, bool check)
        {
            Name = name;
            Location = location;
            Checked = check;
        }


        public string Name { set; get; }
        public PointF Location { set; get; }

        public bool Checked { set; get; }

        static long Namber = 1;



        public void Write(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(Location.X);
            writer.Write(Location.Y);
            writer.Write(Checked);
        }

        public static SimpleWell Read(BinaryReader reader)
        {
            string name = reader.ReadString();
            PointF location = new PointF(reader.ReadSingle(), reader.ReadSingle());
            bool check = reader.ReadBoolean();
            return new SimpleWell(name, location, check);
        }



    }
}
