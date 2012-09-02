using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using King_of_Thieves.Map;

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

    static class CMrMapIO
    {

        static public void Save(CMap map, string path)
        {
            CXMLSerializer<CMap> serializer = new CXMLSerializer<CMap>(map);
            serializer.Serialize(path);
        }

        static public CMap Read(CMap map, string path)
        {
            CXMLSerializer<CMap> serializer = new CXMLSerializer<CMap>(map);
            return serializer.Load(path);
        }
    }
}
