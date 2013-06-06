using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeeseThunder : CBaseKeese
    {
        public CKeeseThunder()
            : base(60)
        {
            _type = KEESETYPE.THUNDER;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keeseThunder:Idle", Graphics.CTextures.textures["keeseThunder:Idle"]));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keeseThunder:Fly", Graphics.CTextures.textures["keeseThunder:Fly"]));
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
