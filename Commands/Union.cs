using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpiPedia.Commands
{
    class Union : Command
    {
        new public static string name = "union";

        private static Dictionary<string, Command> cmdList = Shell.commandList;
        private static char unionSeparator = '|';

        public Union()
        {
            description = "union <cmd1 args...> | <cmd2 args...> ... : Fusionne les résultats des commandes";
        }

        public override Answer Proc(string args)
        {
            List<string> messages = new List<string>();
            Answer a = new Answer();
            args = args.Trim();
            if (args.Equals(""))
            {
                a.errorCode = Answer.ERROR.NO_ARG;
                return a;
            }

            string[] lines = args.Split(unionSeparator);
            foreach (string line in lines)
            {
                string execLine = line.Trim();
                string[] tok = execLine.Split(" ", 2);
                string command = tok[0].Trim(); // command to exec
                if (tok.Length > 1)
                    args = tok[1]; // args for command

                if (cmdList.ContainsKey(command))
                {
                    Command cmd;
                    cmdList.TryGetValue(command, out cmd);

                    Answer subAnswer = cmd.Proc(args);

                    if (subAnswer.errorCode != Answer.ERROR.NO_ERROR)
                    {
                        a.errorCode = Answer.ERROR.INCORECT_ARG;
                        return a;
                    }

                    List<string> ms = subAnswer.message.Split(Command.cmdSeparators).ToList();

                    messages = messages.Union(ms).ToList();

                    
                }
                else
                {
                    a.errorCode = Answer.ERROR.INCORECT_ARG;
                    return a;
                }
            }

            foreach (string item in messages)
            {
                a.message += item + '\n';
            }
            if (a.message.Length > 0)
                a.message = a.message.Remove(a.message.Length - 1);
            a.errorCode = Answer.ERROR.NO_ERROR;
            return a;
        }
    }
}
