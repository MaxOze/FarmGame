using System;
using System.IO;

namespace Generics
{
    public class LoggerMethods
    {
        public static int Pos = 2;

        public static void LogInFile(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Config.LOG_PATH, true))
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
            if (Pos > 51)
            {
                Pos = 2;
                Console.SetCursorPosition(3, Pos);
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
                              "//                                                                    \n");
            }

            Console.SetCursorPosition(3,Pos);
            Console.WriteLine(message);
            Pos++;
        }
    }
}