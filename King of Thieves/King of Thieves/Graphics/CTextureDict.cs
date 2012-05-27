using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace King_of_Thieves.Graphics
{
    public static class CTextureDict
    {

        private static Dictionary<string, Texture2D> _textures;

        public static void init(ContentManager content)
        {
            //_textures.Add(
        }

        public static Texture2D getTexture(string name)
        {
            try
            {
                return _textures[name];
            }
            catch (KeyNotFoundException)
            {
                //not sure how we're gonna report the error yet
                return null;
            }
        }
            
        
    }
}
