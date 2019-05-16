using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsFinder.Model.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BussinesType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AboutCompany { get; set; }
        public string LogoLink { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WebAddress { get; set; }
        public int NumberOfEmployees { get; set; }
        public int Established { get; set; }

    }
}
