using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeeseShadow : CBaseKeese
    {
        public CKeeseShadow()
            : base(60)
        {
            _type = KEESETYPE.SHADOW;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keeseShadow:Idle", Graphics.CTextures.textures["keeseShadow:Idle"]));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keeseShadow:Fly", Graphics.CTextures.textures["keeseShadow:Fly"]));
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
