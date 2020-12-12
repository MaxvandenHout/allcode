using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mat
{
    class Write
    {
        public static Random rmd = new Random();
        public static void Ask()
        {
            
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Type an id: ");
                string id = Console.ReadLine();
                blob blob = Program.blobs.FirstOrDefault(b => b.id == Int32.Parse(id));
                Logblob(blob, "Your asked blob: ");
            }
        }

        public static void LoggingResults()
        {
            Random rmd = new Random();
            Console.WriteLine();
            if (Program.blobs.Count != 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int randomid = rmd.Next(1, Program.blobs.Count) - 1;
                    blob randomblob = Program.blobs[randomid];
                    // Console.WriteLine("Random blob: " + randomblob.id + " age: " + randomblob.age + " children: " + randomblob.children + " gene: " + randomblob.gene + " skill: " + randomblob.skills);
                }

            }

            Console.WriteLine();
            blob maxage = new blob(); 
            blob maxchildren = new blob();
            blob maxgene= new blob();
            blob maxIQ = new blob();


            Console.WriteLine();
            double allSkillCombined = 0;
            int allBabiesCombined = 0;
            int allAgeCombined = 0;
            foreach (var blob in Program.blobs)
            {
                allSkillCombined += blob.skills;
            }
            foreach (var blob in Program.deadblobs)
            {
                allBabiesCombined += blob.children;
                allAgeCombined += blob.age;
            }
            double avarageSkill = (double)allSkillCombined / Program.blobs.Count;
            double avarageBabies = (double)allBabiesCombined / Program.deadblobs.Count;
            double avarageAge = (double)allAgeCombined / Program.deadblobs.Count;
            int all = Program.blobs.Count + Program.deadblobs.Count;
            Console.WriteLine("living blobs: " + Program.blobs.Count);
            Console.WriteLine("maximum blobs: " + Program.maxpop);
            Console.WriteLine("All blobs that ever lived: " + all);
            Console.WriteLine("with avarage skill: " + avarageSkill);
            Console.WriteLine("dead blobs: " + Program.deadblobs.Count);
            Console.WriteLine("all babies: " + allBabiesCombined);
            Console.WriteLine("AvarageBabies: " + avarageBabies);
            Console.WriteLine("avarage age: " + avarageAge);
            Program.blobs.AddRange(Program.deadblobs);

            foreach (var blob in Program.blobs)
            {
                if (blob.age > maxage.age)
                {
                    maxage = blob;
                }
                if (blob.children > maxchildren.children)
                {
                    maxchildren = blob;
                }
                if (blob.gene > maxgene.gene)
                {
                    maxgene = blob;
                }
                if (blob.IQ > maxIQ.IQ)
                {
                    maxIQ = blob;
                }
            }
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("First Name | Last Name  |   Age  |  Birth-Death  |  Children");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Logblob(maxage, "max age");
            Logblob(maxchildren, "max children");
            Logblob(maxgene, "max gene");
            Logblob(maxIQ, "max IQ");
        }

        public static void Logblob(blob blob, string why)
        {
            if (blob != null)
            {
                Console.WriteLine(String.Format("{0,-10} | {1,-10} | {2,6} | {3,13} | {4,10}", blob.firstname, blob.lastname, blob.age, "(" + blob.Birthyear + "-" + blob.Deathyear + ")", blob.children));
                if (blob.partner != 0 && blob.partner != Program.partnerid)
                {
                    blob partner = Program.blobs.FirstOrDefault(bl => bl.id == blob.partner);
                    Program.partnerid = blob.id;
                    Logblob(partner, "Partner:");
                }
            }
            //Console.WriteLine();
        }
    }
}
