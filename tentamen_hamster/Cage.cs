using System;
using System.Collections.Generic;
using System.Text;

namespace tentamen_hamster
{
    public class Cage
    {
        public int CageId { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Hamster> Hamsters { get; set; }
    }
}
