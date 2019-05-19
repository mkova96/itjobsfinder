using ITJobsApp.Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITJobsApp.DLL
{
    public class ITJobsAppContext : IdentityDbContext<User>
    {
        public ITJobsAppContext(DbContextOptions<ITJobsAppContext> options)
            : base(options)
        { } 

        public DbSet<ProgrammingLanguage> ProgrammingLanguage { get; set; }
        public DbSet<Ad> Ad { get; set; }
        public DbSet<AdCategory> AdCategory { get; set; }
        public DbSet<AdIndividual> AdIndividual { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<DataBase> DataBase { get; set; }
        public DbSet<Individual> Individual { get; set; }
        public DbSet<IndividualDataBase> IndividualDataBase { get; set; }
        public DbSet<IndividualProgrammingLanguage> InduvidualProgrammingLanguage { get; set; }
    }
}
