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
        protected Vector2 _position = Vector2.Zero;
        protected Vector2 _oldPosition = Vector2.Zero;
        public Graphics.CSprite image;
        //hitboxes will go here as well? What a terrible night for a curse...
        //event handlers will be added here

        public event createHandler onCreate;
        public event destroyHandler onDestroy;
        public event keyDownHandler onKeyDown;
        public event frameHandler onFrame;
        public event drawHandler onDraw;
        public event keyReleaseHandler onKeyRelease;

        public abstract void create(object sender);
        public abstract void destroy(object sender);
        public abstract void keyDown(object sender);
        public abstract void keyRelease(object sender);
        public abstract void frame(object sender);
        public abstract void draw(object sender);

        public CActor()
        {
            onCreate += new createHandler(create);
            onDestroy += new destroyHandler(destroy);
            onKeyDown += new keyDownHandler(keyDown);
            onKeyRelease += new keyReleaseHandler(keyRelease);
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
            onKeyRelease -= new keyReleaseHandler(keyRelease);
            onDraw -= new drawHandler(draw);
        }

        public virtual void update()
        {
            onFrame(this);

            _oldPosition = _position;
            image.X = (int)_position.X;
            image.Y = (int)_position.Y;

            if (Input.CInput.areKeysPressed)
                onKeyDown(this);
        }

        public virtual void drawMe()
        {
            onDraw(this);

            image.draw();
        }

        public Vector2 position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Vector2 oldPosition
        {
            get
            {
                return _oldPosition;
            }
            
        }

        public Vector2 distanceFromLastFrame
        {
            get
            {
                return (position - oldPosition);
            }
        }
    }
}
