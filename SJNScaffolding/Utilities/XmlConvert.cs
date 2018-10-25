using System;
using System.IO;
using System.Xml.Serialization;

namespace SJNScaffolding.Utilities
{
    public class XmlConvert
    {
        public static T Deserialize<T>(string xmlPath)
        {
            using (var xmlStream = File.OpenText(xmlPath))
            {
                Type targetType = typeof(T);
                XmlSerializer xmldes = new XmlSerializer(targetType);
                return (T)xmldes.Deserialize(xmlStream);
            }
        }
    }
}
