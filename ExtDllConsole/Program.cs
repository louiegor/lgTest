using System;
using DllTest;

namespace ExtDllConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                var x = Console.ReadLine();
                Console.WriteLine(UseDllTest(x));
                
            }
        }

        public static string UseDllTest(string x)
        {
            return x.ByLouiegor();
        }

    }

}
