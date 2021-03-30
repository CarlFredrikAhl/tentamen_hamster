using System;
using System.Collections.Generic;
using System.Text;

namespace tentamen_hamster
{
    public class ExerciseSpace
    {
        public int ExerciseSpaceId { get; set; }
        public virtual ICollection<Hamster> Hamsters { get; set; }
    }
}
