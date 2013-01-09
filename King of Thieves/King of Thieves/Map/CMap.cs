using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace King_of_Thieves.Map
{

    class CMap
    {
        private string _name = "";

        public CMap(string fileName)
        {

            XmlTextReader reader = new XmlTextReader(fileName);

            reader.Read(); reader.Read(); reader.Read();
            if (reader.Name != "map")
                throw new FormatException("The xml file was not in the proper map format.");

            reader.Read(); reader.Read(); reader.Read();
            _name = reader.Value;

            reader.Close();
            reader = null;
            
        }
 
    }

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
     */

   

}
