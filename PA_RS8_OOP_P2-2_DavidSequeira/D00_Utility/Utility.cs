using System;
using System.Text;

namespace D00_Utility
{
    public static class Utility
    {

        public static void SetUnicodeConsole()
        {
            Console.OutputEncoding = Encoding.UTF8;
        }

        public static void WriteTitle(string title)
        {
            Console.WriteLine(new string('-', 50));

            Console.WriteLine(title.ToUpper());

            Console.WriteLine(new string('-', 50));
        }

        public static void WriteMessage(string message, string beginTerminator = "", string endTerminator = "")
        {
            Console.Write($"{beginTerminator}{message}{endTerminator}");
        }

        public static void BlockSeparator(string separator)
        {
            Console.Write(separator);
        }

        public static void TerminateConsole()
        {

            Console.Write("\n\nPrima qualquer tecla para sair: ");
            Console.ReadKey();
            Console.Clear();

            // Console.Write("Prima qualquer tecla para confirmar: ");
            // Console.Clear();


        }

        public static bool ValidateNumber0(double number01)
        {
            #region v1: Funcional, mas pouco optimizado
            /*
            if (number01 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            */


            #endregion

            #region v2: Usar o operador ternário

            // return number01 == 00 ? true : false;

            #endregion

            #region v3: Final, mais otimizado
            return number01 == 0;
            #endregion

        }

        public static bool ValidateNumberDouble(string text)
        {
            double number;
            bool successNumber = double.TryParse(text, out number);

            return successNumber && number != 0;
        }



    }
}
