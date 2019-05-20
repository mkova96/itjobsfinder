using ITJobsApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.ViewModels
{
    public class SearchActionViewModel
    {
        public IEnumerable<Ad> Ad { get; set; }
        public IEnumerable<Company> Company { get; set; }
    }
}
