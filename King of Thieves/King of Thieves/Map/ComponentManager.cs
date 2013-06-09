using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Map
{
    class ComponentManager : Gears.Playable.UnitManager
    {

        private Actors.CActor[] _actorRegistry;
        private int _actorsInRegistry = 0;

        public ComponentManager(ComponentFactory[] factories)
        {
            Register(factories);

            //for (int i = 0; i < factories.Count(); i++)
            //    for (int j = 0; j < factories[i].
        }

        public void addComponent(Actors.CComponent component)
        {
            base.AddUnit(component, 0);
        }

        public void removeComponent(Actors.CComponent component)
        {
            base.RemoveUnit(component, 0);
        }
    }
}
