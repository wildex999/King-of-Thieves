﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.NPC.Enemies.Keese
{
    class CKeeseIce : CBaseKeese
    {
        public CKeeseIce()
            : base(60)
        {
            _type = KEESETYPE.ICE;
        }

        protected override void _initializeResources()
        {
            base._initializeResources();

            _imageIndex.Add("keeseIdle", new Graphics.CSprite("keeseIce:Idle", Graphics.CTextures.textures["keeseIce:Idle"]));
            _imageIndex.Add("keeseFly", new Graphics.CSprite("keeseIce:Fly", Graphics.CTextures.textures["keeseIce:Fly"]));
        }

        protected override void _addCollidables()
        {
            throw new NotImplementedException();
        }
    }
}
