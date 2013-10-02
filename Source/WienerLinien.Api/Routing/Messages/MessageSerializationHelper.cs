using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WienerLinien.Api.Routing.Messages
{
    public static class MessageSerializationHelper
    {
        public static T DeserializeFromString<T>(string data)
        {
            using (var stringReader = new StringReader(data))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}
