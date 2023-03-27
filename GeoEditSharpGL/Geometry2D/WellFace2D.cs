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
            Visible = false;
        }
        public WellFace2D(string title)
        {
            Title = title;
        }
        public string Title { set; get; }
        public Point2D Point { set; get; }
        public bool Visible { set; get; } = true;
        public bool Used { set; get; } = true;
        override public bool Checked
        {
            get
            {
                return Visible && Used;
            }
        }
        public WellStatus Status { set; get; } = WellStatus.PROD;
    }
}
