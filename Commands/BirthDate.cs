using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpiPedia.Commands
{
    class BirthDate : Command
    {
        new public static string name = "birth";
        
        public BirthDate()
        {
            description = "birth <date> : Affiche toutes les personnes nées avant <date>";
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

            string dateSaisi = args.Trim();
            DateTime dateSearch = DateTime.MinValue;
            DateTime.TryParse(dateSaisi, out dateSearch);
            if (dateSearch.Equals(DateTime.MinValue))
            {

                a.errorCode = Answer.ERROR.INCORECT_ARG;
                a.message = "La saisie est incorrecte";
            }
            else
            {
                var query = from personnage in Shell.list
                              where personnage.birthDate < dateSearch
                              orderby personnage.name ascending
                              select personnage.name;

                a.errorCode = Answer.ERROR.NO_ERROR;
                if (query.Count() == 0)
                {
                    a.message = "";
                }
                else
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        a.message += query.ElementAt(i);
                        if (i != query.Count() - 1)
                            a.message += '\n';

                    }
                }
            }
            

            return a;
        }
    }
}
