using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;



namespace Pexel
{


    [Serializable]
    public class PropModifier
    {
        public PropModifier()
        {
            Value = 1;
            Radius = 0;
            Layers = new int[0];
            Applied = false;
            Point = new Point2D();
        }

       


        public bool Applied { set; get; }

        public Point2D Point { set; get; }

        public double Value { set; get; }
        
        public double Radius { set; get; }

        public int[] Layers { set; get; }




    }
}
