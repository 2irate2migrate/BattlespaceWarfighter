using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic.Data.Ships
{
    public class Interceptor : _ShipBase
    {
        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public Interceptor(_ShipSerialized inRawShip)
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
            base._ShipModel = ShipModelType.Cylinder;
        }



        public override string ToString()
        {
            return $"(Interceptor:ID) - {Name}";
        }
    }
}
