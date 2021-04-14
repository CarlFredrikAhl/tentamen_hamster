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
        public string _Gender { get; set; }

        [NotMapped]
        public Gender Gender
        {
            get
            {
                if (_Gender == "Male")
                {
                    return Gender.Male;

                }
                else
                {
                    return Gender.Female;
                }
            }

            set
            {
                if (_Gender == "Male")
                {
                    Gender = Gender.Male;

                }
                else
                {
                    Gender = Gender.Female;
                }
            }
        }
        public string OwnerName { get; set; }
        public DateTime? TimeCheckedIn { get; set; }
        public DateTime? TimeLastExercise { get; set; }
    }
}
