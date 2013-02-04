using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors
{
    class CActorPacket
    {
        private int _userEventID;
        private string _actorName;
        private List<string> _parameters = new List<string>();

        public CActorPacket(int userEvent, string actor, params string[] param)
        {
            _userEventID = userEvent;
            _actorName = actor;

            if (param.Count() > 0)
                _parameters.AddRange(param);
        }

        public string getParam(int index)
        {
            return _parameters[index];
        }

        public string[] getParams()
        {
            return _parameters.ToArray();
        }

        public int userEventID
        {
            get
            {
                return _userEventID;
            }
        }

        public string actor
        {
            get
            {
                return _actorName;
            }
        }
    }
}
