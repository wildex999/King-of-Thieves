using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves
{
    static class CMasterControl
    {
        public static LinkedList<Graphics.CRenderable> drawList = new LinkedList<Graphics.CRenderable>();
        public static LinkedList<Actors.CActor> componentList = new LinkedList<Actors.CActor>();
        public static Sound.CAudioPlayer test = new Sound.CAudioPlayer();
       
    }
}
