using System;
using System.Collections.Generic;
using System.Text;

namespace tentamen_hamster
{
    public class Hamster
    {
        public int HamsterId { get; set; }
        public Activity Activity { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string OwnerName { get; set; }
        public DateTime TimeCheckedIn { get; set; } 
        public DateTime TimeLastExercise { get; set; }
    }
}
