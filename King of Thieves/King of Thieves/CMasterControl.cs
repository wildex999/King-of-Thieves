using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves
{
    static class CMasterControl
    {
        //owners of the CRenderable should determine whether or not it gets queued back up after being drawn
        public static Queue<Graphics.CRenderable> drawQueue = new Queue<Graphics.CRenderable>();

       
    }
}
