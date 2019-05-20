using ITJobsApp.DLL;
using ITJobsApp.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Controllers
{
    public class CompanyController:Controller
    {
        private readonly ITJobsAppContext _databaseContext;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly UserManager<User> _userManager;


        public CompanyController(ITJobsAppContext context,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,UserManager<User> userManager)
        {
            _databaseContext = context;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _userManager = userManager;
        }

        public async Task<ViewResult> Index()
        {
            ViewData["Success"] = TempData["Success"];
            var user = await _userManager.GetUserAsync(User);
            ViewData["id"] = user.Id.ToString();
            IEnumerable<Company> dbs = _databaseContext.Company.ToList();
            return View(dbs);
        }
        public ViewResult Show(string id)
        {
            Company drug = _databaseContext.Company.Where(i => i.Id.Equals(id.ToString())).FirstOrDefault();
            return View(drug);
        }
    }
}
