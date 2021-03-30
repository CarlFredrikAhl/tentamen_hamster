using System;
using System.Collections.Generic;
using System.Text;

namespace tentamen_hamster
{
    public class ActivityLog
    {
        public int ActivityLogId { get; set; }
        public virtual Hamster Hamster { get; set; }
        public virtual Activity Activity { get; set; }
        public DateTime DateTime { get; set; }
    }
}
