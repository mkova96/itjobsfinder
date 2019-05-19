using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Model.Models
{
    public class Individual:User
    {
        public string Surname { get; set; }
        public string Skills { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<IndividualProgrammingLanguage> IndividualProgrammingLanguages { get; set; }
        public virtual ICollection<IndividualDataBase> IndividualDataBases { get; set; }
        public virtual ICollection<AdIndividual> AdIndividual { get; set; }


    }
}
