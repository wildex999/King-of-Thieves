using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Map
{
    enum MAPTYPES
    {
        ROOT = 0,
        CHUNK = 1
    }

    abstract class CMrMap
    {
        private CMrMapIO iocereal;

        public CMrMap(string name, int type)
        {

        }
        public override void create(object sender)
        {
            // Load the map here.
            throw new NotImplementedException();
        }

        public override void destroy(object sender)
        {
            throw new NotImplementedException();
        }

        public override void frame(object sender)
        {
            throw new NotImplementedException();
        }

        public override void draw(object sender)
        {
            throw new NotImplementedException();
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
