using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace mat
{
    class Act
    {
        public static Random rmd = new Random();
        public static DateTime endtime;
        public static void Simulate()
        {
            DateTime starttime = DateTime.Now;
            endtime = starttime.AddHours(6);
            for (int i = 0; i < Program.maxyears; i++)
            {
                Program.year = i + 1;
                int randomid = rmd.Next(1, Program.blobs.Count) - 1;
                blob randomblob = Program.blobs[randomid];
                // Console.WriteLine("Random blob: age: " + randomblob.age + " children: " + randomblob.children + " gene: " + randomblob.gene + " skill: " + randomblob.skills);
                Program.hardnessforfood = Program.blobs.Count / Program.year * 2;
                Program.skillcombined = 0;
                Program.diedblobs.Clear();
                Program.newblobs.Clear();
                var sw = Stopwatch.StartNew();
                Foreachblob();
                sw.Stop();
                Program.blobs.AddRange(Program.newblobs);
                foreach (var blob in Program.diedblobs)
                {
                    blob.Deathyear = Program.year;
                    Program.blobs.Remove(blob);
                }
                Program.deadblobs.AddRange(Program.diedblobs);

                if (Program.blobs.Count == 0)
                {
                    Console.WriteLine("Extinction in year " + Program.year);
                    i = Program.maxyears;
                    Program.extinction = true;
                }
                else
                {
                    if (DateTime.Now > endtime)
                    {
                        Console.WriteLine("endtime in year " + Program.year);
                        i = Program.maxyears;
                    }
                    if (Program.blobs.Count > Program.maxpop)
                    {
                        Program.maxpop = Program.blobs.Count;
                        if (Program.blobs.Count > Program.maxblobs)
                        {
                            Console.WriteLine("populationmax in year " + Program.year);
                            i = Program.maxyears;
                        }
                    }
                    if (sw.ElapsedMilliseconds > 500000)
                    {
                        Console.WriteLine("pcmax in year " + Program.year);
                        i = Program.maxyears;
                    }
                    Program.LogYear();
                }
            }
        }




        public static void Foreachblob()
        {
            foreach (var blob in Program.blobs)
            {
                Program.skillcombined += blob.skills;
                int chancedeath = rmd.Next(0, 10) * rmd.Next(0, 10);
                if (chancedeath + blob.age > 150)
                {
                    blob partner = Program.blobs.FirstOrDefault(e => e.partner == blob.id);
                    Program.diedblobs.Add(blob);
                    if (partner != null)
                    {
                        Program.diedblobs.Add(partner);
                    }
                }
                else
                {
                    if (blob.food > Program.hardnessforfood)
                    {
                        if (blob.partner != 0)
                        {
                            Happenings.givesingleBirth(blob);
                        }
                        else
                        {
                            if (blob.age > 24)
                            {
                                blob partnerblob = Program.blobs.FirstOrDefault(bl => bl.male != blob.male && bl.gene == blob.gene);
                                if (partnerblob != null && partnerblob.partner == 0)
                                {
                                    Happenings.giveBirth(blob, partnerblob);
                                    blob.partner = partnerblob.id;
                                    partnerblob.partner = blob.id;
                                    partnerblob.lastname = blob.lastname;

                                }
                                else
                                {
                                    blob.IQ++;
                                }
                            }
                            else
                            {
                                blob.IQ++;
                            }
                            
                        }
                    }
                    else
                    {
                        var getfood = rmd.Next(0, 10);
                        blob.food += getfood * blob.skills;
                    }
                    blob.age++;
                }
            }
        }

    }
}
