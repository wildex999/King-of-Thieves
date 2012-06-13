using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors
{
    class CActorTest : CActor
    {

        public CActorTest()
        {
            image = new Graphics.CSprite(Graphics.CTextureDict.getTexture("test"));
        }

        public override void create(object sender)
        {
            //throw new NotImplementedException();
        }

        public override void destroy(object sender)
        {
            //throw new NotImplementedException();
        }

        public override void frame(object sender)
        {
            //throw new NotImplementedException();
            
        }

        public override void keyDown(object sender)
        {
            if (Input.CInput.getInputDown(Microsoft.Xna.Framework.Input.Keys.D))
                _position.X += 1;

            
        }

        public override void keyRelease(object sender)
        {
            
        }

        public override void draw(object sender)
        {
            //throw new NotImplementedException();
        }
    }
}
