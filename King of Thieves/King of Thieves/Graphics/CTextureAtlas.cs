using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace King_of_Thieves.Graphics
{
    internal class CTextureAtlas
    {
        public int FrameWidth = 0, FrameHeight = 0, FrameRate = 0, CellSpacing = 0, Column = 0, Row = 0, CurrentCell = 0;
        private Rectangle[,] _textureAtlas;
        private Texture2D _sourceImage;
        private int _fixedWidth = 0, _fixedHeight = 0;
        private static Regex _cellFormat = new Regex("^[1-9]+:[0-9]+$");
        private static Regex _cellSplitter = new Regex(":");

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

        public CTextureAtlas(Texture2D sourceImage, int _frameWidth, int _frameHeight, int _cellSpacing, int frameRate, string startCell, string endCell)
        {
            //parse out the cell ranges
            if (!_cellFormat.IsMatch(startCell) || !_cellFormat.IsMatch(endCell))
                throw new FormatException("Error in cell range format for " + sourceImage.Name + ".  Please use 99:99");

            string[] start = _cellSplitter.Split(startCell);
            string[] end = _cellSplitter.Split(endCell);
            Vector2 startRange = new Vector2(Convert.ToInt32(start[0]), Convert.ToInt32(start[1]));
            Vector2 endRange = new Vector2(Convert.ToInt32(end[0]), Convert.ToInt32(end[1]));
           

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

        public Rectangle getTile(int frameX, int frameY)
        {
            return this._textureAtlas[frameX, frameY];
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
                    //NOW the math is completely fine! :)
                    //The math is completely fine. 
                    //this math seems a bit iffy due to the cellspacing, but we'll see how it goes! -Steve
                    textureAtlas._textureAtlas[x,y] = new Rectangle
                        ((textureAtlas.FrameWidth + CellSpacing) * x, (textureAtlas.FrameHeight + CellSpacing) * y,
                        textureAtlas.FrameWidth, 
                        textureAtlas.FrameHeight);
                }
            }
        }
    }
}
