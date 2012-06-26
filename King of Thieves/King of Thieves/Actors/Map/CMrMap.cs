using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors.Map
{
    enum MAPTYPES
    {
        ROOT = 0,
        CHUNK = 1
    }

    class CMrMap : CActor
    {
        private CMrMapIO iocereal;

        public CMrMap(string name, int type)
            : base(name, ACTORTYPES.MANAGER)
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

        public override void keyDown(object sender)
        {
            throw new NotImplementedException();
        }

        public override void keyRelease(object sender)
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

        public override void collide(object sender, object collider)
        {
            throw new NotImplementedException();
        }

        public override void animationEnd(object sender)
        {
            throw new NotImplementedException();
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
