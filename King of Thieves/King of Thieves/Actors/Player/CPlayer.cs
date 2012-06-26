using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Player
{
    class CPlayer : CActor
    {
        public CPlayer() :
            base("PLAYER", ACTORTYPES.INTERACTABLE)
        {

        }

        public override void collide(object sender, object collider)
        {
            throw new NotImplementedException();
        }

        public override void create(object sender)
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

        public override void animationEnd(object sender)
        {
            throw new NotImplementedException();
        }

        public override void frame(object sender)
        {
            throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {
            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.A))
                _position.X -= 1;

            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.D))
                _position.X += 1;

            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.W))
                _position.Y -= 1;

            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.S))
                _position.Y += 1;
        }

        public override void keyRelease(object sender)
        {
            throw new NotImplementedException();
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }

        public override void drawMe()
        {
            base.drawMe();
        }



        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
