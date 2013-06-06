using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeese : CBaseKeese
    {
        public CKeese()
            : base(60)
        {
            _type = KEESETYPE.NORMAL;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keese:Idle", Graphics.CTextures.textures["keese:Idle"]));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keese:Fly", Graphics.CTextures.textures["keese:Fly"]));
        }
    }
}
