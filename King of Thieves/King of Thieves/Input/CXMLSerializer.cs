using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace King_of_Thieves.Input
{
    static class CXMLSerializer <T>
    {
        private XmlSerializer _xmlserializer;
        private object _stream;
        private object _input;

        public CXMLSerializer(object Input)
        {
            _input = Input;
            XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
        }

        public void Serialize()
        {
            StreamWriter streamWriter = new StreamWriter();
            _xmlserializer.Serialize(streamWriter, _input);
            streamWriter.Close();
        }

        public T Load()
        {
            StreamReader streamReader = new StreamReader();
            T Output = (T)_xmlserializer.Deserialize(streamReader);
            streamReader.Close();
            return Output;
        }
    }
}
