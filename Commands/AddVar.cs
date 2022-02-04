using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpiPedia.Commands
{
    class AddVar : Command
    {
        new public static string name = "$";

        private static Dictionary<string, Answer> vars = Shell.vars;

        public AddVar()
        {
            description = "$ <myVar> <cmd> : met le résultat de <cmd> dans <myVar>";
        }

        public override Answer Proc(string args)
        {
            Answer a = new Answer();

            string[] tok = args.Split(Command.cmdSeparators, 2);

            if (vars.ContainsKey(tok[0]))
            {
                vars.Remove(tok[0]);
            }
            vars.Add(tok[0], new Answer());
            return a;
        }
    }
}
