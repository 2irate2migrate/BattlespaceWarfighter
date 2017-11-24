using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using Assets.GameLogic.Utility;

namespace Assets.GameLogic.Data.Pilots
{
    public class Pilot : ExposableMonobehaviour
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        public static readonly Dictionary<int, uint> Level_XPRequirementsTable = new Dictionary<int, uint>() { { 1, 500 }, {2, 1000 }, { 3, 3000 }, { 4, 5000 }, { 5, 10000 } };



        [HideInInspector, SerializeField]
        protected string _Name_First;

        [ExposeProperty]
        public string Name_First { get { return this._Name_First; } set { _Name_First = value; } }



        [HideInInspector, SerializeField]
        protected string _Name_Last;

        [ExposeProperty]
        public string Name_Last { get { return _Name_Last; } set { _Name_Last = value; } }



        [ExposeProperty]
        public string Name_Full { get { return $"{Name_First} {Name_Last}"; } private set{ } }



        [HideInInspector, SerializeField]
        protected string _Callsign;

        [ExposeProperty]
        /// <summary>
        /// The callsign, or nickname of this pilot.  Some pilots may not have a call sign.
        /// </summary>
        public string Callsign { get { return _Callsign; } set { _Callsign = value; } }



        [HideInInspector, SerializeField]
        protected int _Level;

        [ExposeProperty]
        /// <summary>
        /// The overall level of this Pilot.  Temporary Property.
        /// </summary>
        /// <remarks>ToDo: Switch out for a more in depth level system in the future</remarks>
        public int Level { get { return this._Level; } set { this._Level = value; } }



        [HideInInspector, SerializeField]
        protected int _Level_XPThisLevel;

        [ExposeProperty]
        /// <summary>
        /// The amount of XP the pilot has accumulated this level
        /// </summary>
        public int Level_XPThisLevel { get { return this._Level_XPThisLevel; } set { this._Level_XPThisLevel = value; } }



        [HideInInspector, SerializeField]
        protected PilotGender _Gender;

        [ExposeProperty]
        /// <summary>
        /// The gender of the pilot.  Can consitute things such as 'robot'
        /// </summary>
        public PilotGender Gender { get { return _Gender; } set { _Gender = value; } }



        /// <summary>
        /// Sequential list of ships that this pilot prefers to fly if they are available in the current engagement
        /// </summary>
        public int[] ShipIDPreferences;



        /// <summary>
        /// The game object that is the root object of this Pilot's current ship if he has one.  Otherwise it is a null reference.
        /// </summary>
        public GameObject GO_ShipRoot = null;





        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public Pilot()
        {
            Level = 1;
            Level_XPThisLevel = 0;

            Gender = PilotGender.Unknown;

            ShipIDPreferences = new int[] { Ships._ShipRegistry.GetInternalIDTypeByUIID("ship.interceptors.firebrand"), Ships._ShipRegistry.GetInternalIDTypeByUIID("ship.lightning"), Ships._ShipRegistry.GetInternalIDTypeByUIID("ship.firespit") };
        }



        public static Pilot CreateRandom(Teams.Team inTeam, int inLevel = 1)
        {
            if (inTeam == null)
                return null;

            GameObject GO_Pilot = new GameObject();
            Pilot OutPilot = GO_Pilot.AddComponent<Pilot>();

            OutPilot.Level = inLevel;
            OutPilot.Gender = GameLoop.MAIN.RandomHelper.Primary.Next(100) <= 60 ? PilotGender.Male : PilotGender.Female;

            if (OutPilot.Gender == PilotGender.Male)
                OutPilot._Name_First = PilotNames.Random_First_Male();
            else
                OutPilot._Name_First = PilotNames.Random_First_Female();

            OutPilot._Name_Last = PilotNames.Random_Last();

            GO_Pilot.name = OutPilot.Name_Full;
            GO_Pilot.transform.parent = inTeam.gameObject.transform;

            return OutPilot;
        }



        public static Pilot CreateFromSerialized(Teams.Team inTeam, _Pilot_Serialized inSerialized)
        {
            if (inTeam == null)
                return null;

            GameObject GO_Pilot = new GameObject();
            Pilot OutPilot = GO_Pilot.AddComponent<Pilot>();


            if (inSerialized.Level <= 0)
                inSerialized.Level = 1;

            OutPilot.Level = inSerialized.Level;

            if (inSerialized.Level_XPThisLevel < 0)
                inSerialized.Level_XPThisLevel = 0;

            OutPilot.Level_XPThisLevel = inSerialized.Level_XPThisLevel;

            if (Enum.IsDefined(typeof(PilotGender), inSerialized.Gender))
                OutPilot.Gender = (PilotGender)inSerialized.Gender;
            else
                OutPilot.Gender = PilotGender.Unknown;


            if (string.IsNullOrWhiteSpace(inSerialized.Name_First))
            {
                if (OutPilot.Gender == PilotGender.Male)
                    OutPilot._Name_First = PilotNames.Random_First_Male();
                else if (OutPilot.Gender == PilotGender.Female)
                    OutPilot._Name_First = PilotNames.Random_First_Female();
                else
                    OutPilot._Name_First = PilotNames.Random_First_Male();
            }
            else
                OutPilot._Name_First = inSerialized.Name_First;


            if (string.IsNullOrWhiteSpace(inSerialized.Name_Last))
                OutPilot._Name_Last = PilotNames.Random_Last();
            else
                OutPilot._Name_Last = inSerialized.Name_Last;


            if (!string.IsNullOrWhiteSpace(inSerialized.CallSign))
                OutPilot._Callsign = inSerialized.CallSign;

            //ToDo ShipIDPreferences:
            //string.Join(",", )
            //"".Split(",")

            GO_Pilot.name = OutPilot.Name_Full;
            GO_Pilot.transform.parent = inTeam.gameObject.transform;

            return OutPilot;
        }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================


        public override string ToString()
        {
            return $"{Name_Full} - Level:{_Level}";
        }
    }
}