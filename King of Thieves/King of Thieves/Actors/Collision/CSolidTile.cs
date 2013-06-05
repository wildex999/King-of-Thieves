﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Collision
{
    class CSolidTile : CActor
    {
        public CSolidTile() : base()
        {
            _hitBox = new CHitBox(this, 16, 32, 16, 16);
        }
    }
}
