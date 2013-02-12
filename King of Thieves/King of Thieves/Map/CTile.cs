using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CTile
    {
        private Vector2 _tileBounds;
        public readonly Vector2 tileCoords;
        public readonly string tileSet;


        public CTile(Vector2 atlasCoords, Vector2 mapCoords, string tileSet)
        {
            _tileBounds = atlasCoords;
            tileCoords = mapCoords;
            this.tileSet = tileSet;
        }

        public Vector2 atlasCoords
        {
            get
            {
                return _tileBounds;
            }
        }
    }
}
