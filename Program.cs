using IpiPedia.bdd.collection;
using System;
using System.Collections.Generic;
using System.IO;

namespace IpiPedia
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<Personnage> list =
                Personnage.GetDataFromFile($"{Directory.GetCurrentDirectory()}/../../../bdd/base.txt");
            foreach (var item in list)
            {
                Console.WriteLine("{0} : {1} - {2}", item.name, item.birthDate.ToString("d"), item.deathDate.ToString("d"));
            }
        }
    }
}
