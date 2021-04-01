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
        
        static Stack<Hamster> maleHamsters;
        static Stack<Hamster> femaleHamsters;
        
        static ExerciseSpace exerciseSpace;
        
        static Cage[] maleCages;
        static Cage[] femaleCages;

        static Timer[] timers = new Timer[1];

        static int ticksPerSec;
        static int tickCounter = 0;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            exerciseSpace = new ExerciseSpace();

            maleCages = new Cage[5];
            femaleCages = new Cage[5];

            maleHamsters = new Stack<Hamster>();
            femaleHamsters = new Stack<Hamster>();

            ImportHamsters();
            CheckingIn();

            Menu();

            timers[0] = new Timer(new TimerCallback(TimerTest), null, 0, 1000 / ticksPerSec);

            //Possability to stop timer
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == '1')
                {
                    for (int i = 0; i < timers.Length; i++)
                    {
                        if (timers[i] == null)
                        {
                            Console.WriteLine(
                                Environment.NewLine +
                                "Timer Not " +
                                "Yet Started" +
                                Environment.NewLine);
                            continue;
                        }
                        else
                        {
                            timers[i].Change(Timeout.Infinite, Timeout.Infinite);
                        }
                    }
                }
            }
        }

        private static void TimerTest(object state)
        {
            Console.WriteLine("test");

            if (tickCounter >= 60)
            {
                timers[0].Change(Timeout.Infinite, Timeout.Infinite);
            }
                

            tickCounter++;
        }

        static void Menu()
        {
            Console.WriteLine("Input amount of days to run simulation: ");
            int days = int.Parse(Console.ReadLine());

            Console.WriteLine("Input amount of ticks per second: ");
            ticksPerSec = int.Parse(Console.ReadLine());
        }

        static void ImportHamsters()
        {
            hamsters = ProcessFile("Hamsterlista30.csv");

            foreach (Hamster hamster in hamsters.Where(hamster => hamster.Gender == Gender.Male))
            {
                maleHamsters.Push(hamster);
            }

            foreach (Hamster hamster in hamsters.Where(hamster => hamster.Gender == Gender.Female))
            {
                femaleHamsters.Push(hamster);
            }
        }

        static void CheckingIn()
        {
            //Males
            //Loop 5 times
            for (int i = 0; i < maleCages.Length; i++)
            {
                maleCages[i] = new Cage();
                //maleCages[i].Hamsters = new List<Hamster>();

                //Loop 3 times
                for (int y = 0; y < 3; y++)
                {
                    if(maleHamsters.Count != 0)
                        maleCages[i].Hamsters.Add(maleHamsters.Pop());
                }
            }

            //Females
            //Loop 5 times
            for (int i = 0; i < femaleCages.Length; i++)
            {
                femaleCages[i] = new Cage();
                //femaleCages[i].Hamsters = new List<Hamster>();

                //Loop 3 times
                for (int y = 0; y < 3; y++)
                {
                    if (femaleHamsters.Count != 0)
                        femaleCages[i].Hamsters.Add(femaleHamsters.Pop());
                }
            }
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
            if (columns[2] == "K")
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

            }
            else
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
