using System;
using System.Drawing;

namespace tentamen_hamster
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("Input amount of days: ");
            int days = int.Parse(Console.ReadLine());

            Console.WriteLine("Input amount of ticks per second: ");
            int ticksPerSec = int.Parse(Console.ReadLine());
        }
    }
}
