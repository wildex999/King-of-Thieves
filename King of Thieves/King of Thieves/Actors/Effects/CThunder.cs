using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Effects
{
    class CThunder : CActor
    {
        public CThunder()
        {
            swapImage("thunder");
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("thunder", new Graphics.CSprite("effects:Thunder", Graphics.CTextures.textures["effects:Thunder"]));
        }

        public override void init(string name, Vector2 position, uint compAddress, params string[] additional)
        {
            base.init(name, position, compAddress, additional);
            _followRoot = true;
        }

    }
}
