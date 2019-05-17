using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobs.ViewModels
{
    public class DataBaseViewModel
    {
        [Required]
        public string Name { get; set; }

    }
}
