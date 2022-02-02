using IpiPedia.bdd.collection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace IpiPedia
{
    class Program
    {

        private LinkedList<Personnage> list;

        public Program()
        {
            this.list =
                Personnage.GetDataFromFile($"{Directory.GetCurrentDirectory()}/../../../bdd/base.txt");
        }

        public void Start()
        {
            bool wantToLeave = false;
            printHelp();
            while (!wantToLeave)
            {
                Console.Write(">>> ");
                string rep = Console.ReadLine().ToUpper().Trim();

                switch (rep)
                {
                    case "AFFICHER":
                        Console.WriteLine("VOICI UNE LISTE DE PERSONNAGE");
                        afficherPersonnages();
                        break;

                    case "INFORMATION":
                        Console.WriteLine("Saisissez le nom d'un personnage");
                        string characterName = Console.ReadLine().Trim();
                        afficherInformation(characterName);
                        break;

                    case "NAISSANCE":
                        Console.WriteLine("Saisissez une année et on trouvera tous les personnages née avant (ex : 10/11/1914)");
                        string dateSaisi = Console.ReadLine().Trim();
                        DateTime dateSearch = DateTime.MinValue;
                        DateTime.TryParse(dateSaisi, out dateSearch);
                        if (dateSearch.Equals(DateTime.MinValue))
                        {
                            Console.WriteLine("La saisie est incorrecte");
                        }
                        else
                        {
                            afficherNaissance(dateSearch);
                        }
                        break;

                    case "CLEF":
                        Console.WriteLine("Saisissez un mot clef");
                        string clefs = Console.ReadLine().Trim();
                        keySearch(clefs);
                        break;

                    case "QUIT":
                        wantToLeave = true;
                        break;

                    case "HELP":
                        printHelp();
                        break;

                    case "CLS":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Commande non reconnue, HELP pour plus d'info");
                        break;
                }
            }
        }

        private void printHelp()
        {
            Console.WriteLine("Bienvenue sur Ipipedia, l'encyclopedie qui va mettre a mal la plus grande encyclopedie d'internet nommée Wikipedia");
            Console.WriteLine("AFFICHER : Afficher toutes les personne dans la base de donnée");
            Console.WriteLine("INFORMATION : Affiche les infos d'une personne");
            Console.WriteLine("NAISSANCE : Affiche les personnes nait avant la date inscrite");
            Console.WriteLine("CLEF : Fait une recherche des personnes par mot clef");
            Console.WriteLine("CLS : Efface la console");
            Console.WriteLine("HELP : Affiche l'aide");
            Console.WriteLine("QUIT : Quitte le programme");

        }


        //requete affichage de tout les personnages
        public void afficherPersonnages()
        {
            var query = from personnage in list
                    orderby personnage.name ascending
                    select personnage.name;

            foreach(var item in query)
            {
                Console.WriteLine(item);
            }
        }

        public void afficherInformation(string name)
        {
            var request = from personnage in list
                          where personnage.name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                          select personnage.description;
            if (request.Count() == 0)
            {
                Console.WriteLine(name + " est introuvable !");
            }
            else
            {
                Console.WriteLine("Voici la description de " + name + " :\n" + request.First());
            }
        }


        public void afficherNaissance(DateTime d)
        {
            var request = from personnage in list
                          where personnage.birthDate < d
                          orderby personnage.name ascending
                          select personnage.name;

            if (request.Count() == 0)
            {
                Console.WriteLine("Aucune personne n'a été trouvée !");
            }
            else
            {
                Console.WriteLine("Voici les personnes qui sont nait avant le " + d.ToString("d") + " :");

                foreach (var item in request)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void keySearch(string clefs)
        {
            List<String> answers = new List<String>();

            var tok = clefs.Split(",");
            foreach (var item in tok)
            {
                item.Trim();
                var request = from personnage in list
                              where personnage.description.Contains(item, StringComparison.InvariantCultureIgnoreCase)
                              orderby personnage.name ascending
                              select personnage.name;

                answers = answers.Concat(request).ToList();
            }

            answers = answers.Distinct().ToList();
            answers.Sort();

            if (answers.Count() == 0)
            {
                Console.WriteLine("Aucune personne n'a été trouvée !");
            }
            else
            {
                Console.WriteLine("Voici les personnes associés aux mots \"" + clefs + "\" :");

                foreach (var item in answers)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}

