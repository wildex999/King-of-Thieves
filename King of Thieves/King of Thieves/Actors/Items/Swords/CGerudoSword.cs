using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using King_of_Thieves.Input;
using Gears.Cloud;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Items.Swords
{
    class CSword : CActor
    {
        private event userEventHandler swingEvent;

        public CSword(string swordName) :
            base(swordName, Vector2.Zero, ACTORTYPES.INTERACTABLE)
        {

        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();

            _userEvents.Add(0, userEventSwing);
            swingEvent += _userEvents[0];

        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            //use the gerudo sword for now
            _imageIndex.Add("SwingDown", new Graphics.CSprite(Graphics.CTextures.texture("GerudoSword:SwingDown")));
            _imageIndex.Add("SwingRight", new Graphics.CSprite(Graphics.CTextures.texture("GerudoSword:SwingRight")));
            _imageIndex.Add("SwingLeft", new Graphics.CSprite(Graphics.CTextures.texture("GerudoSword:SwingRight"), null, true));
            _imageIndex.Add("SwingUp", new Graphics.CSprite(Graphics.CTextures.texture("GerudoSword:SwingUp")));
        }

        public void userEventSwing(object sender)
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
            //throw new NotImplementedException();
        }

        public override void keyDown(object sender)
        {

        }

        public override void keyRelease(object sender)
        {
            
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



