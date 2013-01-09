using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace King_of_Thieves.Actors
{
    //component is a series of actors.  Bongo Bongo for example would be 3 actors: 2 hands and a main body, main body being the root.
    //this is really a wrapper around CActor to group related CActors together.
    class CComponent
    {
        //if the root moves, all children follow it.  Actors otherwise are free to move freely of each other.
        //actors can also rotate around the root.
        public CActor root;
        public Dictionary<string, CActor> actors;
        private uint _address;

        public CComponent(uint address = 0)
        {
            actors = new Dictionary<string, CActor>();
            _address = address;
        }

        private void passMessage(ref CActor actor, uint eventID, params string[] param)
        {
            actor.addFireTrigger(eventID);
            actor.userParams.AddRange(param);
        }

        public void updateActors(GameTime gameTime)
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
                kvp.Value.position += root.distanceFromLastFrame;

                //update the faggot
                kvp.Value.update(gameTime);
            }
        }

        public void drawActors()
        {
            root.drawMe();
            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                kvp.Value.drawMe();
            }
        }

        public void destroyActors()
        {
            root.remove();
            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
                kvp.Value.remove();
            }
        }


    }
}
