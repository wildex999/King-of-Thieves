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
        public event keyDownHandler onKeyDown;
        public event frameHandler onFrame;
        public event drawHandler onDraw;

        public abstract void create(object sender);
        public abstract void destroy(object sender);
        public abstract void keyDown(object sender);
        public abstract void frame(object sender);
        public abstract void draw(object sender);

        public CActor()
        {
            onCreate += new createHandler(create);
            onDestroy += new destroyHandler(destroy);
            onKeyDown += new keyDownHandler(keyDown);
            onFrame += new frameHandler(frame);
            onDraw += new drawHandler(draw);

            onCreate(this);
        }

        ~CActor()
        {
            onDestroy(this);

            onCreate -= new createHandler(create);
            onDestroy -= new destroyHandler(destroy);
            onKeyDown -= new keyDownHandler(keyDown);
            onFrame -= new frameHandler(frame);
            onDraw -= new drawHandler(draw);
        }

        public virtual void update()
        {
            onFrame(this);
        }

        public virtual void draw()
        {
            onDraw(this);
        }

        public virtual void destroy()
        {
            onDestroy(this);
        }
    }
}
