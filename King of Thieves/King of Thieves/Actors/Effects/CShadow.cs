using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.Effects
{
    class CShadow : CActor
    {
        public CShadow()
        {
            swapImage("shadow");
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("shadow", new Graphics.CSprite("effects:Shadow", Graphics.CTextures.textures["effects:Shadow"]));
        }

        public override void init(string name, Vector2 position, uint compAddress, params string[] additional)
        {
            base.init(name, position, compAddress, additional);
            _followRoot = true;
        }

    }
}
