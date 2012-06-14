using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves
{
    static class CMasterControl
    {
        public static LinkedList<Graphics.CRenderable> drawList = new LinkedList<Graphics.CRenderable>();
        public static LinkedList<Actors.CActor> componentList = new LinkedList<Actors.CActor>();
        public static Sound.CAudioPlayer audioPlayer = new Sound.CAudioPlayer();
        public static Dictionary<Type, List<List<BoundingBox>>> hitboxes = new Dictionary<Type, List<List<BoundingBox>>>(); //jeez this is wacky
    }
}
