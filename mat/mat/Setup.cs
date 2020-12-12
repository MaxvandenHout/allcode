using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mat
{
    class Setup
    {
        public static List<string> surnames;
        public static List<string> firstnames;
        public static void getnames()
        {
            using (StreamReader r = new StreamReader("D:\\Programeren\\mat\\mat\\Recources\\surnames.json"))
            {
                string json = r.ReadToEnd();
                List<string> items = JsonConvert.DeserializeObject<List<string>>(json);
                surnames = items;
            }
            using (StreamReader r = new StreamReader("D:\\Programeren\\mat\\mat\\Recources\\first-names.json"))
            {
                string json = r.ReadToEnd();
                List<string> items = JsonConvert.DeserializeObject<List<string>>(json);
                firstnames = items;
            }
        }
        public static int Surnamecount;
        public static int Firstnamecount;

        public static void CreateÍnit()
        {
            Console.WriteLine("What should be the maximum number of years: ");
            var maxys = Console.ReadLine();
            Program.maxyears = Int32.Parse(maxys);
            Console.WriteLine();
            Console.WriteLine("What should be the maximum number of blobs: ");
            var maxbbs = Console.ReadLine();
            Program.maxblobs = Int32.Parse(maxbbs);
            Console.WriteLine();
            Console.WriteLine("How many to start: ");
            var start = Console.ReadLine();
            int startint = Int32.Parse(start);
            getnames();
            Surnamecount = surnames.Count;
            Firstnamecount = firstnames.Count;
            for (int i = 0; i < startint; i++)
            {   
                Random rmd = new Random();
                int gender = rmd.Next(0, 2);
                blob blob = new blob
                {
                    id = i,
                    children = 0,
                    age = 0,
                    food = 1,
                    skills = rmd.Next(0, 10),
                    gene = rmd.Next(1, 10),
                    firstname = firstnames[rmd.Next(0, Firstnamecount)],
                    lastname = surnames[rmd.Next(0, Surnamecount)],
                    male = gender == 0 ? true : false
                };
                Program.b++;
                Program.blobs.Add(blob);
                Console.WriteLine(blob.lastname);
            }
        }
    }
}
