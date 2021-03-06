﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors
{
    class CActorPacket
    {
        private int _userEventID;
        private string _actorName;
        private List<object> _parameters = new List<object>();

        public CActorPacket(int userEvent, string actor, params object[] param)
        {
            _userEventID = userEvent;
            _actorName = actor;

            if (param.Count() > 0)
                _parameters.AddRange(param);
        }

        public object getParam(int index)
        {
            return _parameters[index];
        }

        public object[] getParams()
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
