using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Pexel
{
    [Serializable]
    public class WellsPlane2D
    {
        public WellsPlane2D()
        {
            Wells = new List<WellFace2D>();
        }

        public int ID { set; get; } = 0;
        public List<WellFace2D> Wells { set; get; }


        public double MinX()
        {
            double result = Wells[0].Point.X;
            foreach (WellFace2D well in Wells)
                if (result > well.Point.X)
                    result = well.Point.X;
            return result;
        }
        public double MaxX()
        {
            double result = Wells[0].Point.X;
            foreach (WellFace2D well in Wells)
                if (result < well.Point.X)
                    result = well.Point.X;
            return result;
        }
        public double MinY()
        {
            double result = Wells[0].Point.Y;
            foreach (WellFace2D well in Wells)
                if (result > well.Point.Y)
                    result = well.Point.Y;
            return result;
        }
        public double MaxY()
        {
            double result = Wells[0].Point.Y;
            foreach (WellFace2D well in Wells)
                if (result < well.Point.Y)
                    result = well.Point.Y;
            return result;
        }


    }
}
