using IpiPedia.bdd.collection;
using IpiPedia.Commands;
using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IpiPedia
{
    class Shell
    {
        public static LinkedList<Personnage> list = Personnage.GetDataFromFile($"{Directory.GetCurrentDirectory()}/../../../bdd/base.txt");
        public static Dictionary<string, Command> commandList;
        public static Dictionary<string, Answer> vars;

        public Shell()
        {
            vars = new Dictionary<string, Answer>();
            commandList = new Dictionary<string, Command>();

            commandList.Add(Help.name, new Help());
            commandList.Add(Man.name, new Man());
            commandList.Add(Echo.name, new Echo());
            commandList.Add(Afficher.name, new Afficher());
            commandList.Add(Info.name, new Info());
            commandList.Add(BirthDate.name, new BirthDate());
            commandList.Add(KeySearch.name, new KeySearch());
            commandList.Add(Union.name, new Union());
            commandList.Add(Inter.name, new Inter());

        }

        public void Run()
        {
            bool needLeave = false;
            Console.WriteLine(commandList.GetValueOrDefault("help").Proc("").message);

            while (!needLeave)
            {
                string line, command, args = "";
                string[] tok;

                Console.Write(">>> ");

                line = Console.ReadLine().Trim();
                tok = line.Split(" ", 2);
                command = tok[0].Trim(); // command to exec
                if (tok.Length > 1)
                    args = tok[1]; // args for command


                if (command.Equals("quit"))
                {
                    needLeave = true;
                }
                else if (commandList.ContainsKey(command))
                {
                    Command cmd;
                    commandList.TryGetValue(command, out cmd);

                    Answer a = cmd.Proc(args);

                    PrintAnswer(a);
                }
                else
                {
                    Console.WriteLine("La commande n'existe pas");
                }
            }
        }

        private void PrintAnswer(Answer a)
        {
            switch (a.errorCode)
            {
                case Answer.ERROR.NO_ERROR:
                    if (!a.message.Equals(""))
                        Console.WriteLine(a.message);
                    else
                        Console.WriteLine("La réponse de la commande est vide");
                    break;

                case Answer.ERROR.NO_ARG:
                    Console.WriteLine("Aucun argument n'a été donné");
                    break;

                case Answer.ERROR.INCORECT_ARG:
                    Console.WriteLine("Les arguments donnés sont incorrects");
                    break;

                case Answer.ERROR.INIT_ERROR:
                    Console.WriteLine("ERREUR dans la réponse de la commande (elle n'a pas été initialisée)");
                    break;

                default:
                    Console.WriteLine("Réponse inconnue");
                    break;

            }
        }
    }
}
