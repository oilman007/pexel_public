using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace Pexel
{
    public class Index3D // : IProjectable
    {
        public Index3D()
        {
            I = 0;
            J = 0;
            K = 0;
        }

        public Index3D(int i, int j, int k)
        {
            I = i;
            J = j;
            K = k;
        }


        public int I { set; get; }
        public int J { set; get; }
        public int K { set; get; }






        public void Write(BinaryWriter writer)
        {
            writer.Write(I);
            writer.Write(J);
            writer.Write(K);
        }


        public static Index3D Read(BinaryReader reader)
        {
            return new Index3D(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }



        public Index2D ToIndex2D()
        {
            return new Index2D(I, J);
        }


        static public bool operator ==(Index3D i1, Index3D i2)
        {
            if (i1 is null || i2 is null) return false;
            return (i1.I == i2.I) && (i1.J == i2.J) && (i1.K == i2.K);
        }
        static public bool operator !=(Index3D i1, Index3D i2)
        {
            if (i1 is null || i2 is null) return true;
            return (i1.I != i2.I) || (i1.J != i2.J) || (i1.K != i2.K);
        }


        override public string ToString()
        {
            return $"[{Helper.ShowInt(I)},{Helper.ShowInt(J)},{Helper.ShowInt(K)}]";
        }


        public int Number(int nx, int ny)
        {
            return I + J * nx + K * nx * ny;
        }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType()) return false;
            Index3D i = obj as Index3D;
            return (i.I == I) && (i.J == J) && (i.K == K);
        }

        public override int GetHashCode()
        {
            int hCode = I ^ J ^ K;
            return hCode.GetHashCode();
        }

    }
}
