using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace King_of_Thieves
{
    static class CMasterControl
    {
        public static Sound.CAudioPlayer audioPlayer = new Sound.CAudioPlayer();
        //public static Graphics.CTexAtlasControl atlasControl = new Graphics.CTexAtlasControl();
        public static Map.CMapManager mapManager = new Map.CMapManager();
        public static GameTime gameTime;
        public static Dictionary<int, List<Actors.CActorPacket>> commNet = new Dictionary<int, List<Actors.CActorPacket>>();  
    }
}
