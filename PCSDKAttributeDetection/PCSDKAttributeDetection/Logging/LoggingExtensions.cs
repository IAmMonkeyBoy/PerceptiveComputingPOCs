using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSDKAttributeDetection.Logging
{
    public static class LoggingExtensions
    {
        public static void Log(this object o, String message)
        {
            Console.WriteLine(message);

        }

        public static void Log(this object o, Exception exception)
        {
            Console.WriteLine(exception.Message);
        }

    }
}
