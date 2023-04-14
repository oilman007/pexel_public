using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Geometry2D
{
    public class IViewable2D
    {
        public bool Visible { set; get; } = true;
        public bool Used { set; get; } = true;
        public bool Active { set; get; } = true;


        public IViewable2D Parent { set; get; } = null;


        public bool Enabled 
        {
            get
            {
                return Visible && Used && Active && (Parent is null || Parent.Enabled);
            }
        }
    }
}
