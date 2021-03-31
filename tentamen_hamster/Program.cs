using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace tentamen_hamster
{
    class Program
    {
        static List<Hamster> hamsters;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            ImportHamsters();

            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("Input amount of days to run simulation: ");
            int days = int.Parse(Console.ReadLine());

            Console.WriteLine("Input amount of ticks per second: ");
            int ticksPerSec = int.Parse(Console.ReadLine());
        }

        static void ImportHamsters()
        {
            hamsters = ProcessFile("Hamsterlista30.csv");
        }

        private static List<Hamster> ProcessFile(string path)
        {
            return File.ReadAllLines(path).Where(line => line.Length > 1).Select(ParseFromCsv).ToList();
        }

        //Rename this later to CsvParser for example
        private static Hamster ParseFromCsv(string line)
        {
            var columns = line.Split(';');

            //Female
            if(columns[2] == "K")
            {
                return new Hamster
                {
                    Name = columns[0],
                    Age = int.Parse(columns[1]),
                    Gender = Gender.Female,
                    OwnerName = columns[3],
                    Activity = Activity.Arrival,
                    TimeCheckedIn = DateTime.Now,
                    TimeLastExercise = null
                };
            
            } else
            {
                return new Hamster
                {
                    Name = columns[0],
                    Age = int.Parse(columns[1]),
                    Gender = Gender.Male,
                    OwnerName = columns[3],
                    Activity = Activity.Arrival,
                    TimeCheckedIn = DateTime.Now,
                    TimeLastExercise = null
                };
            }
        }
    }
}
