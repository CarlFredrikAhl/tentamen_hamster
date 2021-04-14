using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace tentamen_hamster
{
    public class Hamster
    {
        public virtual Cage Cage { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int HamsterId { get; set; }

        //Write code in set to start events when activity changes
        public Activity Activity { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Column("Gender")]
        public string _Gender 
        {
            get
            {
                if (Gender == Gender.Male)
                {
                    return "Male";

                }
                else
                {
                    return "Female";
                }
            }

            set
            {
                if (Gender == Gender.Male)
                {
                    _Gender = "Male";

                }
                else
                {
                    _Gender = "Female";
                }
            }
        }

        [NotMapped]
        public Gender Gender { get; set; }
        public string OwnerName { get; set; }
        public DateTime? TimeCheckedIn { get; set; }
        public DateTime? TimeLastExercise { get; set; }
    }
}
