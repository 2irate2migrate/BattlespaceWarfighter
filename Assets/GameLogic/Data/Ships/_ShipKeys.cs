using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic.Data.Ships
{
    public class _ShipKeys
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        public string ID_UI { get; private set; }



        public int ID_Internal { get; private set; }








        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public _ShipKeys(int inID_Internal, string inID_UI)
        {
            ID_UI = inID_UI;
            ID_Internal = inID_Internal;
        }



        public _ShipKeys(string inID_UI)
        {
            ID_UI = inID_UI;
            ID_Internal = -1;
        }



        public _ShipKeys(int inID_Internal)
        {
            ID_Internal = inID_Internal;
            ID_UI = null;
        }






        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != typeof(_ShipKeys))
                return false;

            _ShipKeys i = (_ShipKeys)obj;

            return (i.ID_Internal == this.ID_Internal || i.ID_UI == this.ID_UI);
        }



        public override int GetHashCode()
        {
            return ID_Internal;
        }
    }
}
