using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Pexel
{

    [Serializable]
    public class InclPoint
    {
        public InclPoint() { X = 0.0; Y = 0.0; TVD = 0.0; MD = 0.0; }
        public InclPoint(double x, double y, double tvd, double md) { X = x; Y = y; TVD = tvd; MD = md; }
        public double X { set; get; }
        public double Y { set; get; }
        public double TVD { set; get; }
        public double MD { set; get; }
        public Point3D XYZ() { return new Point3D(X, Y, TVD); }


        public void Write(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(TVD);
            writer.Write(MD);
        }


        public static InclPoint Read(BinaryReader reader)
        {
            return new InclPoint(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }





    }
}
