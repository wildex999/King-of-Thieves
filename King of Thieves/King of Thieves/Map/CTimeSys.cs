using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Map
{
    class CTimeSys
    {
        private int _dayTime;
        private int _nightTime;
        private bool _isDay;

        public CTimeSys(int daySeconds, int nightSeconds, bool isDay = true)
        {
            _dayTime = daySeconds;
            _nightTime = nightSeconds;
            _isDay = isDay;
        }

        public bool isDay
        {
            get
            {
                return _isDay;
            }
        }

        public int dayTime
        {
            get
            {
                return _dayTime;
            }
        }

        public int nightTime
        {
            get
            {
                return _nightTime;
            }
        }
    }
}
