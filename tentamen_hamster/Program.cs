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

        static List<Cage> emptyCages;

        static Timer[] timers = new Timer[3];

        static int ticksPerSec;
        static int daysToRun;

        static int tickCounter;
        static int TickCounter
        {
            get { return tickCounter; }
            set
            {
                //Write to log-file
                WriteToLog();
                tickCounter = value;
            }
        }

        static AppContext hamsterContext;

        //Current time and day
        public static DateTime curTime;
        public static int curDay;

        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Magenta;

            hamsterContext = new AppContext();

            //The day starts at 7 at the morning
            curTime = DateTime.Parse("07:00");
            curDay = 1;

            tickCounter = 1;

            using (hamsterContext)
            {
                exerciseSpace = new ExerciseSpace();
                exerciseSpace.Hamsters = new Queue<Hamster>();
                maleCages = new Cage[5];
                femaleCages = new Cage[5];

                maleHamsters = new Stack<Hamster>();
                femaleHamsters = new Stack<Hamster>();

                ImportHamsters();
                CheckingIn();

                Menu();

                //emptyCages = new List<Cage>();
                TakeToExercise("Female");

                //TickerHandlerThread
                //Task tickerTask = Task.Run(TickerHandlerAsync);

                timers[0] = new Timer(new TimerCallback(Exercise), null, 0, (1000 / ticksPerSec));
                timers[2] = new Timer(new TimerCallback(TickerHandler), null, 0, 1000 / ticksPerSec);

                //Possability to stop timers
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.KeyChar == 's')
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
        }

        private static void TickerHandler(object state)
        {
            bool changedDay = false;

            //New day
            if (TickCounter > 100)
            {
                curDay++;
                TickCounter = 1;
                curTime = DateTime.Parse("07:00");
                curTime = curTime.AddMinutes(6);
                changedDay = true;
            }

            if (curDay >= daysToRun + 1)
            {
                //Stop this timer
                timers[2].Change(Timeout.Infinite, 0);

                //End of program
                Console.WriteLine("End of program");
            }

            if (!changedDay)
            {
                TickCounter++;
                curTime = curTime.AddMinutes(6);
            }
        }

        //Remake so that hamsters don't get removed and added, just id change
        private static void Exercise(object state)
        {
            //Console.WriteLine("tick");

            //Console.WriteLine("Tick: " + TickCounter);

            if (TickCounter >= 10)
            {
                Console.WriteLine("Time's up, change hamsters");
                Console.WriteLine("Current hamsters");

                for (int i = 0; i < exerciseSpace.Hamsters.Count; i++)
                {
                    Console.WriteLine(exerciseSpace.Hamsters.ElementAt(i).Name);
                }

                //Console.WriteLine("Cages to put back these hamsters to: ");
                //foreach (var cage in emptyCages.Distinct())
                //{
                //    Console.WriteLine(cage.CageId);
                //}

                for (int i = 0; i < exerciseSpace.Hamsters.Count; i++)
                {
                    hamsterContext.Hamsters.Add(exerciseSpace.Hamsters.Dequeue());
                }

                hamsterContext.SaveChanges();
                ;

                Console.WriteLine("Hamsters added back, current hamsters: ");

                foreach (var item in hamsterContext.Hamsters.Select(x => x.Name))
                {
                    Console.WriteLine(item);
                }

                timers[0].Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        static void Menu()
        {
            Console.WriteLine("Input amount of days to run simulation: ");
            daysToRun = int.Parse(Console.ReadLine());

            Console.WriteLine("Input amount of ticks per second: ");
            ticksPerSec = int.Parse(Console.ReadLine());
        }

        //Remake so that hamsters don't get removed and added, just id change
        static void TakeToExercise(string gender)
        {
            //This is to know which cages to put back the hamster when they 
            //get dequeued from exerciseSpace
            //emptyCages = new List<Cage>();

            var exerciseHamsters = hamsterContext.Hamsters.OrderBy(hamster => hamster.TimeLastExercise)
                .Where(hamster => hamster.Gender == gender).Select(hamster => hamster).Take(6);
            List<Hamster> hamsterLista = exerciseHamsters.ToList();

            //Temporary code to see which hamsters are in the cages
            Console.WriteLine("Before removing 6 hamsters: ");
            var query1 = hamsterContext.Hamsters.Select(x => x.Name);

            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }

            //Looping through the exercise hamsters and take them to exercise space
            foreach (Hamster hamster in hamsterLista)
            {
                //Cage hamsterCage = hamsterContext.Cages.Single(x => x.Hamsters.Contains(hamster));
                //emptyCages.Add(hamsterCage);

                //Add to exercise space
                exerciseSpace.Hamsters.Enqueue(hamster);
                //hamsterContext.ExerciseSpace.Hamsters
            }

            //Remove the exercise hamsters from the cages the cages (emptyCages)

            // var cageHamsters = hamsterContext.Hamsters.Select(x => x.Name);
            foreach (var hamster in exerciseHamsters)
            {
                hamsterContext.Hamsters.Remove(hamster);
            }

            hamsterContext.SaveChanges();
            //;

            Console.WriteLine(" \n After: ");

            //This is to see all the hamsters
            foreach (var item in query1)
            {
                Console.WriteLine(item);
            }

            //Console.WriteLine("Hamster context count: " + hamsterContext.Hamsters.Count());
            //Console.WriteLine("Cages context count: " + hamsterContext.Cages.Count());

            //hamsterContext.SaveChanges();
        }

        static void ImportHamsters()
        {
            hamsters = ProcessFile("Hamsterlista30.csv");

            foreach (Hamster hamster in hamsters.Where(hamster => hamster.Gender == "Male"))
            {
                maleHamsters.Push(hamster);
            }

            foreach (Hamster hamster in hamsters.Where(hamster => hamster.Gender == "Female"))
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
                    if (maleHamsters.Count != 0)
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

            //Add to database

            if (hamsterContext.Hamsters.Count() != 0)
            {
                //Remove the hamsters if there are any already
                try
                {
                    var hamsters = from hamster in hamsterContext.Hamsters select hamster;

                    foreach (var hamster in hamsters)
                    {
                        hamsterContext.Hamsters.Remove(hamster);
                        hamsterContext.SaveChanges();
                    }

                    ;
                
                } catch(Exception e)
                {

                }
            }

            foreach (var hamster in hamsters)
            {
                if (hamsterContext.Hamsters.Count() < 30)
                {
                    hamsterContext.Hamsters.Add(hamster);
                    hamsterContext.SaveChanges();
                }
            }

            if (hamsterContext.Cages.Count() < 10)
            {
                foreach (Cage cage in maleCages)
                {
                    if (hamsterContext.Cages.Count() < 10)
                    {
                        hamsterContext.Cages.Add(cage);
                        hamsterContext.SaveChanges();
                    }
                }

                foreach (Cage cage in femaleCages)
                {
                    if (hamsterContext.Cages.Count() < 10)
                    {
                        hamsterContext.Cages.Add(cage);
                        hamsterContext.SaveChanges();
                    }
                }
            }
        }

        static void WriteToLog()
        {
            string logTime = curTime.ToString("HH:mm");
            string logData = $"Tick {TickCounter} : Day {curDay} : Time {logTime}\n";

            if (TickCounter != 101 && curDay <= daysToRun)
            {
                FileWriter.WriteData("hamster_log", logData);
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
                    Gender = "Female",
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
                    Gender = "Male",
                    OwnerName = columns[3],
                    Activity = Activity.Arrival,
                    TimeCheckedIn = DateTime.Now,
                    TimeLastExercise = null
                };
            }
        }
    }
}
