using System;
using System.Collections.Generic;
using System.Text;

namespace IpiPedia.Tools
{
    abstract class Command
    {
        public static string name = "Command class";
        protected string description = "No Description";
        public static char[] cmdSeparators = { ',', '\n' };
        public abstract Answer Proc(string args);

        public string GetDescription()
        {
            return description;
        }
    }
}
