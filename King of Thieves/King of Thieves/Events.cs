using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves
{
    public delegate void createHandler(object sender);
    public delegate void destroyHandler(object sender);
    public delegate void keyDownHandler(object sender);
    public delegate void frameHandler(object sender);
    public delegate void drawHandler(object sender);
    public delegate void keyReleaseHandler(object sender);
    public delegate void collideHandler(object sender, object collider);
    
}
