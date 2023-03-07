using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Pexel.Modifiers
{
    class RadiusModifier : Modifiers.Modifier
    {
        public RadiusModifier() : base()
        {
            Value = 1;
            Radius = 0;
            Layer = 0;
            Use = true;
            Location = new Point2D();
        }
        public RadiusModifier(Point2D location, int layer, double value, double radius, bool use = true) : base()
        {
            Radius = radius;
            Value = value;
            Layer = layer;
            Use = use;
            Location = new Point2D(location.X, location.Y);
        }
        RadiusModifier(string[] split): base()
        {
            Value = Helper.ParseDouble(split[1]);
            Radius = Helper.ParseDouble(split[2]);
            Layer = Helper.ParseInt(split[5]);
            Use = false;
        }



        public double Value { set; get; }
        public double Radius { set; get; }
        public int Layer { set; get; }
        public Point2D Location { set; get; }





        const byte Version0 = 0;
        new public void Write(BinaryWriter writer)
        {
            writer.Write(Version0);
            writer.Write(Location.X);
            writer.Write(Location.Y);
            writer.Write(Layer);
            writer.Write(Value);
            writer.Write(Radius);
            writer.Write(Use);
        }



        new static public Modifier Read(BinaryReader reader)
        {
            byte version = reader.ReadByte();
            switch (version)
            {
                case Version0:
                    {
                        Point2D location = new Point2D(reader.ReadDouble(), reader.ReadDouble());
                        int layer = reader.ReadInt32();
                        double value = reader.ReadSingle();
                        double radius = reader.ReadSingle();
                        bool use = reader.ReadBoolean();
                        return new RadiusModifier(location, layer, value, radius, use);
                    }
                default:
                    return new RadiusModifier();
            }
        }



        public string TextForm()
        {
            return Layer.ToString() + '\t' +
                    Value.ToString() + '\t' +
                    Radius.ToString();
        }


        public const string TextHeader = " --name\tvalue\trad\tx\ty\tk";






    }
}
