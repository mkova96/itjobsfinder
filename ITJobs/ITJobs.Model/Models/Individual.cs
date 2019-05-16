using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobs.Model.Models
{
    public class Individual
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Biography { get; set; }
        public string Skills { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<IndividualProgrammingLanguage> IndividualProgrammingLanguages { get; set; }
        public virtual ICollection<IndividualDataBase> IndividualDataBases { get; set; }


    }
}
