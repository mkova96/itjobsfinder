using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.ViewModels
{
    public class AdViewModel
    {
        public string Title { get; set; }
        public int AdCategoryId { get; set; }
        public string JobSummary { get; set; }
        public string RequiredSkills { get; set; }
        public string Location { get; set; }
        public int NumberOfWorkingHours { get; set; }

    }
}
