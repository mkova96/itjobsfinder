using System;
using System.Collections.Generic;
using System.Text;

namespace ITJobsApp.Model.Models
{
    public class AdIndividual
    {
        public int Id { get; set; }
        public Individual Individual { get; set; }
        public Ad Ad { get; set; }
    }
}
