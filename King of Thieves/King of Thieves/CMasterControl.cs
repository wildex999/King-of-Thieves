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
        public static GameTime gameTime;
        //public static Dictionary<string,Map.CMrMap>mapList = new Dictionary<string,Map.CMrMap>();
        public static Dictionary<int, List<Actors.CActorPacket>> commNet = new Dictionary<int, List<Actors.CActorPacket>>();  
    }
}
