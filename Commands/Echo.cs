using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpiPedia.Commands
{
    class Echo : Command
    {
        new public static string name = "echo";

        public Echo()
        {
            description = "echo <text> : affiche le texte saisi";
        }

        public override Answer Proc(string args)
        {
            args = args.Trim();
            Answer answer = new Answer(Answer.ERROR.NO_ERROR, args);
            if (args.Equals(""))
                answer.message = " ";

            return answer;
        }
    }
}
