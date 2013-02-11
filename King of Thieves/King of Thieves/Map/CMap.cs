using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using King_of_Thieves.Actors;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CMap : Gears.Cartography.Map
    {
        private int _componentCount = 0;
        private CLayer[] _layers = null;
        private static Regex _coordFormat = new Regex("^[0-9]+:[0-9]+$");
        private static Regex _valSplitter = new Regex(":");

        public CMap(string fileName) :
            base(Gears.Cartography.Map.deserializeFromXml(fileName))
        {
            _layers = new CLayer[base.NUM_LAYERS];
            int layerCount = 0;
            foreach (Gears.Cartography.layer layer in base.LAYERS)
            {
                
                uint componentAddresses = 0;
                uint componentCount = 0;
                Actors.CComponent[] compList = new CComponent[layer.COMPONENTS.Count()];

                //=======================================================================
                //Tiles
                //=======================================================================
                CTile[] tiles = new CTile[layer.TILES.Count()];
                int tileCounter = 0;
                foreach (Gears.Cartography.tile tile in layer.TILES)
                {
                    if (!_coordFormat.IsMatch(tile.COORDS))
                        throw new FormatException("The coordinate format provided was not valid.\n" + "Tile: " + tile.COORDS);

                    if (!_coordFormat.IsMatch(tile.TILESELECTION))
                        throw new FormatException("The coordinate format provided was not valid.\n" + "Tile: " + tile.TILESELECTION);

                    Vector2 atlasCoords = new Vector2((float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTION)[0]),
                                                      (float)Convert.ToDouble(_valSplitter.Split(tile.TILESELECTION)[1]));
                    Vector2 mapCoords =   new Vector2((float)Convert.ToDouble(_valSplitter.Split(tile.COORDS)[0]),
                                                      (float)Convert.ToDouble(_valSplitter.Split(tile.COORDS)[1]));

                    tiles[tileCounter++] = new CTile(atlasCoords,mapCoords,tile.TILESET);
                }

                //=======================================================================
                //Components
                //=======================================================================
                foreach (Gears.Cartography.component component in layer.COMPONENTS)
                {
                    CComponent tempComp = new CComponent(componentAddresses++);

                    foreach (Gears.Cartography.actors actor in component.ACTORS)
                    {
                        Type actorType = actor.actor;
                        CActor tempActor = (CActor)Activator.CreateInstance(actorType);

                        Vector2 coordinates = Vector2.Zero;

                        if (!_coordFormat.IsMatch(actor.COORDS))
                            throw new FormatException("The coordinate format provided was not valid.\n" + "Actor: " + actor.actor.ToString() + " " + actor.name);

                        coordinates.X = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[0]);
                        coordinates.Y = (float)Convert.ToDouble(_valSplitter.Split(actor.COORDS)[1]);

                        tempActor.init(actor.name, coordinates, componentAddresses - 1, _valSplitter.Split(actor.param));

                        tempComp.actors.Add(actor.name, tempActor);

                    }
                    compList[componentCount++] = tempComp;

                }
                _layers[layerCount] = new CLayer(layer.NAME, compList, tiles);

            }

        }

        private void serializeToXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Gears.Cartography.Map));
            TextWriter writer = new StreamWriter(fileName);

            serializer.Serialize(writer, this);
            writer.Close();
        }

       
    }
}
