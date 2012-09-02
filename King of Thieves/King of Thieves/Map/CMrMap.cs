using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    public enum MAPTYPES
    {
        ROOT = 0,
        CHUNK = 1
    }

    abstract class CMrMap
    {
        //private CMrMapIO iocereal;
        protected string _name;
        protected MAPTYPES _type;
        protected CMap _map;
        protected CTileLayer _tile;
        protected CHitBoxLayer _hitBoxLayer;
        protected CObjectLayer _objectLayer;

        public delegate void ioHandler(object sender);

        public event createHandler onCreate;
        public event ioHandler onLoad;
        public event destroyHandler onDestroy;
        public event drawHandler onDraw;

        public readonly MAPTYPES MAPETYPE;

        public abstract void create(object sender);
        public abstract void load(object sender);
        public abstract void destroy(object sender);
        public abstract void draw(object sender);

        public CMrMap(string name, MAPTYPES type = MAPTYPES.ROOT)
        {
            onCreate += new createHandler(create);
            onLoad += new ioHandler(load);
            onDestroy += new destroyHandler(destroy);
            onDraw += new drawHandler(draw);
            _name = name;
            MAPETYPE = type;
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public MAPTYPES Type
        {
            get
            {
                return this._type;
            }
        }

        ~CMrMap()
        {
            onCreate -= new createHandler(create);
            onLoad -= new ioHandler(load);
            onDestroy -= new destroyHandler(destroy);
            onDraw -= new drawHandler(draw);
        }


    }
}
