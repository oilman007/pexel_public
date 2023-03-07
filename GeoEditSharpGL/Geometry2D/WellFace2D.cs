using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pexel
{
    [Serializable]
    public class WellFace2D
    {
        public WellFace2D()
        {
            Title = string.Empty;
            Checked = false;
        }
        public WellFace2D(string title)
        {
            Title = title;
        }
        public string Title { set; get; }
        public Point2D Point { set; get; }
        public bool Checked { set; get; } = true;
        public WellStatus Status { set; get; } = WellStatus.PROD;
    }
}
