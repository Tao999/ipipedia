using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace IpiPedia.Tools
{
    class Answer
    {
        public enum ERROR
        {
            INIT_ERROR,
            NO_ERROR,
            NO_ARG,
            INCORECT_ARG,
        }

        public ERROR errorCode;
        public string message;

        public Answer()
        {
            errorCode = ERROR.INIT_ERROR;
            message = "";
        }

        public Answer(ERROR errorCode, string message)
        {
            this.errorCode = errorCode;
            this.message = message;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
