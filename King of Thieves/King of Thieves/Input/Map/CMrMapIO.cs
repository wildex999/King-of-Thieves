using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using King_of_Thieves.Actors.Map;
namespace King_of_Thieves.Input
{
    /*
     * MAP DOCUMENTATION
     * DEVELOPED BY: JONATHAN BASNIAK
     * WHY IS THIS WRITTEN IN ALL CAPS? LOL? I AM DOING IT TO MAKE FUN OF THE 
     * OLD MACHINES THAT COULD DO CAPS BACK IN THE HIPPIE AGE. LOLOLOLOL!!
     * So anyways.. here's how this shit's gonna work:
     * 
     *              - The files will be in XML Format. ALWAYS.
     *              Why? We want an open human-readable format for anyone to pick up.
     *              We don't care about security. If the end-user wants that, well...
     *              we can provide a procedure for that, I guess.
     *              
     *              - Content Directory has to be set up a specific way. There will be a
     *              magic number in the XML file that will read in a tag. The tag will fire up
     *              Content.Load and it'll attempt to load in that file from content. There will be
     *              special flags for telling the engine whether or not to bug out. Like if a  
     *              resource is missing, should the engine just throw an exception? Yeah.
     *              
     *              - Layer cap is defined on a per-engine instance basis. That is, upon firing up
     *              the main map database, there will be a definition for what the layer restrictions
     *              are. Should this go undefined, we'll just default it to 0 - 255 layers and all
     *              will be good.
     *              
     *              - Hitboxes layer will be optional. It won't work like a tilemap, though. It'll
     *              just be a list of locations of hitboxes, what type of hitbox they are
     *              and their dimensions. This'll allow for odd-shaped hitboxes to be defined on the
     *              map.
     *              
     *              - CMrMap instances will be defined on a Root / Chunk basis. By default, MrMap
     *              defaults to 0, which is a root. Root and chunks are the same thing mostly except
     *              should a map be labeled as Chunk, then Root will _not_ have its own map. Instead
     *              it will contain a database of its chunks.
     *              
     *              - MrMap will want an object database eventually. I'm going to write an object
     *              database format that'll allow us to tell CActor who is on the map. MrMap will like this.
     *             
     */
    /* Structure:
     *          MAP CHUNK STRUCTURE
     *          0.00 I WROTE THIS WHILE BEING HALF AWAKE FUCK ME.
     *          <map>
     *          <version>written half awaken please dont sue me</version>
     *          <name>map.Name</name>
     *          <type>0</type>
     *          <layerCount>map.layerCount</layerCount>
     *          <tileset>map.tileset</tileset>
     *          
     *          <ID>
     *              <type>0</type> <!-- Type 0 == Event -->
     *              <ID#>per-mapID</ID#> <!-- This might not even make it because it might cause ID collision. Thinking of just having a coordinate here. -->
     *              <callback>objectNameOrTagMaybeHere?</callback>
     *          </ID>
     *          
     *           <tileLayer>   
     *              <layer#>i.ToString()</layer#>
     *              <mapData>0,0,0,0,0,0,0,0,0</mapData>
     *           </tileLayer>
     *           
     *           <hitBoxLayer>
     *              <layer#>i.ToString()</layer#>
     *              <type>0</type> <!-- Type 0 == Rectangle hitbox -->
     *              <hitBox>0,1,2,3</hitBox> <!-- Since we have a rectangle, only define x,y and width/height -->
     *           </hitBoxLayer>
     *          </map>
     *          
     *          MAP ROOT STRUCTURE
     *          <map>
     *          <version>written half awaken please dont sue me</version>
     *          <name>map.Name</name>
     *          <chunkCount>map.ChunkCount</chunkCount>
     *          
     *          <chunk>
     *              <file>map.GetChunk(ID)</file>
     *              <region>map.GetChunkRegion(ID)</region> <!-- Possibly a selection defining what positions this chunk makes up? -->
     *              <!-- Feels like we might need more for this chunk structure -->
     *          </chunk>
     *          </map>
     */
    class CMrMapIO
    {
        private string _mapName;

        public CMrMapIO(string name, int type)
        {
            _mapName = name;
        }

        public void Save(CMrMap map, string path)
        {
            CXMLSerializer<CMrMap> serializer = new CXMLSerializer<CMrMap>(map);
            serializer.Serialize(path);
        }

        public CMrMap Read(CMrMap map, string path)
        {
            CXMLSerializer<CMrMap> serializer = new CXMLSerializer<CMrMap>(map);
            CMrMap temp = serializer.Load(path);
            return temp;
        }
    }
}
