using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.ConstrainedExecution;

namespace Pexel.General
{
    public class BSerializer<T> where T : class
    {


        public static string Serialize(T obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, obj);
                    return sww.ToString();
                }
            }
        }


        public static T Deserialize(string path)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (XmlReader reader = XmlReader.Create(path))
            {
                return (T)xsSubmit.Deserialize(reader);
            }
        }





    }
}
