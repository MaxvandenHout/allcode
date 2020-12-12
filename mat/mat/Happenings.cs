using System;
using System.Collections.Generic;
using System.Text;

namespace mat
{
    class Happenings
    {
        public static Random rmd = new Random();

        public static void giveBirth(blob blob, blob partnerblob)
        {
            double skillchance = rmd.NextDouble() * 2.2;
            int randogene = rmd.Next(1, 5);
            int calcgene = randogene + blob.gene;
            int gender = rmd.Next(0, 2);
            if (calcgene > 1000)
            {
                calcgene -= 1000;
            }
            blob newblob = new blob
            {
                id = Program.b + 1,
                age = 0,
                children = 0,
                food = 1,
                skills = blob.skills * skillchance,
                gene = calcgene,
                father = blob.id,
                mother = partnerblob.id,
                firstname = Setup.firstnames[rmd.Next(0, Setup.Firstnamecount)],
                lastname = randogene < 3 ?blob.lastname: Setup.surnames[rmd.Next(0, Setup.Surnamecount)],
                Birthyear = Program.year,
                male = gender == 0 ? true : false
            };
            //Console.WriteLine("gene: " + newblob.gene + " randogene: " + randogene + " oldgene: " + blob.gene);
            Program.newblobs.Add(newblob);
            blob.food = 0;
            blob.children++;
            partnerblob.food = 0;
            partnerblob.children++;
            Program.b++;
        }

        public static void givesingleBirth(blob blob)
        {
            double skillchance = rmd.NextDouble() * 2.2;
            int randogene = rmd.Next(1, 5);
            int calcgene = randogene + blob.gene;
            int gender = rmd.Next(0, 2);
            if (calcgene > 1000)
            {
                calcgene -= 1000;
            }
            blob newblob = new blob
            {
                id = Program.b + 1,
                age = 0,
                children = 0,
                food = 1,
                skills = blob.skills * skillchance,
                gene = calcgene,
                father = blob.id,
                firstname = Setup.firstnames[rmd.Next(0, Setup.Firstnamecount)],
                lastname = blob.lastname,
                Birthyear = Program.year,
                male = gender == 0 ? true : false
            };
            //Console.WriteLine("gene: " + newblob.gene + " randogene: " + randogene + " oldgene: " + blob.gene);
            Program.newblobs.Add(newblob);
            blob.food = 0;
            blob.children++;
            Program.b++;
        }

    }
}
