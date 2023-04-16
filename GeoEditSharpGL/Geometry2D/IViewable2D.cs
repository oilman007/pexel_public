using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Geometry2D
{
    public class IViewable2D
    {

        public string Title { set; get; } = string.Empty;


        public IViewable2D Parent { set; get; } = null;

        public List<IViewable2D> Children { set; get; } = new List<IViewable2D>();


        /*
        public bool Enabled 
        {
            get
            {
                return Visible && Used && Active && (Parent is null || Parent.Enabled);
            }
        }
        */


        public bool _Visible { protected set;  get; } = true;
        public bool Visible
        {
            set
            {
                _Visible = value;
            }
            get
            {
                return _Visible && (Parent is null || Parent.Visible);
            }
        }



        public bool _Used { protected set; get; } = true;
        public bool Used
        {
            set
            {
                _Used = value;
            }
            get
            {
                return _Used && (Parent is null || Parent.Visible);
            }
        }



    }
}
