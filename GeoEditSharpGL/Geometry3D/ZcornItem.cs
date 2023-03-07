using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;



namespace Pexel
{
    [Serializable]
    public class ZcornItem
    {


                /*
                     Y
                    /
                   /
                  /
                 2----3
                /|   /|
               / |  / |
              /  6-/- 7
             /  / /  /
            0----1------------------------ X
            | /  | /
            |/   |/
            4----5
            |
            |
            |
            |
            |
            Z
        */


        public ZcornItem()
        {
            Corners = new double[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }



        public ZcornItem(double c0, double c1, double c2, double c3, double c4, double c5, double c6, double c7)
        {
            Corners = new double[8] { c0, c1, c2, c3, c4, c5, c6, c7 };
        }





        public double[] Corners { set; get; }






        public void Write(BinaryWriter writer)
        {
            for (int i = 0; i < 8; ++i)
                writer.Write(Corners[i]);
        }

        public static ZcornItem Read(BinaryReader reader)
        {
            return new ZcornItem(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(),
                                 reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
        }


    }
}
