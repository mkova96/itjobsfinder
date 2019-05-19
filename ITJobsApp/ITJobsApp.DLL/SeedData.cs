using ITJobsApp.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITJobsApp.DLL
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ITJobsAppContext(
                serviceProvider.GetRequiredService<DbContextOptions<ITJobsAppContext>>()))
            {

                if (context.ProgrammingLanguage.Any())
                {
                    return;
                }
                var sds = new[]{
                    new ProgrammingLanguage { Name = "C" },
                    new ProgrammingLanguage { Name = "C#" },
                    new ProgrammingLanguage { Name = "Java" },
                    new ProgrammingLanguage { Name = "Python" },
                    new ProgrammingLanguage { Name = "JavaScript" },
                    new ProgrammingLanguage { Name = "Ruby" },
                    new ProgrammingLanguage { Name = "C++" }
                };
                context.ProgrammingLanguage.AddRange(sds);
                context.SaveChanges();

                //////////////////////////////////////////////////////////

                if (context.DataBase.Any())
                {
                    return;
                }
                var dbs = new[]{
                new DataBase { Name = "Oracle 12c" },
                new DataBase { Name = "MySQL" },
                new DataBase { Name = "Microsoft SQL Server" },
                new DataBase { Name = "PostgreSQL" },
                new DataBase { Name = "MongoDB" },
                new DataBase { Name = "MariaDB" }
            };
                context.DataBase.AddRange(dbs);
                context.SaveChanges();

                //////////////////////////////////////////////////////////

                if (context.AdCategory.Any())
                {
                    return;
                }
                var adc = new[]{
                new AdCategory { Name = "Stalni" },
                new AdCategory { Name = "Honorarni" },
                new AdCategory { Name = "Freelance" },

            };
                context.AdCategory.AddRange(adc);
                context.SaveChanges();
            }

        }

    }
}
