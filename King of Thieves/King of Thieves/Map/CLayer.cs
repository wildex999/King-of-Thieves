using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Map
{
    class CLayer
    {
        private ComponentManager _components;
        public readonly string NAME;
        private int _width, _height; //PIXELS!!!
        private Texture2D _image; //key refers to the image depth
        private CTile[] _tiles; //raw tile data

        public CLayer(string name, Actors.CComponent[] components, CTile[] tiles)
        {
            NAME = name;
            _tiles = tiles;
            _components = new ComponentManager(new ComponentFactory[]{ new ComponentFactory(components) } );
            _image = Graphics.CTextures.generateLayerImage(this, tiles);
        }

        ~CLayer()
        {
            if (_image != null)
                _image.Dispose();

            _image = null;
        }

        public void updateLayer(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //components
            _components.Update(gameTime);
        }

        public void drawLayer()
        {
            _components.Draw(null);
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

        public void addImage(Texture2D image)
        {
            _image = image;
        }
    }
}
