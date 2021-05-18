using System.Collections.Generic;

namespace tentamen_hamster
{
    public class Cage
    {
        public int CageId { get; set; }
        public int Number { get; set; }
        public virtual ICollection<Hamster> Hamsters { get; set; }

        public Cage()
        {
            Hamsters = new HashSet<Hamster>();
        }
    }
}
