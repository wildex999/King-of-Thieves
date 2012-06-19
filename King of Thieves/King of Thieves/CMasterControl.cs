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
        public static Dictionary<string, Actors.CActor> componentList = new Dictionary<string,Actors.CActor>();
        public static Sound.CAudioPlayer audioPlayer = new Sound.CAudioPlayer();
        public static Dictionary<Type, Dictionary<string, List<BoundingBox>>> hitboxes = new Dictionary<Type, Dictionary<string,List<BoundingBox>>>(); //jeez this is wacky
    }
}
