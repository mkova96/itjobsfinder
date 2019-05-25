using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.ViewModels
{
    public class AdViewModel
    {
        [Required(ErrorMessage = "Morate unijeti naslov oglasa")]
        public string Title { get; set; }

        public int AdCategoryId { get; set; }

        [Required(ErrorMessage = "Morate unijeti opis posla")]
        public string JobSummary { get; set; }

        [Required(ErrorMessage = "Morate unijeti zahtjevane vještine")]
        public string RequiredSkills { get; set; }

        [Required(ErrorMessage = "Morate unijeti lokaciju")]
        public string Location { get; set; }

        [Range(1, 168, ErrorMessage = "Broj radnih sati tjedno mora biti između 1-168")]
        public int NumberOfWorkingHours { get; set; }

    }
}
