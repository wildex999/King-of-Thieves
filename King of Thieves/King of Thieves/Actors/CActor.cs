using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Actors
{
    abstract class CActor
    {
        private Vector2 _position = Vector2.Zero;
        public Graphics.CSprite _image;
        //hitboxes will go here as well? What a terrible night for a curse...
        //event handlers will be added here

        public event createHandler onCreate;
        public event destroyHandler onDestroy;

        public abstract void create(object sender);
        public abstract void destroy(object sender);

        public CActor()
        {
            onCreate += new createHandler(create);
            onDestroy += new destroyHandler(destroy);

            onCreate(this);
        }

        ~CActor()
        {
            onDestroy(this);

            onCreate -= new createHandler(create);
            onDestroy -= new destroyHandler(destroy);
        }
    }
}
