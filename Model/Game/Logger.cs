using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_wiedzmin_wanna_be.View;

namespace RPG_wiedzmin_wanna_be.Model.Game
{
    internal class Logger
    {
        private static Queue<string> logMessages = new Queue<string>(); //stores logs
        private const int logLimit = 5;


        public static void PrintLog(string msg)
        {
            if (logMessages.Count >= logLimit)
            {
                logMessages.Dequeue();
            }
            logMessages.Enqueue(msg);

            Render.Instance.PrintLogs();
        }
        /*        public static Queue<string> GetLogs()
                {
                    return logMessages;
                }*/

        public static IReadOnlyCollection<string> GetLogs()
        {
            return logMessages.ToList().AsReadOnly();
        }
    }
}
