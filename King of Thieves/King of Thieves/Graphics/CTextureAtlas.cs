using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace King_of_Thieves.Graphics
{
    internal class CTextureAtlas
    {
        public int FrameWidth = 0, FrameHeight = 0, FrameRate = 0, CellSpacing = 0, Column = 0, Row = 0, CurrentCell = 0;
        private Rectangle[,] _textureAtlas;
        private Texture2D _sourceImage;
        private int _fixedWidth = 0, _fixedHeight = 0;

        public CTextureAtlas(Texture2D sourceImage, int _frameWidth, int _frameHeight, int _cellSpacing)
        {
            FrameWidth = _frameWidth;
            FrameHeight = _frameHeight;
            CellSpacing = _cellSpacing;
            _sourceImage = sourceImage;

            _fixedWidth = (_sourceImage.Bounds.Width / (_frameWidth + _cellSpacing));
            _fixedHeight = (_sourceImage.Bounds.Height / (_frameHeight + _cellSpacing));

            _textureAtlas = new Rectangle[_fixedWidth, _fixedHeight];//made a small change here to allow for cellspacing in the calculation. -Steve
            _assembleTextureAtlas(this);
        }

        public CTextureAtlas(Texture2D sourceImage, int _frameWidth, int _frameHeight, int _cellSpacing, int frameRate)
        {
            FrameWidth = _frameWidth;
            FrameHeight = _frameHeight;
            CellSpacing = _cellSpacing;
            _sourceImage = sourceImage;
            FrameRate = frameRate;

            _fixedWidth = (_sourceImage.Bounds.Width / (_frameWidth + _cellSpacing));
            _fixedHeight = (_sourceImage.Bounds.Height / (_frameHeight + _cellSpacing));

            _textureAtlas = new Rectangle[_fixedWidth, _fixedHeight];
            _assembleTextureAtlas(this);
        }

        public Rectangle getTile(int row, int col)
        {
            return _textureAtlas[row,col];
        }

        public Texture2D sourceImage
        {
            get
            {
                return _sourceImage;
            }
        }

        public int tileXCount
        {
            get
            {
                return _fixedWidth;
            }
        }

        public int tileYCount
        {
            get
            {
                return _fixedHeight;
            }
        }

        /*
            * x == row
            * y == column
        */
        private void _assembleTextureAtlas(CTextureAtlas textureAtlas)
        {
            for (int y = 0; y <= _fixedHeight - 1; y++)
            {
                for (int x = 0; x <= _fixedWidth - 1; x++)
                {

                    //this math seems a bit iffy due to the cellspacing, but we'll see how it goes! -Steve
                    textureAtlas._textureAtlas[x,y] = new Rectangle
                        (textureAtlas.FrameWidth * x, textureAtlas.FrameHeight * y,
                        textureAtlas.FrameWidth + textureAtlas.CellSpacing, 
                        textureAtlas.FrameHeight + textureAtlas.CellSpacing);

                    
                }
            }
        }
/*
* Must refactor frame-cycle code now.
        public void UpdateFrames(GameTime gameTime, int _frameRate)
        {
            float _timeElapsed = 0;
            if (FrameRate != _frameRate)
                FrameRate = _frameRate;

            _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeElapsed > (1 / (float)FrameRate))
            {
                Cell++;
                Cell = Cell % TextureAtlas.Count();
                _timeElapsed -= (1 / (float)FrameRate);
            }
        }

        public void UpdateFrames(GameTime gameTime, int StartingFrame, int EndingFrame, int _frameRate)
        {
            float _timeElapsed = 0;
            if (Cell < StartingFrame)
                Cell = StartingFrame;

            if (FrameRate != _frameRate)
                FrameRate = _frameRate;

            _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeElapsed > (1 / (float)FrameRate))
            {
                Cell++;
                Cell = Cell % TextureAtlas.Count();
                _timeElapsed -= (1 / (float)FrameRate);
            }
            if (Cell > EndingFrame)
                Cell = StartingFrame;
        }
*/
    /*
        public void DrawFrames(SpriteBatch sb, Vector2 position, Color color, SpriteFont spriteFont)
        {
#if DEBUG
            sb.DrawString(spriteFont, "Current Frame: " + _currFrame.ToString(), Vector2.Zero, color);
#endif
            sb.Draw(_sprite.Tex, position, _sprite.Cells[_currFrame], color);
        }
    */
        ~CTextureAtlas()
        {

        }
    }
}
