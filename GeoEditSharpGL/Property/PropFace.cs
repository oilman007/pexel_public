using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Pexel
{
    public class PropFace
    {
        public PropFace() { Face = new CellFace(); }

        public PropFace(CellFace face, double value)
        {
            Face = face;
            Value = value;
        }

        public CellFace Face { set; get; }

        public double Value { set; get; }
    }
}
