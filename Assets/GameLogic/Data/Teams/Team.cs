using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using Assets.GameLogic.Utility;

namespace Assets.GameLogic.Data.Teams
{
    public class Team : ExposableMonobehaviour
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        [HideInInspector, SerializeField]
        protected int _ID;

        [ExposeProperty]
        public int ID { get { return _ID; } protected set { _ID = value; } }



        [HideInInspector, SerializeField]
        protected string _Name;

        [ExposeProperty]
        public string Name { get { return _Name; } set { _Name = value; } }



        [HideInInspector, SerializeField]
        protected bool _PlayerControlled;

        [ExposeProperty]
        public bool PlayerControlled { get { return _PlayerControlled; } set { _PlayerControlled = value; } }



        public List<Pilots.Pilot> Pilots = new List<Pilots.Pilot>();



        [HideInInspector, SerializeField]
        protected int _CrewPopulation;

        /// <summary>
        /// The number of Pilots in the fleet.  If the current list of pilots does not meet this value, new random pilots will be generated to meet the capacity
        /// </summary>
        [ExposeProperty]
        public int CrewPopulation { get { return _CrewPopulation; } set { _CrewPopulation = value; } }



        [Header("Team Branding")]
        public Color TeamColor_Primary = Color.gray;
        public Color TeamColor_Secondary = Color.gray;
        public Color TeamColor_Tertiary = Color.gray;



        /// <summary>
        /// The team's logo used for branding ships and UI
        /// </summary>
        public Sprite _Logo = null;





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        /// <summary>
        /// Crates an entirely new Team with a name
        /// </summary>
        /// <param name="inName"></param>
        /// <returns></returns>
        public static Team Create(string inName, bool inPlayerControlled = false, int inCrewPopulation = 50)
        {
            if (GameLoop.MAIN == null || GameLoop.MAIN.TeamManager == null)
            {
                Debug.LogError("Could not Create TeamManager.  TeamManager could not be found in scene.");
                return null;
            }
            else if (GameLoop.MAIN.TeamManager.Teams == null)
                GameLoop.MAIN.TeamManager.Teams = new List<Team>();

            GameObject GO_Team = new GameObject("Team");
            Team NewTeam = GO_Team.AddComponent<Team>();
            GO_Team.transform.parent = GameLoop.MAIN.TeamManager.gameObject.transform;

            NewTeam._PlayerControlled = inPlayerControlled;
            NewTeam._ID = GameLoop.MAIN.TeamManager.Teams.Count + 1;

            if (!string.IsNullOrWhiteSpace(inName))
                NewTeam._Name = inName;

            string TeamName = !string.IsNullOrWhiteSpace(NewTeam._Name) ? NewTeam._Name.Substring(0, NewTeam._Name.Length > 128 ? 128 : NewTeam._Name.Length) : NewTeam._ID.ToString();

            GO_Team.name = TeamName;

            NewTeam._CrewPopulation = inCrewPopulation;

            if(NewTeam.PlayerControlled)
            {
                NewTeam.TeamColor_Primary = Color.blue;
                NewTeam.TeamColor_Secondary = Color.blue;
                NewTeam.TeamColor_Tertiary = Color.blue;
            }
            else
            {
                NewTeam.TeamColor_Primary = Color.red;
                NewTeam.TeamColor_Secondary = Color.red;
                NewTeam.TeamColor_Tertiary = Color.red;
            }

            return NewTeam;
        }



        public void LoadPlayerPilotsFromDatabase(int inAmount = 50)
        {
            if (inAmount < 1)
                return;

            List<Pilots._Pilot_Serialized> PilotsFromDB = Database.Tables.PilotTable.ReadPlayerPilots();

            if(PilotsFromDB != null && PilotsFromDB.Count > 0)
            {
                Pilots.Pilot newPilot = null;

                for(int iPilot = 0; iPilot < PilotsFromDB.Count; iPilot++)
                {
                    newPilot = Data.Pilots.Pilot.CreateFromSerialized(this, PilotsFromDB[iPilot]);

                    if (newPilot != null)
                        this.Pilots.Add(newPilot);
                }
            }
        }



        public void GenerateNPCPilots(int inAmount = 10)
        {
            if (inAmount < 1)
                return;

            Pilots.Pilot iPilot = null;

            for(int i=0;i<inAmount;i++)
            {
                iPilot = Data.Pilots.Pilot.CreateRandom(this);

                if(iPilot != null)
                    this.Pilots.Add(iPilot);
            }
        }
    }
}