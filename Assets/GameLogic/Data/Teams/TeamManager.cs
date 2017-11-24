using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Data.Teams
{
    public class TeamManager : MonoBehaviour
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        public List<Team> Teams = new List<Team>();





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public void Init_PlayerTeam()
        {
            Team NewTeam = Team.Create("Player", true, 50);

            if(NewTeam != null)
                Teams.Add(NewTeam);

            //Load Pilots from DB
            NewTeam.LoadPlayerPilotsFromDatabase(NewTeam.CrewPopulation);

            //Generate missing pilots to fill out the fleet
            NewTeam.GenerateNPCPilots(NewTeam.CrewPopulation - NewTeam.Pilots.Count);
        }
    }
}