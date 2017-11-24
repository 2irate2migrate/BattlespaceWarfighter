using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Data.Ships
{
    public static class _ShipRegistry
    {





        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        /// <summary>
        /// The master list of all ship types that are currently loaded into the game
        /// </summary>
        public static Dictionary<int, _ShipBase> ShipList = new Dictionary<int, _ShipBase>();




        /// <summary>
        /// A list of unique ship IDs that you can access according to a nicely formatted string
        /// </summary>
        public static Dictionary<string, int> ShipIDs = new Dictionary<string, int>();





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public static _ShipBase GetShipTypeByUIID(string inUIID)
        {
            if (ShipList == null || ShipIDs == null || string.IsNullOrWhiteSpace(inUIID))
                return null;

            if (!ShipIDs.ContainsKey(inUIID))
                return null;

            int InternalShipID = ShipIDs[inUIID];

            if (!ShipList.ContainsKey(InternalShipID))
                return null;

            return ShipList[InternalShipID];
        }



        /// <summary>
        /// Get the ID of the ship with the matching UI ID.  Return -1 if there were no matches.
        /// </summary>
        /// <param name="inUIID"></param>
        /// <returns></returns>
        public static int GetInternalIDTypeByUIID(string inUIID)
        {
            if (ShipList == null || ShipIDs == null || string.IsNullOrWhiteSpace(inUIID))
                return -1;

            if (!ShipIDs.ContainsKey(inUIID))
                return -1;

            return ShipIDs[inUIID];
        }



        public static bool AddShipType(int inIDType_Internal, string inIDType_UI, _ShipBase inShipData)
        {
            if(inIDType_Internal < 1 || inShipData == null || ShipList.ContainsKey(inIDType_Internal))
            {
                return false;
            }

            ShipList.Add(inIDType_Internal, inShipData);

            if(!string.IsNullOrWhiteSpace(inIDType_UI))
            {
                ShipIDs.Add(inIDType_UI, inIDType_Internal);
            }

            return true;
        }
    }
}
