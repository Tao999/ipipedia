using IpiPedia.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpiPedia.Commands
{
    class Help : Command
    {
        new public static string name = "help";
        public Help()
        {
            description = "help : affiche toutes les commandes disponible";
        }
        public override Answer Proc(string args)
        {
            Answer a = new Answer();
            a.errorCode = Answer.ERROR.NO_ERROR;
            foreach(Command item in Shell.commandList.Values){
                a.message += item.GetDescription() + '\n';
            }
            if (a.message.Length > 0)
                a.message = a.message.Remove(a.message.Length - 1);
            return a;
        }
    }
}
