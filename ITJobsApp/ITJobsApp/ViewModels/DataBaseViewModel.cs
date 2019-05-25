using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.ViewModels
{
    public class DataBaseViewModel
    {
        [Required(ErrorMessage = "Naziv baze podataka je obavezan")]
        public string Name { get; set; }

    }
}
