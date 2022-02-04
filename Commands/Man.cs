using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpiPedia.Commands
{
    class Man : Command
    {
        new public static string name = "man";

        public Man()
        {
            description = "man <command> : affiche la description de <command>";
        }

        public override Answer Proc(string args)
        {
            Answer a = new Answer();
            args = args.Trim();
            if (args.Equals(""))
            {
                a.errorCode = Answer.ERROR.NO_ARG;
                return a;
            }

            if (!Shell.commandList.ContainsKey(args))
            {
                a.errorCode = Answer.ERROR.INCORECT_ARG;
                return a;
            }
            a.errorCode = Answer.ERROR.NO_ERROR;

            Command c;
            Shell.commandList.TryGetValue(args, out c);
            a.message = c.GetDescription();

            return a;
        }
    }
}
