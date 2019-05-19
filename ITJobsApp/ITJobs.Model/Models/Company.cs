using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Model.Models
{
    public class Company:User
    {
        public string BussinesType { get; set; }
        public string LogoLink { get; set; }
        public string WebAddress { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime Established { get; set; }

    }
}
