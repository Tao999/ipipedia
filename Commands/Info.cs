using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IpiPedia.Commands
{
    class Info : Command
    {
        new public static string name = "info";
        
        public Info()
        {
            description = "info <name> : affiche la description de <name>";
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

            a.errorCode = Answer.ERROR.NO_ERROR;
            a.message = "";

            var request = from personnage in Shell.list
                          where personnage.name.Equals(args, StringComparison.InvariantCultureIgnoreCase)
                          select personnage.description;
            if (!(request.Count() == 0))
            {
                a.message = request.First();
            }
            return a;
        }
    }
}
