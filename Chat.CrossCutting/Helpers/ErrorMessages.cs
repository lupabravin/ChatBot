using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.CrossCutting.Helpers
{
    public static class ErrorMessages
    {
        public const string COMMAND_MISSING_PARAMETER = "'[command]' needs a parameter. ([command]=parameter)";
        public const string UNKNOWN_COMMAND = "Unknown Command: ";

    }
}
