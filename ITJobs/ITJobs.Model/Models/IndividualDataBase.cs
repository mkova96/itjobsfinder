using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobs.Model.Models
{
    public class IndividualDataBase
    {
        public int Id { get; set; }
        public Individual Individual { get; set; }
        public DataBase DataBase { get; set; }
    }
}
