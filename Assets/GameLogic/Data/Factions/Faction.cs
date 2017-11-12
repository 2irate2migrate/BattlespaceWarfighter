using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Data.Factions
{
    public class Faction
    {





        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        protected string _ID = "faction.id";

        /// <summary>
        /// A user friendly unique ID for this faction.  Always prefixed with 'faction.'
        /// </summary>
        public string ID { get { return this._ID; } }



        protected string _Name = "Unknown Faction";

        /// <summary>
        /// The name of the faction as presented to the UI
        /// </summary>
        public string Name { get { return this._Name; } }





        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public Faction(_FactionSerialized inRawFaction)
        {
            DeSerialize(inRawFaction);
        }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public virtual void DeSerialize(_FactionSerialized inRawFaction)
        {

            if (GameLoop.MAIN?.DataManager?.FactionManager == null)
                return;

            #region Name

            if (!string.IsNullOrWhiteSpace(inRawFaction.Name))
            {
                this._Name = inRawFaction.Name;
            }
            else
                Debug.LogWarning($"Creating a {this.GetType().Name} with no name defined.");

            #endregion



            #region ID

            inRawFaction.ID = inRawFaction.ID.ToLowerInvariant();

            //ToDo: Regex Check Syntax
            if(!string.IsNullOrWhiteSpace(inRawFaction.ID))
            {
                if (!inRawFaction.ID.StartsWith("faction."))
                {
                    inRawFaction.ID = "faction." + inRawFaction.ID;
                    //Debug.LogError($"Cannot create Faction:'{_Name}'.  Faction ID must start with 'faction.'!");
                }

                if (!GameLoop.MAIN.DataManager.FactionManager.FactionIDExists(inRawFaction.ID))
                {
                    this._ID = inRawFaction.ID;
                }
                else
                {
                    Debug.LogError($"Cannot creation Faction:'{_Name}'.  Faction ID:{inRawFaction.ID} already exists!");
                    return;
                }
            }
            else
            {
                Debug.LogError($"Cannot creation Faction:'{_Name}'.  A unique faction ID must be provided!");
                return;
            }

            #endregion

        }



        public override string ToString()
        {
            return $"({ID}) - {Name}";
        }
    }
}
