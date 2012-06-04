using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    //This is used for tiles as well as sprite sheets
    class CTileSheet
    {
        private Rectangle[] _breaks;
        private Texture2D _sheet;
        private int _countH, _countV;
        private int _total;

        public CTileSheet(Texture2D texture, int tileWidth, int tileHeight, int spacing)
        {
            try
            {
                _countH = texture.Width / (tileWidth + spacing);
                _countV = texture.Height / (tileHeight + spacing);
            }
            catch (DivideByZeroException)
            {
                throw new System.FormatException("(tileWidth + spacing) and (tileHeight + spacing) must be greater than 0.");
            }

            if (_countH == 0 || _countV == 0)
                throw new System.FormatException("Error in texture dimensions.  Must be greater than 0.");

            _total =_countH * _countV;
            _breaks = new Rectangle[_total];
            int tileX = spacing, tileY = spacing;
            int tileTrackX = 1, tileTrackY = 1;


            for (int i = 0; i < _total; i++)
            {
                _breaks[i] = new Rectangle(tileX, tileY, tileWidth, tileHeight);

                tileX += tileWidth + spacing;
                tileTrackX++;

                if (tileTrackX > _countH)
                {
                    tileTrackY++;
                    tileY += tileHeight + spacing;
                    tileTrackX = 1;
                    tileX = spacing;
                }
            }
        }

        public int count
        {
            get
            {
                return _total;
            }
        }

        public Rectangle getSprite(int index)
        {
            return _breaks[index];
        }
    }
}
