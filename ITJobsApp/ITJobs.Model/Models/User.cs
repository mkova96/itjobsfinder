using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ITJobsApp.Model.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

    }
}
