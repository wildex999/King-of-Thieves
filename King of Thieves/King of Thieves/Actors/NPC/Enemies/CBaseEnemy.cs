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
    public struct dropRate
    {
        public object item;
        public float rate;
    }

    abstract class CBaseEnemy : CActor
    {
        protected Dictionary<object,float> _itemDrop; //leave this as object until we have classes for items ready
        protected int _lineOfSight;
        protected float _visionRange; //this is an angle
        protected int _hearingRadius; //how far away they can hear you from

        //protected abstract void _addCollidables();
        public CBaseEnemy(params dropRate[] drops)
        {
            foreach (dropRate x in drops)
                _itemDrop.Add(x.item, x.rate);
        }

        //just chill there
        protected virtual void idle()
        {
            hunt();
        }

        //look for the player while idling
        private void hunt()
        {

        }

        //chase the player
        protected virtual void chase()
        {

        }

        public override void destroy(object sender)
        {
            _dropItem();

            base.destroy(sender);
        }

        private void _dropItem()
        {
            object itemToDrop = null;
            Random roller = new Random();
            double sum = 0;

            foreach (KeyValuePair<object, float> x in _itemDrop)
            {
                sum += roller.NextDouble();

                if (sum >= x.Value)
                {
                    itemToDrop = x.Key;
                    break;
                }
            }


        }
    }
}
