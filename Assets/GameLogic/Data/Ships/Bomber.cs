using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic.Data.Ships
{
    public class Bomber : _ShipBase
    {




        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public Bomber(_ShipSerialized inRawShip)
        {
            DeSerialize(inRawShip);
        }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public override void DeSerialize(_ShipSerialized inRawShip)
        {
            base.DeSerialize(inRawShip);
            base._ShipModel = ShipModelType.Cube;
        }



        public override string ToString()
        {
            return $"(Bomber:ID) - {Name}";
        }
    }
}
