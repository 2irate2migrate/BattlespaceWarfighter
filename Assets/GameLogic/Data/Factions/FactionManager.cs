using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Data.Factions
{
    public class FactionManager
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        /// <summary>
        /// List of Faction IDs.  Key is the unique faction ID, Value is the index inside of this._Factions
        /// </summary>
        public Dictionary<string, int> Faction_IDs = new Dictionary<string, int>();



        protected List<Faction> _Factions = new List<Faction>();

        /// <summary>
        /// List of factions loaded into the game
        /// </summary>
        public List<Faction> Factions { get { return this._Factions; } }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public Faction GetFactionFromID(string inFactionID)
        {
            if (string.IsNullOrWhiteSpace(inFactionID) || !Faction_IDs.ContainsKey(inFactionID))
                return null;

            return _Factions[Faction_IDs[inFactionID]];
        }



        public bool Add(Faction inFaction)
        {
            if (inFaction == null || string.IsNullOrWhiteSpace(inFaction.ID))
                return false;

            if(this.Faction_IDs.ContainsKey(inFaction.ID))
            {
                Debug.LogWarning($"Could not add Faction:{inFaction.Name} to Faction Manager because a faction with the ID:{inFaction.ID} already exists");
                return false;
            }

            _Factions.Add(inFaction);
            Faction_IDs.Add(inFaction.ID, _Factions.Count-1);

            return true;
        }



        public bool FactionIDExists(string inFactionID)
        {
            if (string.IsNullOrWhiteSpace(inFactionID))
                return true;

            return Faction_IDs.ContainsKey(inFactionID);
        }
    }
}
