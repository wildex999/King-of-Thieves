using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.decoration
{
    class CPot : CLiftable
    {
        public CPot() :
            base()
        {
            _hitBox = new Collision.CHitBox(this, 16, 32, 16, 16);

            swapImage("PotChillinLikeaVillain");
        }

        protected override void _initializeResources()
        {
            base._initializeResources();
            _imageIndex.Add("PotChillinLikeaVillain", new Graphics.CSprite("items:decor:potSmall"));
            _imageIndex.Add("PotBreaking", new Graphics.CSprite("items:decor:potSmallBreak"));
        }
    }
}
