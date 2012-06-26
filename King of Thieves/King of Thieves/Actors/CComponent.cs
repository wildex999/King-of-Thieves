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

        public CComponent()
        {
            actors = new Dictionary<string, CActor>();
        }

        //not 100% sure how these will work yet
        public void updateActors(GameTime gameTime)
        {
            root.update(gameTime);

            foreach (KeyValuePair<string, CActor> kvp in actors)
            {
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
