using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic.Data.Pilots
{
    public static class PilotNames
    {
        private static List<string> First_Male = new List<string>() {
            "Bastian",
            "Cullen",
            "Gray",
            "Emmett",
            "Fry",
            "Gaius",
            "Kael",
            "Logan",
            "Ludo",
            "Marty",
            "Milo",
            "Rylan",
            "Sauron",
            "Tyrian",
            "Grubb",
            "Araym",
            "Corgan",
            "Abel",
            "Altos",
            "Farris",
            "Boba",
        };



        private static List<string> First_Female = new List<string>()
        {
            "Alma",
            "Artimis",
            "Maya",
            "Led",
            "Selina",
            "Aquila",
            "Aurora",
            "Dawn",
            "Claire",
            "Kara",
            "Raven",
            "Eve",
            "Emma",
            "Kala",
            "Leela",
            "Jade",
            "Alexis",
            "Lexa",
            "Celeste"
        };



        private static List<string> Last = new List<string>()
        {
            "Domel",
            "Hadrian",
            "Connaught",
            "Idris",
            "Baltar",
            "McFly",
            "Septerra",
            "Underlost",
            "Ackbar",
            "Arak",
            "Asimov",
            "Herbert",
            "Galway",
            "Blackwell",
            "Dre",
            "Starshock",
            "Landwell",
            "Columbo",
            "Precipice",
            "Annodue",
            "Fortuna",
            "Brannigan",
            "Farnsworth",
            "Hubris",
            "Hawkins",
            "Hayter",
            "Thorne",
            "Starrunner"
        };



        private static List<string> Droid = new List<string>()
        {
            "Runner",
            "Lobo",
            "B.O.B.",
            "Dot",
            "Matrix",
            "Hal",
            "Shodan",
            "Shock",
            "Analyzer",
            "Beta"
        };







        public static string Random_First_Male()
        {
            return First_Male[GameLoop.MAIN.RandomHelper.Primary.Next(First_Male.Count)];
        }



        public static string Random_First_Female()
        {
            return First_Female[GameLoop.MAIN.RandomHelper.Primary.Next(First_Female.Count)];
        }



        public static string Random_Last()
        {
            return Last[GameLoop.MAIN.RandomHelper.Primary.Next(Last.Count)];
        }




        public static string Random_Robot()
        {
            return Droid[GameLoop.MAIN.RandomHelper.Primary.Next(Droid.Count)];
        }
    }
}
