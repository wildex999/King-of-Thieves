using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace King_of_Thieves.Actors
{
    //component is a series of actors.  Bongo Bongo for example would be 3 actors: 2 hands and a main body, main body being the root.
    //this is really a wrapper around CActor to group related CActors together.
    public class CComponent : Gears.Playable.Unit
    {
        //if the root moves, all children follow it.  Actors otherwise are free to move freely of each other.
        //actors can also rotate around the root.
        public CActor root;
        public uint rootDrawHeight; //The height that root is to draw at, that is, how many elements to draw behind root
        public Dictionary<string, CActor> actors;
        private uint _address;
        private uint currentDrawHeight;

        protected override string TextureFileLocation
        {
            get { return ""; }
        }

        public CComponent(uint address = 0)
        {
            actors = new Dictionary<string, CActor>();
            _address = address;
        }

        private void passMessage(ref CActor actor, uint eventID, params object[] param)
        {
            actor.addFireTrigger(eventID);
            actor.userParams.AddRange(param);
        }

        public override void Update(GameTime gameTime)
        {
            root.update(gameTime);

            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                //first get messages from the commNet
                if (CMasterControl.commNet[(int)_address].Count() > 0)
                {
                    CActorPacket[] packetData = new CActorPacket[CMasterControl.commNet[(int)_address].Count()];
                    CMasterControl.commNet[(int)_address].CopyTo(packetData);

                    var group = from packets in packetData
                                where kvp.Key == packets.actor
                                select packets;

                    foreach (var result in group)
                    {
                        
                        //pass the message to the actor
                        CActor temp = kvp.Value;
                        passMessage(ref temp, (uint)result.userEventID, result.getParams());
                        CMasterControl.commNet[(int)_address].Remove(result);
                        
                    }
          
                }

                //update position relative to the root
                if (kvp.Value._followRoot)
                    kvp.Value.position += root.distanceFromLastFrame;

                //update
                kvp.Value.update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) //spritebatch not used
        {
            currentDrawHeight = 0;
            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                if(rootDrawHeight == currentDrawHeight++)
                    root.drawMe();
                kvp.Value.drawMe();
            }
            //If root is last
            if (rootDrawHeight == currentDrawHeight)
                root.drawMe();
        }

        public void destroyActors()
        {
            root.remove();
            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                kvp.Value.remove();
            }
        }

        public void addActor(CActor actor, String name)
        {
            //Add as root if no root is set
            if (root != null)
                actors.Add(name, actor);
            else
                root = actor;
            //Allow actor to access it's component
            actor.component = this;
        }

        public void removeActor(CActor actor)
        {
            if (actor.component == this)
            {
                //If we are removing the root, we need to add the next actor as root
                if (root == actor)
                {
                    root = actors.GetEnumerator().Current.Value;
                    actors.Remove(root.name);
                    //This will fail if there are no more root, but in that case we can't realy do much, nor should that happen.
                }
                else
                {
                    actor.component = null;
                    actors.Remove(actor.name);
                }
            }
        }


    }
}
