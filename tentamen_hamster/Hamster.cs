using System;
using System.Collections.Generic;
using System.Text;

namespace tentamen_hamster
{
    public class Hamster
    {
        public virtual Cage Cage { get; set; }
        public int HamsterId { get; set; }
        
        //Write code in set to start events when activity changes
        public Activity Activity { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string OwnerName { get; set; }
        public DateTime? TimeCheckedIn { get; set; } 
        public DateTime? TimeLastExercise { get; set; }
    }
}
