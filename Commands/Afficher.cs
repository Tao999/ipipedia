using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IpiPedia.Commands
{
    class Afficher : Command
    {
        new public static string name = "afficher";

        public Afficher()
        {
            description = "afficher : Affiche tous les noms de la bdd";
        }

        public override Answer Proc(string args)
        {
            Answer answer = new Answer(Answer.ERROR.NO_ERROR, args);

            var query = from personnage in Shell.list
                        orderby personnage.name ascending
                        select personnage.name;

            for(int i = 0; i < query.Count(); i++)
            {
                answer.message += query.ElementAt(i);
                if (i != query.Count() - 1)
                    answer.message += '\n';

            }

            return answer;
        }
    
    }
}
