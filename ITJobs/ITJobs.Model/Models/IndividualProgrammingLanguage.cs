using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobs.Model.Models
{
    public class IndividualProgrammingLanguage
    {
        public int Id { get; set; }
        public Individual Individual { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }

    }
}
