using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IpiPedia.Commands
{
    class Script : Command
    {
        new public static string name = "script";

        private static Dictionary<string, Command> cmdList = Shell.commandList;

        public Script()
        {
            description = "script <file name> : Lance le script <file name>";
        }
        public override Answer Proc(string args)
        {
            args = args.Trim();
            Answer a = new Answer();
            if (!File.Exists(args))
            {
                a.errorCode = Answer.ERROR.INCORECT_ARG;
                return a;
            }
            a.errorCode = Answer.ERROR.NO_ERROR;
            
            foreach (string fLine in System.IO.File.ReadLines(args))
            {
                string line = fLine.Trim();
                string[] tok = line.Split(" ", 2);
                string command = tok[0].Trim(); // command to exec
                if (tok.Length > 1)
                    args = tok[1]; // args for command

                if (cmdList.ContainsKey(command))
                {
                    Command cmd;
                    cmdList.TryGetValue(command, out cmd);

                    Shell.PrintAnswer(cmd.Proc(args));

                    
                }
                else
                {
                    a.message = "Une erreur est survenu lors de la lecture du script, \"" + line + "\" non reconnue\n";
                    return a;
                }
            }
            a.message = "Fin du script";
            return a;
        }
    }
}
