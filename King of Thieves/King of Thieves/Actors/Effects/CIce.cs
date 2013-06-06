using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Effects
{
    class CIce : CActor
    {
        public CIce()
        {
            swapImage("ice");
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("ice", new Graphics.CSprite("effects:Ice", Graphics.CTextures.textures["effects:Ice"]));
        }

        public override void init(string name, Vector2 position, uint compAddress, params string[] additional)
        {
            base.init(name, position, compAddress, additional);
            _followRoot = true;
        }

    }
}
