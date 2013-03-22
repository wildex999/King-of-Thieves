using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using Gears.Cloud;
using King_of_Thieves.Input;

namespace King_of_Thieves.Actors
{
    enum ACTORTYPES
    {
        MANAGER = 0,
        INTERACTABLE
    }

    enum DIRECTION
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT
    }

    abstract class CActor
    {
        protected Vector2 _position = Vector2.Zero;
        protected Vector2 _oldPosition = Vector2.Zero;
        public readonly ACTORTYPES ACTORTYPE;
        protected string _name;
        protected List<Type> _collidables;
        protected CAnimation _sprite;
        protected DIRECTION _direction = DIRECTION.UP;
        protected Boolean _moving = false; //used for prioritized movement
        private uint _componentAddress = 0;
        protected Dictionary<uint, userEventHandler> _userEvents;
        protected List<uint> _userEventsToFire;
        protected string _state = "Idle";
        public Graphics.CSprite image;
        protected Dictionary<string, Graphics.CSprite> _imageIndex;
        private bool _animationHasEnded = false;
        public List<string> userParams = new List<string>();
        //hitboxes will go here as well? What a terrible night for a curse...
        //event handlers will be added here

        public event createHandler onCreate;
        public event destroyHandler onDestroy;
        public event keyDownHandler onKeyDown;
        public event frameHandler onFrame;
        public event drawHandler onDraw;
        public event keyReleaseHandler onKeyRelease;
        public event collideHandler onCollide;
        public event animationEndHandler onAnimationEnd;
        public event timerHandler onTimer0;
        public event timerHandler onTimer1;
        public event timerHandler onTimer2;

        public virtual void create(object sender) { }
        public virtual void destroy(object sender) { }
        public virtual void keyDown(object sender) { }
        public virtual void keyRelease(object sender) { }
        public virtual void frame(object sender) { }
        public virtual void draw(object sender) { }
        public virtual void collide(object sender, object collider) { }
        public virtual void animationEnd(object sender) { }

        protected abstract void _addCollidables(); //Use this guy to tell the Actor what kind of actors it can collide with

        

        public CActor()
            
        {
            onCreate += new createHandler(create);
            onDestroy += new destroyHandler(destroy);
            onKeyDown += new keyDownHandler(keyDown);
            onKeyRelease += new keyReleaseHandler(keyRelease);
            onFrame += new frameHandler(frame);
            onDraw += new drawHandler(draw);
            onAnimationEnd += new animationEndHandler(animationEnd);

            _name = name;
            _collidables = new List<Type>();

            try
            {
                _addCollidables();
            }
            catch (NotImplementedException)
            { ;}

            _position = position;

            try
            {
                onCreate(this);
            }
            catch (NotImplementedException)
            { }

            _registerUserEvents();
            _initializeResources();
        }

        ~CActor()
        {
            onCreate -= new createHandler(create);
            onDestroy -= new destroyHandler(destroy);
            onKeyDown -= new keyDownHandler(keyDown);
            onFrame -= new frameHandler(frame);
            onKeyRelease -= new keyReleaseHandler(keyRelease);
            onDraw -= new drawHandler(draw);
        }

        //overload this and call the base to process your own parameters
        public virtual void init(string name, Vector2 position, uint compAddress, params string[] additional)
        {
            _name = name;
            _position = position;
            _componentAddress = compAddress;
        }

        public string state
        {
            get
            {
                return _state;
            }
        }

        public void swapImage(string imageIndex, bool triggerAnimEnd = true)
        {
            image = _imageIndex[imageIndex];

            if (triggerAnimEnd)
            {
                _animationHasEnded = true;
            }
        }

        public void addFireTrigger(uint userEvent)
        {
            _userEventsToFire.Add(userEvent);
        }

        protected virtual void _registerUserEvents()
        {
            _userEvents = new Dictionary<uint, userEventHandler>();
            _userEventsToFire = new List<uint>();
        }

        public CAnimation spriteIndex
        {
            get
            {
                return _sprite;
            }
            set
            {
                _sprite = value;
            }
        }

        public virtual void update(GameTime gameTime)
        {
            
            onFrame(this);

            if (_animationHasEnded)
                try
                {
                    onAnimationEnd(this);
                }
                catch (NotImplementedException) { ;}

            
            _oldPosition = _position;

            if (image != null)
            {
                image.X = (int)_position.X;
                image.Y = (int)_position.Y;
            }

            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysPressed)
                onKeyDown(this);

            if ((Master.GetInputManager().GetCurrentInputHandler() as CInput).areKeysReleased)
                onKeyRelease(this);

            foreach (uint ID in _userEventsToFire)
            {
                _userEvents[ID](this);
            }

            _userEventsToFire.Clear();

            _animationHasEnded = false;
            userParams.Clear();

        }

        public virtual void drawMe()
        {
            try
            {
                onDraw(this);
            }
            catch (NotImplementedException)
            { ;}

            if (image != null)
                _animationHasEnded = image.draw((int)_position.X, (int)_position.Y);
 
        }

        protected virtual void _initializeResources()
        {
            //add sprites to image index by overloading this function.
            //also add resources to the texture cache here.
            _imageIndex = new Dictionary<string, CSprite>();
        }

        private void _closeResources()
        {
            _imageIndex.Clear();
            _imageIndex = null;

            
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

        public virtual void remove()
        {
            onDestroy(this);
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        private void _checkCollisions()
        {
            //This shit is WEIRD.
            //fetch my hitboxes
            //List<BoundingBox> myBoxes = CMasterControl.hitboxes[this.GetType()][_name];
            
           
        }

        //this will go up to the component and trigger the specified user event in the specified actor
        //what this does is create a "packet" that will float around in some higher level scope for the component to pick up
        protected void _triggerUserEvent(int eventNum, string actorName, params string[] param)
        {
            CMasterControl.commNet[(int)_componentAddress].Add(new CActorPacket(eventNum, actorName, param));
        }
    }
}
