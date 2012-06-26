using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace King_of_Thieves.Input
{
    class CXMLSerializer <T>
    {
        private XmlSerializer _xmlserializer;
        private T _input;

        public CXMLSerializer(T Input)
        {
            _input = Input;
            XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
        }

        public void Serialize(string path)
        {
            StreamWriter streamWriter = new StreamWriter(path);
            _xmlserializer.Serialize(streamWriter, _input);
            streamWriter.Close();
        }

        public T Load(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            T Output = (T)_xmlserializer.Deserialize(streamReader);
            streamReader.Close();
            return Output;
        }

    }
}
