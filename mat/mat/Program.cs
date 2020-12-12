using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mat
{
    class Program
    {
        public static int maxpop = 0;
        public static int year = 0;
        public static int partnerid = 0;
        public static bool extinction = false;
        public static int hardnessforfood = 0;
        public static int b = 0;
        public static int maxyears = 1000;
        public static int maxblobs = 1000;
        public static double skillcombined = 0;
        public static double avaragesskill = 0;
        public static List<blob> deadblobs = new List<blob>();
        public static List<blob> blobs = new List<blob>();
        public static List<blob> diedblobs = new List<blob>();
        public static List<blob> newblobs = new List<blob>();
        public static Random rmd = new Random();
        static void Main(string[] args)
        {
            Setup.CreateÍnit();
            Act.Simulate();
            Write.LoggingResults();
            Write.Ask();
        }
        
        public static void LogYear()
        {
            Console.WriteLine("Year " + year + " total blobs: " + blobs.Count);
        }
       

        

        
    }
}
