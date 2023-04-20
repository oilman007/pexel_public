using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pexel;
using Pexel.General;

namespace TestProject1
{
    public class SerializationTest
    {
        [Fact]
        public void ColorWrite()
        {
            //BSerializer<Color> serializer = new BSerializer<Color>();

            Assert.Equal(2.83, Math.Round(Line2D.Distance(new Point2D(1, 2), new Point2D(3, 4)), 2));
        }
    }
}
