using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsFinder.Model.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AdCategory AdCategory { get; set; }
        public Company Company { get; set; }
        public string JobSummary { get; set; }
        public string RequiredSkills { get; set; }
        public string Location { get; set; }
        public int NumberOfWorkingHours { get; set; }
        public DateTime Date { get; set; }

    }
}
