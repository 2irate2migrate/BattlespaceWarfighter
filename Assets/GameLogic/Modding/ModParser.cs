using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Assets.GameLogic.Modding
{
    public static class ModParser
    {
        public static List<Data.Ships._ShipBase> Ships = new List<Data.Ships._ShipBase>();







        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public static void LoadDefaultMod()
        {
            string Path = $"{Application.dataPath}/StreamingAssets/Mods/Default Mod";

            if (Directory.Exists(Path))
            {
                string[] Files = Directory.GetFiles(Path, "*.json", SearchOption.TopDirectoryOnly);

                if (Files.Count() < 1)
                {
                    Debug.LogError("No *.json Mod files found for the Default Mod.");
                    return;
                }

                string FileText = "";
                JObject j = null;

                JEnumerable<JToken> tTokens;
                List<JToken> Factions = new List<JToken>();
                List<JToken> Interceptors = new List<JToken>();
                List<JToken> Bombers = new List<JToken>();

                foreach (var iFile in Files)
                {
                    FileText = File.ReadAllText(iFile);

                    if (string.IsNullOrWhiteSpace(FileText))
                        continue;

                    j = JObject.Parse(FileText);

                    if (j["factions"] != null)
                    {
                        tTokens = j["factions"].Children();
                        if (tTokens.Count() > 0)
                            Factions.AddRange(tTokens);
                    }

                    if (j["ships.interceptors"] != null)
                    {
                        tTokens = j["ships.interceptors"].Children();
                        if (tTokens.Count() > 0)
                            Interceptors.AddRange(tTokens);
                    }

                    if (j["ships.bombers"] != null)
                    {
                        tTokens = j["ships.bombers"].Children();
                        if (tTokens.Count() > 0)
                            Bombers.AddRange(tTokens);
                    }
                }

                ParseFactions(ref Factions);
                ParseInterceptors(ref Interceptors);
                ParseBombers(ref Bombers);


                Debug.Log("Finished Parsing Default Mod.");
                Debug.Log($"{Ships.Count} Ships found.  Interceptors:{Ships.OfType<Data.Ships.Interceptor>().Count()} | Bombers:{Ships.OfType<Data.Ships.Bomber>().Count()}");

                Debug.Log("Ship Manifest");
                Debug.Log("-----------------------");
                foreach (var Ship in Ships)
                {
                    Debug.Log(Ship.ToString());
                }
            }
            else
                Debug.LogError("Could not find Default Mod directory in Streaming Assets folder.");
        }



        private static void ParseFactions(ref List<JToken> inFactions)
        {
            if (inFactions == null || inFactions.Count < 1)
                return;

            Data.Factions.Faction iFaction = null;

            foreach (JToken Result in inFactions)
            {
                iFaction = new Data.Factions.Faction(Result.ToObject<Data.Factions._FactionSerialized>());

                if(iFaction != null)
                {
                    GameLoop.MAIN.DataManager.FactionManager.Add(iFaction);
                }
            }
        }



        private static void ParseInterceptors(ref List<JToken> inInterceptors)
        {
            if (inInterceptors == null || inInterceptors.Count < 1)
                return;

            Data.Ships.Interceptor iShip = null;

            foreach (JToken Result in inInterceptors)
            {
                iShip = new Data.Ships.Interceptor(Result.ToObject<Data.Ships._ShipSerialized>());

                if (iShip != null)
                    Ships.Add(iShip);
            }
        }



        private static void ParseBombers(ref List<JToken> inBombers)
        {
            if (inBombers == null || inBombers.Count < 1)
                return;

            Data.Ships.Bomber iShip = null;

            foreach (JToken Result in inBombers)
            {
                iShip = new Data.Ships.Bomber(Result.ToObject<Data.Ships._ShipSerialized>());

                if (iShip != null)
                    Ships.Add(iShip);
            }
        }
    }
}