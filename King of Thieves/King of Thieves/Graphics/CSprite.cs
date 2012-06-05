using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    class CSprite : CRenderable
    {

        private Texture2D _sprite = null;
        private Rectangle _size;
        protected string _name = "";

        public CSprite(Texture2D sprite, Effect shader = null, params VertexPositionColor[] vertices)
            : base(shader, vertices)
        {
            _sprite = sprite;
            _size = new Rectangle(0, 0, _sprite.Width, _sprite.Height);
            _name = sprite.Name;
        }

        public override void draw()
        {
            if (_sprite == null)
                throw new FormatException("Unable to draw sprite " + _name + ", may be the target of an animation.");

            CGraphics.spriteBatch.Draw(_sprite, _size, Color.White);
            base.draw();
        }

        public int width
        {
            get
            {
                return _size.Width;
            }
        }

        public int height
        {
            get
            {
                return _size.Height;
            }
        }

        
    }
}
