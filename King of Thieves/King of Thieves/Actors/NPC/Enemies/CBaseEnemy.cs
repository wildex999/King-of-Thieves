using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using King_of_Thieves.Graphics;
using Gears.Cloud;
using King_of_Thieves.Input;
namespace King_of_Thieves.Actors.NPC.Enemies
{
    //has all the things that an enemy NPC will have
    //ex: item drops
    abstract class CBaseEnemy : CActor
    {
        protected Dictionary<object,float> _itemDrop; //leave this as object until we have classes for items ready
        

        protected abstract void _addCollidables();

        public override void destroy(object sender)
        {
            _dropItem();

            base.destroy(sender);
        }

        private void _dropItem()
        {
            object itemToDrop = null;


        }
    }
}
