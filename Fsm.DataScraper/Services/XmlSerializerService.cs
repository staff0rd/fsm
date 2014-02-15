using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fsm.DataScraper.Services
{
    public class XmlSerializerService
    {
        static public void Serialize<T>(T obj, string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, obj);
            textWriter.Close();
        }

        public static T Deserialize<T>(string path)
        {
            var deserializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StreamReader(path);
            var result = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return result;
        }
    }
}
