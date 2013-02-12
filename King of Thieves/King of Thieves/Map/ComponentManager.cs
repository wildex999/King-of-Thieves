using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Map
{
    class ComponentManager : Gears.Playable.UnitManager
    {
        public ComponentManager(ComponentFactory[] factories)
        {
            Register(factories);
        }
    }
}
