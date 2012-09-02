using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace King_of_Thieves.Map
{
    /* Structure:
     *          MAP CHUNK STRUCTURE
     *          0.00 I WROTE THIS WHILE BEING HALF AWAKE FUCK ME.
     *          <map>
     *          <version>written half awaken please dont sue me</version>
     *          <name>testmap</name>
     *          <type>0</type>
     *          <layerCount>0</layerCount>
     *          <tileset>testTiles</tileset>
     *          
     *          <!-- <ID> is subject for scrapping now.
     *          <ID>
     *              <type>0</type> <!-- Type 0 == Event -->
     *              <position>0,0</position>
     *              <callback>objectNameOrTagMaybeHere</callback>
     *          </ID> -->
     *          
     *           <tileLayer>   
     *              <layer#>0</layer#>
     *              <mapData>0,0,0,0,0,0,0,0,...,</mapData>
     *           </tileLayer>
     *           
     *           <hitBoxLayer>
     *              <layer#>0</layer#>
     *              <type>0</type> <!-- Type 0 == Rectangle hitbox -->
     *              <hitBox>0,1,2,3</hitBox> <!-- Since we have a rectangle, only define x,y and width/height -->
     *           </hitBoxLayer>
     *           
     *          <objectLayer>
     *              <layer#>0</layer#>
     *              <objectData>0,0,0,0,...,</objectData>
     *          </objectLayer>
     *          </map>
     *          
     *          MAP ROOT STRUCTURE
     *          <map>
     *          <version>written half awaken please dont sue me</version>
     *          <name>testMap</name>
     *          <chunkCount>0</chunkCount>
     *          
     *          <chunk number=0>
     *              <file>testMapLeft</file>
     *              <region>0,0,200,200</region> <!-- Possibly a selection defining what positions this chunk makes up? -->
     *              <!-- Feels like we might need more for this chunk structure -->
     *          </chunk>
     *          </map>
     */
    [XmlRoot("map")]
    public class CMap
    {
        //<Version>
        [XmlElement(DataType = "int", ElementName = "version")]
        public int Version;

        //<Name>
        [XmlElement(DataType="string", ElementName="name")]
        public string Name;

        //<Type>
        [XmlElement(ElementName="type")]
        public MAPTYPES Type;

        //<layerCount>
        [XmlElement(DataType="int",ElementName="layerCount")]
        public int layerCount;

        //<tileSet>
        [XmlElement(DataType="string",ElementName="tileSet")]
        public string tileSet;

        //<ID>
        /*
        [XmlElement("ID")]
        public CSpecialID ID
        {
            get; set;
        } */

        //<tileLayer>
        [XmlElement("tileLayer")]
        public CTileLayer tileLayer;

        //<hitBoxLayer>
        [XmlElement("hitBoxLayer")]
        public CHitBoxType hitBoxLayer;

        //<objectLayer>
        [XmlElement("objectLayer")]
        public CObjectLayer objectLayer;
    }

    public class CTileLayer
    {
        [XmlElement("layerNum")]
        public int layerNum;

        [XmlArray(ElementName="tile",IsNullable=true)]
        public int[] tileData;
    }

    public class CObjectLayer
    {
        [XmlElement("layerNum")]
        public int layerNum;
        [XmlArray(ElementName="objectData",IsNullable=true)]
        public int[] objectData;
    }

    public class CSpecialID
    {
        [XmlElement("type")]
        public int Type;

        [XmlElement("Position")]
        public int[] Position;

        [XmlElement("callback")]
        public object Callback;
    }

    public class CHitBoxType
    {
        [XmlElement("layerNum")]
        public int layerNum
        {
            get; set;
        }
        [XmlElement("type")]
        public int Type
        {
            get; set;
        }
        [XmlArray(ElementName = "hitBox",IsNullable=true)]
        public int[] hitBox
        {
            get; set;
        }
    }

    [XmlRoot("Root")]
    public class CMapRoot
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
    public class CMapChunk
    {
        [XmlElement("file")]
        public string file
        {
            get; set;
        }

        [XmlArray(ElementName = "region",IsNullable=true)]
        public int[] Region
        {
            get; set;
        }
    }

}
