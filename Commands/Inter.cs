using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpiPedia.Commands
{
    class Inter : Command
    {
        new public static string name = "inter";

        private static Dictionary<string, Command> cmdList = Shell.commandList;
        private static char unionSeparator = '&';

        public Inter()
        {
            description = "inter <cmd1 args...> & <cmd2 args...> ... : Sélectionne les résultats commun des commandes";
        }

        public override Answer Proc(string args)
        {
            string[] messages1 = { };
            string[] messages2 = { };
            Answer a = new Answer();
            args = args.Trim();
            if (args.Equals(""))
            {
                a.errorCode = Answer.ERROR.NO_ARG;
                return a;
            }

            string[] lines = args.Split(unionSeparator);
            for(int i=0; i < lines.Length; i++)
            {
                string line = lines[i];
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

                    messages1 = subAnswer.message.Split(Command.cmdSeparators);//on sépare nos msg
                    if (i == 0)//si c'est la première fois, on met msg 1 dans msg2
                    {
                        messages2 = messages1;
                    }
                    else
                    {//sinon on fait l'inter

                        var query = from m1 in messages1
                                    where messages2.Contains(m1.Trim())
                                    select m1.Trim();

                        messages2 = query.ToArray();
                    }
                   

                }
                else
                {
                    a.errorCode = Answer.ERROR.INCORECT_ARG;
                    return a;
                }
            }

            foreach (string item in messages2)
            {
                a.message += item + '\n';
            }

            if(a.message.Length > 0)
                a.message = a.message.Remove(a.message.Length - 1);
            a.errorCode = Answer.ERROR.NO_ERROR;
            return a;
        }
    }
}
