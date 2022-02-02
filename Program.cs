using IpiPedia.bdd.collection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IpiPedia
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine("Bienvenue sur Ipipedia, l'encyclopedie qui va mettre a mal la plus grande encyclopedie d'internet nommée Wikipedia");
           

            Console.WriteLine("Si vous voulez afficher le nom des personnages écrivez AFFICHER,");
            Console.WriteLine("Si vous voulez voir les informations d'un personnage écrivez INFORMATION,");
            Console.WriteLine("Si vous voulez voir les personnes née avant une certaine année écrivez NAISSANCE");
            string rep=Console.ReadLine();

            switch (rep)
            {
                case "AFFICHER":
                    Console.WriteLine("VOICI UNE LISTE DE PERSONNAGE");
                    afficherPersonnages();
                    break;
                case "INFORMATION":
                    Console.WriteLine("Saisissez le nom d'un personnage");
                    string characterName = Console.ReadLine();
                    afficherInformation(characterName);
                    break;
                case "NAISSANCE":
                    Console.WriteLine("Saisissez une année et on trouvera tous les personnages née avant");
                    string dateSaisi = Console.ReadLine();
                    int dateSearch = int.Parse(dateSaisi);
                    //afficherNaissance(dateSearch);
                    break;
                default:
                    Console.WriteLine("Nothing");
                    break;
            }


        }
            //requete affichage de tout les personnages
        static void afficherPersonnages()
        {
            LinkedList<Personnage> list =
                Personnage.GetDataFromFile($"{Directory.GetCurrentDirectory()}/../../../bdd/base.txt");
            foreach(var personnage in list)
            {
                Console.WriteLine(personnage.name);
            }
        }

        static void afficherInformation(string name)
        {
            LinkedList<Personnage> list =
               Personnage.GetDataFromFile($"{Directory.GetCurrentDirectory()}/../../../bdd/base.txt");

            var request = from personnage in list
                          where personnage.name == name
                          select personnage.description;

            Console.WriteLine("Voici une description de " + name + " : " + request);
        }
        



       


    }
    }

