using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpiPedia.Commands
{
    class KeySearch : Command
    {
        new public static string name = "keysh";

        public KeySearch()
        {
            description = "keysh <arg1>, <arg2>, ... : les personnages en relation avec les deux arguments saisis";
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

            List<String> answers = new List<String>();

            var tok = args.Split(cmdSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in tok)
            {
                string shKey = item.Trim();
                var request = from personnage in Shell.list
                              where(
                                personnage.description.Contains(shKey, StringComparison.InvariantCultureIgnoreCase) ||
                                personnage.name.Contains(shKey, StringComparison.InvariantCultureIgnoreCase)
                                )
                              orderby personnage.name ascending
                              select personnage.name;

                answers = answers.Concat(request).ToList();
            }

            answers = answers.Distinct().ToList();
            answers.Sort();

            a.errorCode = Answer.ERROR.NO_ERROR;

            for (int i = 0; i < answers.Count(); i++)
            {
                a.message += answers.ElementAt(i);
                if (i != answers.Count() - 1)
                    a.message += '\n';
            }

            return a;
        }
    }
}
