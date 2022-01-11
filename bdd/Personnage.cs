using System;
using System.Collections.Generic;
using System.Text;

namespace IpiPedia.bdd.collection
{
    class Personnage
    {
        public string name;
        public string description;
        public DateTime birthDate;
        public DateTime deathDate;
        public bool isDead;

        Personnage()
        {
            name = "NOT ASSIGNED";
            description = "NOT ASSIGNED";
            birthDate = new DateTime();
            deathDate = new DateTime();
            isDead = true;
        }
        Personnage(string name, string desc, DateTime birthDate, DateTime deathDate, bool isDead = false)
        {
            this.name = name;
            this.description = desc;
            this.birthDate = birthDate;
            this.deathDate = deathDate;
            this.isDead = isDead;
        }

        static public LinkedList<Personnage> GetDataFromFile(string path)
        {
            //data structure from file :
            //Name / Description / DD-MM-YYYY / DD-MM-YYYY 
            //                     birth date   death date
            LinkedList<Personnage> list = new LinkedList<Personnage>();
            foreach (string line in System.IO.File.ReadLines(path))
            {
                Personnage p = new Personnage();
                string[] tok = line.Split('/');
                //name
                string name = tok[0].Trim();
                //desc
                string desc = tok[1].Trim();
                //birth date
                string[] dateTok = tok[2].Split('-');
                DateTime birthDate = DateTime.Parse(tok[2]);
                
                //death date
                DateTime deathDate = birthDate;
                if (tok.Length >= 4)
                {
                    deathDate = DateTime.Parse(tok[3]);
                }

                p.name = name;
                p.description = desc;
                p.birthDate = birthDate;
                p.deathDate = deathDate;
                p.isDead = (tok.Length >= 4);

                list.AddLast(p);
            }
            return list;
        }

        public override string ToString()
        {
            int age = isDead ? (int)(deathDate - DateTime.Now).TotalDays / 365 : (int)DateTime.Now.Subtract(birthDate).TotalDays/365;
            string liveSentence = isDead ? $"est mort le {deathDate.ToString("d")}" : $"";
            string rv = $"{name}\nné le {birthDate.ToString("d")}, {liveSentence}\n{description}";
            return rv;
        }
    }
}
