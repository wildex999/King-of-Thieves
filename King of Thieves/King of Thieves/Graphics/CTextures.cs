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

        public static void init(ContentManager content)
        {
            _content = content;
            _textures = new Dictionary<string, CTextureAtlas>();


            _textures.Add("test", new CTextureAtlas(_content.Load<Texture2D>("test"), 19, 23, 0));
        }

        public static CTextureAtlas texture(string name)
        {
            return _textures[name];
        }


    }
}
