using System;

namespace Chat.WebApp.Helpers
{
    public static class ExtensionMethods
    {
        public static string FormatDate(this DateTime date)
        {
            return date.Hour.ToString().PadLeft(2, '0') + ":" +
                           date.Minute.ToString().PadLeft(2, '0') + " - " +
                           date.Month.ToString().PadLeft(2, '0') + "/" +
                           date.Day.ToString().PadLeft(2, '0') + "/" +
                           date.Year;
        }
    }
}
