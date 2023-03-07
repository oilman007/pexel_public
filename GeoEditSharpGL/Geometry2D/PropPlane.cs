using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace Pexel
{
    public class PropPlane
    {
        public PropPlane()
        {
            Values = new double[0, 0];
        }

        public PropPlane(int nx, int ny)
        {
            Values = new double[nx, ny];
        }
        

        public double[,] Values { set; get; }

        


        public int NX()
        {
            return Values.GetLength(0);
        }

        public int NY()
        {
            return Values.GetLength(1);
        }



    }
}
