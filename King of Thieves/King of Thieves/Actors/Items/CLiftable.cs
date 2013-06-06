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
        public CLiftable() :
            base()
        {
            
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

        public override void collide(object sender, CActor collider)
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
                }

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
