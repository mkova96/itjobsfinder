using ITJobsApp.DLL;
using ITJobsApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ITJobsAppContext _databaseContext;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;


        public SearchController(ITJobsAppContext context,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _databaseContext = context;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public ViewResult SearchAction(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return View();
            }

            var searchLower = searchString.ToLower().Split(" ");
            var drugs =_databaseContext.Company.Where(c => searchForArray(c.Name.ToLower(), searchLower)).ToList();
            var ads= _databaseContext.Ad.Include(p=>p.AdCategory).Where(c => searchForArray(c.Title.ToLower(), searchLower)).ToList();


            var searchResults = new SearchActionViewModel 
            {
                Company = drugs,
                Ad=ads
            };
            return View(searchResults);
        }

        private bool searchForArray(string value, string[] searchStrings)
        {
            foreach (var s in searchStrings)
            {
                if (value.Contains(s)) return true;
            }
            return false;
        }


    }
}
