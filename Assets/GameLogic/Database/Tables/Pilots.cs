using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mono.Data.Sqlite;
using UnityEngine;

namespace Assets.GameLogic.Database.Tables
{
    public static class PilotTable
    {
        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public static List<Data.Pilots._Pilot_Serialized> ReadPlayerPilots(int inMaxPilots = 100000)
        {
            List<Data.Pilots._Pilot_Serialized> OutPilots = new List<Data.Pilots._Pilot_Serialized>();

            if (DatabaseManager.IsConnected)
            {
                using (IDbCommand Cmd = DatabaseManager.Conn.CreateCommand())
                {
                    if(inMaxPilots < 1)
                        inMaxPilots = Math.Abs(inMaxPilots);

                    Cmd.CommandText = $"SELECT Name_First, Name_Last, Callsign, Level, Level_XPThisLevel, Gender, ShipIDPreferences FROM Pilot WHERE 1 ORDER BY Level DESC, Level_XPThisLevel DESC LIMIT {inMaxPilots};";

                    using (IDataReader Rdr = Cmd.ExecuteReader())
                    {
                        while(Rdr.Read())
                        {
                            Data.Pilots._Pilot_Serialized iPilot = new Data.Pilots._Pilot_Serialized();

                            iPilot.Name_First = Rdr.GetString(Rdr.GetOrdinal("Name_First"));
                            iPilot.Name_Last = Rdr.GetString(Rdr.GetOrdinal("Name_Last"));

                            if (!Rdr.IsDBNull(Rdr.GetOrdinal("Callsign")))
                                iPilot.CallSign = Rdr.GetString(Rdr.GetOrdinal("Callsign"));

                            iPilot.Level = Rdr.GetInt32(Rdr.GetOrdinal("Level"));
                            iPilot.Level_XPThisLevel = Rdr.GetInt32(Rdr.GetOrdinal("Level_XPThisLevel"));
                            iPilot.Gender = Rdr.GetByte(Rdr.GetOrdinal("Gender"));

                            if (!Rdr.IsDBNull(Rdr.GetOrdinal("ShipIDPreferences")))
                                iPilot.CallSign = Rdr.GetString(Rdr.GetOrdinal("ShipIDPreferences"));

                            OutPilots.Add(iPilot);
                        }
                    }
                }
            }

            return OutPilots;
        }
    }
}
