using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework;


namespace King_of_Thieves.Map
{
    class MTestMap : CMrMap
    {
        private CMap _out;

        public MTestMap(string name, MAPTYPES type)
            : base(name, type)
        {
            _map = new CMap();
            _tile = new CTileLayer();
            _tile.tileData = new int[] {0,0,0,0,0};
            _tile.layerNum = 0;
            _objectLayer = new CObjectLayer();
            _hitBoxLayer = new CHitBoxLayer();
            _map.Name = name;
            _map.Version = 2;
            _map.Type = type;
            _map.layerCount = 0;
            _map.tileSet = "/path/to/tileset.png";
            _map.tileLayer = _tile;
            _map.hitBoxLayer = _hitBoxLayer;
            _map.objectLayer = _objectLayer;
            Input.CMrMapIO.Save(_map,"testmap.xml");
            _out = Input.CMrMapIO.Read(_map, "testmap.xml");
#if DEBUG
            System.Diagnostics.Debug.WriteLine(_out.Name + "\n" + _out.tileLayer.tileData[3]);
            System.Diagnostics.Debugger.Break();
#endif
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
