using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mono.Data.Sqlite;
using UnityEngine;

namespace Assets.GameLogic.Database
{
    public static class DatabaseManager
    {
        //==============================================================================
        //
        //                                    CONSTANTS
        //
        //==============================================================================



        /// <summary>
        /// The path to the database
        /// </summary>
        private static readonly string DATABASE_PATH = $"URI=file:{Application.dataPath}/StreamingAssets/Database.db";





        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        public static IDbConnection Conn = null;








        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public static bool IsConnected
        {
            get { return Connect(); }
        }



        public static bool Connect()
        {
            if (Conn != null)
            {
                if(Conn.State == ConnectionState.Open)
                    return true;
                else if(Conn.State == ConnectionState.Closed)
                {
                    return _TryOpenDB();
                }
            }
            else
            {
                Conn = new SqliteConnection(DATABASE_PATH);
                return _TryOpenDB();
            }

            return false;
        }



        private static bool _TryOpenDB()
        {
            try
            {
                if (Conn != null)
                    Conn.Open();

                return Conn.State == ConnectionState.Open;
            }
            catch(Exception e)
            {
                Debug.LogError($"Could not open SQLite database at location:'{DATABASE_PATH}'!");
                return false;
            }
        }



        public static void Close()
        {
            if (Conn != null)
            {
                Conn.Close();
                Conn = null;
            }
        }
    }
}
