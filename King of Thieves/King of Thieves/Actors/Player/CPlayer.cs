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
            base("Player", Vector2.Zero, ACTORTYPES.INTERACTABLE)
        {
           

            //resource init

            _initializeResources();
            image = _imageIndex["PlayerWalkDown"];
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("PlayerWalkDown", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkDown")));
            _imageIndex.Add("PlayerWalkLeft", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkLeft")));
            _imageIndex.Add("PlayerWalkUp", new Graphics.CSprite(Graphics.CTextures.texture("Player:WalkUp")));
            _imageIndex.Add("PlayerIdleDown", new Graphics.CSprite(Graphics.CTextures.texture("Player:IdleDown")));
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
            //throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {
            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _position.X -= 1;
                image = _imageIndex["PlayerWalkLeft"];
            }

            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.D))
                _position.X += 1;

            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.W))
            {
                _position.Y -= 1;
                image = _imageIndex["PlayerWalkUp"];
            }

            if (Input.CInput.keysPressed.Contains(Microsoft.Xna.Framework.Input.Keys.S))
            {
                _position.Y += 1;
                image = _imageIndex["PlayerWalkDown"];
            }
        }

        public override void keyRelease(object sender)
        {
            image = _imageIndex["PlayerIdleDown"];
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
