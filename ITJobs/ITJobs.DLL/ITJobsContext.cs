using ITJobs.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITJobs.DLL
{
    public class ITJobsContext : DbContext
        {
            public ITJobsContext(DbContextOptions<ITJobsContext> options)
                : base(options)
            { }

            public DbSet<ProgrammingLanguage> ProgrammingLanguage { get; set; }
            public DbSet<Ad> Ad { get; set; }
            public DbSet<AdCategory> AdCategory { get; set; }
            public DbSet<Company> Company { get; set; }
            public DbSet<DataBase> DataBase { get; set; }
            public DbSet<Individual> Individual { get; set; }
            public DbSet<IndividualDataBase> IndividualDataBase { get; set; }
            public DbSet<IndividualProgrammingLanguage> InduvidualProgrammingLanguage { get; set; }
        }
}
