using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace King_of_Thieves.Actors.Map
{
    [XmlRoot("Map")]
    class CMap
    {
        //<Version>
        [XmlElement("version")]
        public int Version
        {
            get; set;
        }

        //<Name>
        [XmlElement("name")]
        public string Name
        {
            get; set;
        }

        //<Type>
        [XmlElement("type")]
        public int Type
        {
            get; set;
        }

        //<layerCount>
        [XmlElement("layerCount")]
        public int layerCount
        {
            get; set;
        }

        //<tileSet>
        [XmlElement("tileSet")]
        public string tileSet
        {
            get; set;
        }

        //<ID> id, object ref
        [XmlElement("ID")]
        public CSpecialID ID
        {
            get; set;
        }

        //<tileLayer>
        public Dictionary<int, int[]> tileLayer
        {
            get; set;
        }

        //<hitBoxLayer>
        public CHitBoxType hitBoxLayer
        {
            get; set;
        }

    }

    class CSpecialID
    {
        [XmlElement("type")]
        public int Type
        {
            get; set;
        }
        [XmlElement("ID#")]
        public int ID
        {
            get; set;
        }
        [XmlElement("callback")]
        public object Callback
        {
            get; set;
        }
    }

    class CHitBoxType
    {
        [XmlElement("layer#")]
        public int layerNum
        {
            get; set;
        }
        [XmlElement("type")]
        public int Type
        {
            get; set;
        }
        [XmlArray(ElementName = "hitBox")]
        public int[][] hitBox
        {
            get; set;
        }
    }

    [XmlRoot("Root")]
    class CMapRoot
    {
        [XmlElement("version")]
        public int Version
        {
            get; set;
        }
        [XmlElement("name")]
        public string Name
        {
            get; set;
        }
        [XmlElement("chunkCount")]
        public int chunkCount;
    }

    [XmlRoot("Chunk")]
    class CMapChunk
    {
        [XmlElement("file")]
        public string file
        {
            get; set;
        }

        [XmlArray(ElementName = "region")]
        public int[][] Region
        {
            get; set;
        }
    }

}
