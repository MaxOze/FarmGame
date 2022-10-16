using System;
using System.IO;

namespace Generics
{
    public class LoggerMethods
    {
        static int _pos = 2;

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
            if (_pos > 55)
            {
                _pos = 2;
                Console.SetCursorPosition(3, _pos);
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

            Console.SetCursorPosition(3,_pos);
            Console.WriteLine(message);
            _pos++;
        }
    }
}