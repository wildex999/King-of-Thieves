using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Map
{
    class CLayer
    {
        private ComponentManager _components;
        public string NAME;
        private int _width, _height; //PIXELS!!!
        private Graphics.CSprite _image;
        public Dictionary<string, Graphics.CSprite> otherImages = new Dictionary<string, Graphics.CSprite>();

        private List<CTile> _tiles = new List<CTile>(); //raw tile data

        public CLayer(ref Graphics.CSprite image)
        {
            _image = image;
        }

        public CLayer(string name, Actors.CComponent[] components, CTile[] tiles, ref Graphics.CSprite image)
        {
            NAME = name;
            _tiles.AddRange(tiles);
            _image = image;
            _components = new ComponentManager(new ComponentFactory[]{ new ComponentFactory(components) } );
            //_image = Graphics.CTextures.generateLayerImage(this, tiles);
        }

        ~CLayer()
        {
             _image = null;
        }

        public void updateTileSet(Graphics.CSprite newSet)
        {
            foreach (CTile tile in _tiles)
            {
                if (string.IsNullOrEmpty(tile.tileSet))
                    tile.tileSet = _image.atlasName;
            }

            _image = newSet;
        }

        public void setName(string name)
        {
            NAME = name;
        }

        public void addTile(CTile tile)
        {
            _tiles.Add(tile);
        }

        public void updateLayer(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //components
            _components.Update(gameTime);
            
        }

        public void drawLayer(bool editor = false)
        {
            foreach (CTile tile in _tiles)
            {
                Vector2 dimensions = Vector2.Zero;
                float dividerX, dividerY;

                //get tileset info
                if (string.IsNullOrEmpty(tile.tileSet))
                    dimensions = new Vector2(Graphics.CTextures.textures[_image.atlasName].FrameWidth, Graphics.CTextures.textures[_image.atlasName].FrameHeight);
                else
                    dimensions = new Vector2(Graphics.CTextures.textures[tile.tileSet].FrameWidth, Graphics.CTextures.textures[tile.tileSet].FrameHeight);
            }

            if (_components != null)
                _components.Draw(Graphics.CGraphics.spriteBatch);


        }

        public int width
        {
            get
            {
                return _width;
            }
        }

        public int height
        {
            get
            {
                return _height;
            }
        }

        public void addImage(Graphics.CSprite image)
        {
            _image = image;
        }
    }
}
