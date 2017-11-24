using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Utility
{
    [RequireComponent(typeof(GameLoop))]
    public class RandomHelper : MonoBehaviour
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================


        public int Primary_Seed = 218236963;



        public System.Random Primary { get; protected set; }





        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public void Start()
        {
        }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public void Init()
        {
            Primary = new System.Random(Primary_Seed);
        }
    }
}
