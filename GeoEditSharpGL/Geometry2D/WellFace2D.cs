using Pexel.Geometry2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pexel
{
    [Serializable]
    public class WellFace2D : IViewable2D
    {
        public WellFace2D()
        {
            Title = string.Empty;
        }
        public WellFace2D(string title)
        {
            Title = title;
        }

        public WellFace2D(string title, Point2D point, bool visible, bool used, WellStatus status) : this(title)
        {
            Point = point;
            Visible = visible;
            Used = used;
            Status = status;
        }



        public string Title { set; get; }
        public Point2D Point { set; get; }


        public WellStatus Status { set; get; } = WellStatus.PROD;

        public override bool Equals(object obj)
        {
            return obj is WellFace2D d &&
                   Title == d.Title &&
                   Point2D.Equals(Point, d.Point) &&
                   Used == d.Used &&
                   Status == d.Status;
        }
    }
}
