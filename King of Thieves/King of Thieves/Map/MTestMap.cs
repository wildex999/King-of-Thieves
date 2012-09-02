using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework;


namespace King_of_Thieves.Map
{
    class MTestMap : CMrMap
    {
        public CMap Map = new CMap();
        private CTileLayer _tile = new CTileLayer();

        public MTestMap(string name, MAPTYPES type)
            : base(name, type)
        {
            _tile.tileData = new int[] {0,0,0,0,0};
            _tile.layerNum = 0;

            Map.Name = name;
            Map.Version = 2;
            Map.Type = type;
            Map.layerCount = 0;
            Map.tileSet = "/path/to/tileset.png";
            Map.tileLayer = _tile;

        }


        public override void create(object sender)
        {
            throw new NotImplementedException();
        }

        public override void load(object sender)
        {
            throw new NotImplementedException();
        }

        public override void destroy(object sender)
        {
            throw new NotImplementedException();
        }

        public override void draw(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
