using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace Pexel.General
{
    [DataContract]
    public class ColorClass
    {

        public ColorClass() { }

        public ColorClass(Color color)
        {
            R = color.R; G = color.G; B = color.B; A = color.A;
        }

        [DataMember]
        public byte R { set; get; } = 0;

        [DataMember]
        public byte G { set; get; } = 0;

        [DataMember]
        public byte B { set; get; } = 0;

        [DataMember]
        public byte A { set; get; } = 0;


        public Color GetColor()
        {
            return Color.FromArgb(A, R, G, B);
        }
    }
}
