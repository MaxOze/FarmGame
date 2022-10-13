using System;
using System.IO;

namespace Generics
{
    public class LoggerMethods
    {
        static int pos = 2;

        public static void LogInFile(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("/home/maks/Рабочий стол/Учеба/5 семестр (.NET)/log.txt", true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }

        public static void LogInConsole(string message)
        {
            if (pos > 55)
            {
                pos = 2;
                Console.SetCursorPosition(3, pos);
                Console.Write("                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n" +
                              "//                                                                    \n");
            }

            Console.SetCursorPosition(3,pos);
            Console.WriteLine(message);
            pos++;
        }
    }
}