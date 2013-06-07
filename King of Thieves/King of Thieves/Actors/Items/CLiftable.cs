using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using King_of_Thieves.Actors.Collision;

namespace King_of_Thieves.Actors.Items
{
    class CLiftable : Collision.CSolidTile
    {
        private CActor _collider = null;

        public CLiftable() :
            base()
        {
            _followRoot = true;
            _userEvents.Add(0, _toss);
        }

        protected override void _addCollidables()
        {
            base._addCollidables();
            _collidables.Add(typeof(Player.CPlayer));
            _collidables.Add(typeof(CSolidTile));

            
        }

        private void solidCollide(CActor collider)
        {
            //Calculate How much to move to get out of collision moving towards last collisionless point
            CHitBox otherbox = collider.hitBox;

            //Calculate how far in we went
            float distx = (collider.position.X + otherbox.center.X) - (position.X + hitBox.center.X);
            distx = (float)Math.Sqrt(distx * distx);
            float disty = (position.Y + hitBox.center.Y) - (collider.position.Y + otherbox.center.Y);
            disty = (float)Math.Sqrt(disty * disty);

            float lenx = hitBox.halfWidth + otherbox.halfWidth;
            float leny = hitBox.halfHeight + otherbox.halfHeight;

            int px = 1;
            int py = 1;

            if (collider.position.X + otherbox.center.X < position.X + hitBox.center.X)
                px = -1;
            if (collider.position.Y + otherbox.center.Y < position.Y + hitBox.center.Y)
                py = -1;

            float penx = px * (distx - lenx);
            float peny = py * (disty - leny);
            //Resolve closest to previous position
            float diffx = (position.X + penx) - _oldPosition.X;
            diffx *= diffx;
            float diffy = (position.Y + peny) - _oldPosition.Y;
            diffy *= diffy;

            if (diffx < diffy)
                _position.X += penx; //TODO: dont make a new vector every time
            else if (diffx > diffy)
                _position.Y += peny; //Same here 
            else
                position = new Vector2(position.X + penx, position.Y + peny); //Corner cases 
        }

        private void _toss(object sender)
        {
            _direction = (DIRECTION)userParams[0];
            _state = "Tossing";
            CActor _sender = (CActor)sender;
            startTimer1(500);
            _sender.component.removeActor(this, true);
            this.component = this._oldComponent;
            this.component.enabled = true;
            
        }

        public override void animationEnd(object sender)
        {
            if (_state == "Smash") //ELLO EEELYYYYYZZAAAAA
            {
                this.name = _oldName;
                _killMe = true;
            }
        }

        public override void timer0(object sender)
        {

            _state = "Carry";
            this.component.enabled = false;
            this._oldComponent = this.component;
            this._oldName = this.name;
            this.name = "carryMe";
            _collider.component.addActor(this, _name);
        }

        public override void timer1(object sender)
        {
            _state = "Smash";
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (_state)
            {
                case "Lift":
                    switch (_direction)
                    {
                        case DIRECTION.UP:
                            _position.Y -= 2;
                            break;

                        case DIRECTION.RIGHT:
                            _position.Y -= 1;
                            _position.X += .9f;
                            break;

                        case DIRECTION.LEFT:
                            _position.Y -= 1;
                            _position.X -= .8f;
                            break;

                        case DIRECTION.DOWN:
                            _position.Y -= .1f;
                            break;
                    }
                    break;

                case "Tossing":
                    
                    switch (_direction)
                    {
                        case DIRECTION.UP:
                            _position.Y -= 1.5f;
                            break;
                    }
                    break;
            }
        }

        public override void collide(object sender, CActor collider)
        {
            if (_state != "Lift" || _state != "Carry")
            {
                if (collider is CSolidTile)
                {
                    solidCollide(collider);
                }

                if (collider is Player.CPlayer)
                {
                    //check if the player lifted this
                    if (CMasterControl.glblInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                    {
                        collider.state = "Lift";
                        _state = "Lift";
                        _collider = collider;
                        noCollide = true;

                        //center with the player's hitbox
                        DIRECTION movein = collider.direction;
                        startTimer0(250);
                        switch (movein)
                        {
                            case DIRECTION.DOWN:
                                _direction = DIRECTION.UP;
                                jumpToPoint(collider.position.X - 8, _position.Y);
                                break;

                            case DIRECTION.UP:
                                _direction = DIRECTION.DOWN;
                                jumpToPoint(collider.position.X - 8, _position.Y);
                                break;

                            case DIRECTION.LEFT:
                                _direction = DIRECTION.RIGHT;
                                jumpToPoint(_position.X, collider.position.Y - 16);
                                break;

                            case DIRECTION.RIGHT:
                                _direction = DIRECTION.LEFT;
                                jumpToPoint(_position.X, collider.position.Y - 16);
                                break;
                        }
                        _collider = collider;

                    //moveToPoint((moveTo.X + collider.position.X), (moveTo.Y + collider.position.Y), 30);
                    }
                    else
                    {
                    //get the direction the player walked into this at
                        DIRECTION movein = collider.direction;

                        switch (movein)
                        {
                            case DIRECTION.DOWN:

                                _position.Y += collider.velocity.Y;
                                break;

                            case DIRECTION.UP:
                                _position.Y -= collider.velocity.Y;
                                break;

                            case DIRECTION.LEFT:
                                _position.X -= collider.velocity.X;
                                break;

                            case DIRECTION.RIGHT:
                                _position.X += collider.velocity.X;
                                break;
                         }
                    }
                }
            }
        }
    }
}
