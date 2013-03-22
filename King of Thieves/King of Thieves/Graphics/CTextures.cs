using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace King_of_Thieves.Graphics
{
    static class CTextures
    {
        private static Dictionary<string, CTextureAtlas> _textures;
        private static ContentManager _content;
        private static RenderTarget2D _tileMapGen = null;
        private static SpriteBatch _tileBatch = null;

        public static void init(ContentManager content)
        {
            _content = content;
            _textures = new Dictionary<string, CTextureAtlas>();

            //Core textures should go here.  Things that are ALWAYS used (or small enough to not hog up memory)
            //other textures should only be loaded when they're needed and should be removed from memory asap.

            _tileBatch = new SpriteBatch(CGraphics.GPU);

            _textures.Add("test", new CTextureAtlas(_content.Load<Texture2D>("test"), 19, 23, 0));
            _textures.Add("mcDungeon2", new CTextureAtlas(_content.Load<Texture2D>("mcDungeon2"), 16, 16, 0, 1));
            _textures.Add("menu", new CTextureAtlas(_content.Load<Texture2D>("menu"), 288, 192, 0,0));
            _textures.Add("Player:WalkDown", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "0:1", "9:1", 15));
            _textures.Add("Player:WalkLeft", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "11:0", "15:1", 15));
            _textures.Add("Player:WalkUp", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "17:0", "21:1", 15));
            _textures.Add("Player:IdleDown", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "0:0", "0:0", 0));
            _textures.Add("Player:IdleUp", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "2:0", "2:0", 0));
            _textures.Add("Player:IdleLeft", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "1:0", "1:0", 0));
            _textures.Add("Player:SwingUp", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "23:2", "33:2", 55));
            _textures.Add("Player:SwingDown", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "23:0", "33:0", 55));
            _textures.Add("Player:SwingLeft", new CTextureAtlas(_content.Load<Texture2D>("Player"), 32, 32, 1, "23:1", "32:1", 55));
            _textures.Add("GerudoSword:SwingDown", new CTextureAtlas(_content.Load<Texture2D>("Swords"), 64, 64, 1, "0:0", "7:0", 55));
            _textures.Add("GerudoSword:SwingUp", new CTextureAtlas(_content.Load<Texture2D>("Swords"), 64, 64, 1, "0:1", "7:1", 55));
            _textures.Add("GerudoSword:SwingRight", new CTextureAtlas(_content.Load<Texture2D>("Swords"), 64, 64, 1, "0:2", "7:2", 55));
        }

        public static void cleanUp(string nameSpace = "")
        {
            if (nameSpace == "")
            {
                _textures.Clear();
                _textures = null;
                return;
            }

            var resourcesToRemove = (from pair in _textures
                                     where pair.Key.Contains(nameSpace)
                                     select pair.Key).ToArray();

            foreach (string key in resourcesToRemove)
                _textures.Remove(key);

            _tileBatch.Dispose();
            _tileBatch = null;
        }

        public static CTextureAtlas texture(string name)
        {
            return _textures[name];
        }

        public static Texture2D generateLayerImage(Map.CLayer layerToRender, Map.CTile[] tileStrip)
        {
            _tileMapGen = new RenderTarget2D(CGraphics.GPU, layerToRender.width, layerToRender.height);

            CGraphics.GPU.SetRenderTarget(_tileMapGen);
            _tileBatch.Begin();

            foreach (Map.CTile tile in tileStrip)
                _tileBatch.Draw(_textures[tile.tileSet].sourceImage, tile.tileCoords, _textures[tile.tileSet].getTile((int)tile.atlasCoords.X, (int)tile.atlasCoords.Y), Color.White);

            _tileBatch.End();
            CGraphics.GPU.SetRenderTarget(null);

            return (_tileMapGen);
        }


    }
}
