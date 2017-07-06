using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infraestructura.Soft
{
    public static class ExtString
    {
        public static T DeserializeXML<T>(this string xmlString)
        {
            T returnValue = default(T);
            XmlSerializer serial = new XmlSerializer(typeof(T));
            if (xmlString == null || xmlString == "") { return returnValue; }
            using (StringReader reader = new StringReader(xmlString))
            {
                object result = serial.Deserialize(reader);
                if (result != null && result is T)
                {
                    returnValue = ((T)result);
                }
                reader.Close();
            }
            return returnValue;
        }
    }
}
