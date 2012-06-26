using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Menu
{
    //This class is the menu background itself
    class CMenu : CActor
    {
        private Graphics.CSprite _image;
        private int _displayTime;
        private Sound.CSound _bgm;
        private Sound.CSound _itemSwitch;
        private Sound.CSound _itemSelect;
        private GameTime _gameTime;

        public CMenu(string name, Graphics.CSprite image, int displayTime, GameTime gameTime):
            base(name)
        {
            _image = image;
            _displayTime = displayTime;
            _gameTime = gameTime;
        }

        public override void animationEnd(object sender)
        {
            throw new NotImplementedException();
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }

        public override void collide(object sender, object collider)
        {
            //throw new NotImplementedException();
        }

        public override void create(object sender)
        {
            throw new NotImplementedException();
            //play bgm here
        }

        public override void destroy(object sender)
        {
            throw new NotImplementedException();
        }

        public override void draw(object sender)
        {
            throw new NotImplementedException();
        }

        public override void frame(object sender)
        {
            throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {
            if (CInput.keysPressed.Contains(Keys.Down))
            {
                //move selector down
            }
            else if (CInput.keysPressed.Contains(Keys.Up))
            {
                //move selector up
            }
            
        }

        public override void keyRelease(object sender)
        {
            throw new NotImplementedException();
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            
            
        }
    }
}
