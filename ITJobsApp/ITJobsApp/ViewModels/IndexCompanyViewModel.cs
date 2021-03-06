﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.ViewModels
{
    public class IndexCompanyViewModel
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Morate unijeti podatke o firmi")]
        public string About { get; set; }

        [Required(ErrorMessage = "Morate unijeti broj telefona firme")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }



        [Required(ErrorMessage = "Morate unijeti mail adresu")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "E-mail adresa je u krivom formatu")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Morate unijeti ime")]
        [Display(Name = "Ime")]
        public string Name { get; set; }
        public string StatusMessage { get; set; }

        [Required(ErrorMessage = "Morate unijeti adresu")]
        [Display(Name = "Adresa")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Morate unijeti grad")]
        [Display(Name = "Grad")]
        public string City { get; set; }


        [Required(ErrorMessage = "Datum osnivanja tvrtke je obavezan")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Established { get; set; }

        [Required(ErrorMessage = "Morate unijeti vrstu poslovanja")]
        [Display(Name = "Vrsta poslovanja")]
        public string BussinesType { get; set; }

        [Required(ErrorMessage = "Poveznica slike firme je obavezna")]
        [DataType(DataType.ImageUrl)]
        public string LogoLink { get; set; }

        [Required(ErrorMessage = "Web adresa firme je obvezna")]
        [DataType(DataType.Url)]
        public string WebAddress { get; set; }

        [Range(1, 100000, ErrorMessage = "Broj radnika mora biti veći od 0")]
        public int NumberOfEmployees { get; set; }
    }
}
