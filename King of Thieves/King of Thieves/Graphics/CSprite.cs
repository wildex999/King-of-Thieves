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
        //private Texture2D _spritertar = null; Hang on, might not be needed, not 100% sure atm. -- gm112
        private Rectangle _size;
        protected string _name = "";
        protected Vector2 _position = new Vector2(0);

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

            CGraphics.spriteBatch.Draw(_sprite, _position, _size, Color.White);
            base.draw();
        }

        public int X
        {
            get
            {
                return (int)_position.X;
            }
            set
            {
                _position.X = value;
            }
        }

        public int Y
        {
            get
            {
                return (int)_position.Y;
            }
            set
            {
                _position.Y = value;
            }
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
