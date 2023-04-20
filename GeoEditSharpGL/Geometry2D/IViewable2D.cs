using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pexel.Geometry2D
{
    public class IViewable2D
    {
        public string Title { set; get; } = string.Empty;


        [JsonIgnore]
        public List<IViewable2D> Controllers { set; get; } = new List<IViewable2D>();

        [JsonIgnore]
        public List<IViewable2D> Controlled { set; get; } = new List<IViewable2D>();



        public bool _Visible { set;  get; } = true;

        [JsonIgnore]
        public bool Visible
        {
            set
            {
                _Visible = value;
            }
            get
            {
                return _Visible && 
                    (Controllers is null || Controllers.Count == 0 || Controllers.Where(v => v != this && v != null).All(x => x.Visible));
            }
        }


        public bool _Used { set; get; } = true;

        [JsonIgnore]
        public bool Used
        {
            set
            {
                _Used = value;
            }
            get
            {
                return _Used && 
                    (Controllers is null || Controllers.Count == 0 || Controllers.Where(v => v != this && v != null).All(x => x.Used));
            }
        }



    }
}
