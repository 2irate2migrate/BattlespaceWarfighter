using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic.Data.Pilots
{
    public struct _Pilot_Serialized
    {
        public string Name_First;
        public string Name_Last;
        public string CallSign;

        public int Level;
        public int Level_XPThisLevel;

        public byte Gender;

        public string ShipIDPreferences;
    }
}
