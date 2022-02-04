﻿using IpiPedia.Tools;
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
            description = "echo <text> : affiche <text>";
        }

        public override Answer Proc(string args)
        {
            args = args.Trim();
            Answer answer = new Answer(Answer.ERROR.NO_ERROR, args);
            if (args.Equals(""))
                answer.errorCode = Answer.ERROR.NO_ARG;

            return answer;
        }
    }
}
