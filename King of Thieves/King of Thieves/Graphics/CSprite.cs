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
        private Texture2D _shaderTex = null; 
        private Rectangle _size;
        protected string _name = "";
        protected Vector2 _position = Vector2.Zero;
        private CTextureAtlas _imageAtlas;
        private int _frameTracker = 0;
        private int frameX = 0, frameY = 0;
        

        public CSprite(CTextureAtlas atlas, Effect shader = null, params VertexPositionColor[] vertices)
            : base(shader, vertices)
        {
            _imageAtlas = atlas;
            _size = new Rectangle(0,0, atlas.FrameWidth, atlas.FrameHeight);
            _name = _imageAtlas.sourceImage.Name;
            CMasterControl.drawList.AddLast(this);
        }

        //public void flipH()
        //{
        //    Texture2D flipped = new Texture2D(_imageAtlas.sourceImage.GraphicsDevice, _imageAtlas.sourceImage.Width, _imageAtlas.sourceImage.Height);
        //    Color[] data = new Color[_imageAtlas.sourceImage.Width * _imageAtlas.sourceImage.Height];
        //    Color[] flippedData = new Color[data.Length];

        //    _imageAtlas.sourceImage.GetData<Color>(data);

        //    for (int x = 0; x < _imageAtlas.sourceImage.Width; x++)
        //        for (int y = 0; y < _imageAtlas.sourceImage.Height; y++)
        //        {
        //            int idx = (_imageAtlas.sourceImage.Width - 1 - x) + y * _imageAtlas.sourceImage.Width));
        //            flippedData[x + y * _imageAtlas.sourceImage.Width] = data[idx];
        //        }

        //    flipped.SetData<Color>(flippedData);

             
        //}

        public override void draw(int x, int y)
        {
            if (_imageAtlas == null)
                throw new FormatException("Unable to draw sprite " + _name + ", may be the target of an animation.");
            if (isOffscreen != false)
                renderOffScreen();

            _frameTracker += _imageAtlas.FrameRate;

            if (_frameTracker >= 60)
            {
                _frameTracker = 0;
                frameX++;

                if (frameX >= _imageAtlas.tileXCount)
                {
                    frameX = 0;
                    frameY++;

                    if (frameY >= _imageAtlas.tileYCount)
                        frameY = 0;
                }
                _size = _imageAtlas.getTile(frameX, frameY);
            }

            _position.X = x; _position.Y = y;
            //CGraphics.spriteBatch.Draw(_imageAtlas.sourceImage, _position, _size, Color.White);
            CGraphics.spriteBatch.Draw(_imageAtlas.sourceImage, _position, _size, Color.White);
            base.draw(x,y);
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
