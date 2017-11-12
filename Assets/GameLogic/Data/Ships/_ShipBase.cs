using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Data.Ships
{
    public abstract class _ShipBase
    {
        //==============================================================================
        //
        //                                    CONSTANTS
        //
        //==============================================================================



        public enum ShipModelType
        {
            Cube,
            Sphere,
            Cylinder
        }
        
        
        
        
        
        //==============================================================================
        //
        //                                    CONSTANTS
        //
        //==============================================================================



        public const string DEFAULT_SHIP_NAME = "Unknown Ship";



        public const int DEFAULT_HP = 200;





        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        protected Factions.Faction _Faction = null;

        /// <summary>
        /// The faction that this ship belongs to
        /// </summary>
        public Factions.Faction Faction { get { return this._Faction; } }



        protected string _Name = DEFAULT_SHIP_NAME;

        public string Name { get { return this._Name; } }



        /*protected Crew.Pilots._BasePilot _Pilot = null;

        public Crew.Pilots._BasePilot Pilot
        {
            get { return this._Pilot; }
        }*/



        protected ShipModelType _ShipModel = ShipModelType.Cylinder;

        public ShipModelType ShipModel { get { return this._ShipModel; } }



        protected int _HullHP_Current = 100;

        public int HullHP_Current { get { return this._HullHP_Current; } }

        protected int _HullHP_Maximum = DEFAULT_HP;

        public int HullHP_Maximum { get { return this._HullHP_Maximum; } }



        protected int _ShieldHP_Current = 100;

        public int ShieldHP_Current { get { return this._ShieldHP_Current; } }

        protected int _ShieldHP_Maximum = 100;

        public int ShieldHP_Maximum { get { return this._ShieldHP_Maximum; } }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public virtual void DeSerialize(_ShipSerialized inRawShip)
        {
            #region FactionID

            if (string.IsNullOrWhiteSpace(inRawShip.FactionID))
            {
                Debug.LogWarning($"{this.GetType().Name} has no faction assigned, and cannot be created.");
                return;
            }
            else
            {
                if (!inRawShip.FactionID.StartsWith("faction."))
                {
                    inRawShip.FactionID = "faction." + inRawShip.FactionID;
                }

                if (GameLoop.MAIN.DataManager.FactionManager.FactionIDExists(inRawShip.FactionID))
                {
                    this._Faction = GameLoop.MAIN.DataManager.FactionManager.GetFactionFromID(inRawShip.FactionID);
                }
                else
                {
                    //ToDo: Assign to a neutral faction that any faction can use?
                    Debug.LogError($"{this.GetType().Name} has a non-existent faction assigned ({inRawShip.FactionID}) and cannot be created.");
                    return;
                }
            }

            #endregion


            #region Name

            if (!string.IsNullOrWhiteSpace(inRawShip.Name))
            {
                this._Name = inRawShip.Name;
            }
            else
                Debug.LogWarning($"Creating a {this.GetType().Name} with no name defined.");

            #endregion


            #region Hull HP

            if(inRawShip.HP < 1)
            {
                Debug.LogWarning($"Problem parsing {this.GetType().Name}. Attempted to set the HP for ship to {inRawShip.HP}, defaulting to {DEFAULT_HP}");
                this._HullHP_Maximum = DEFAULT_HP;
            }
            else
            {
                this._HullHP_Maximum = inRawShip.HP;
            }

            #endregion


        }
    }
}