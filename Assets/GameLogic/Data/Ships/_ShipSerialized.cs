using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic.Data.Ships
{
    public struct _ShipSerialized
    {
        public float Velocity;
        public int HP;
        public string Name;
        public string FactionID;

        public override string ToString()
        {
            return $"{Name} - HP:{HP}, Velocity:{Velocity}";
        }
    }
}