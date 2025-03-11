using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_wiedzmin_wanna_be.Game
{
    internal class Logger
    {
        private static Queue<string> logMessages = new Queue<string>(); //stores logs
        private const int logLimit = 5;
        private const int logStartY = 24;

        public static void PrintLog(string msg)
        {
            if (logMessages.Count >= logLimit)
            {
                logMessages.Dequeue();
            }
            logMessages.Enqueue(msg);
            Console.SetCursorPosition(0, logStartY - 1);
            Console.WriteLine("Logs:");
            for (int i = 0; i < logLimit; i++)
            {
                Console.SetCursorPosition(0, logStartY + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            int index = 0;
            foreach (string log in logMessages)
            {
                Console.SetCursorPosition(0, logStartY + index);
                Console.WriteLine(log);
                index++;
            }
        }
    }
}
