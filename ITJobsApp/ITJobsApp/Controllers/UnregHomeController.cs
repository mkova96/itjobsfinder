using ITJobsApp.DLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Controllers
{
    public class UnregHomeController : Controller
    {
        private readonly ITJobsAppContext _databaseContext;

        public UnregHomeController(ITJobsAppContext context)
        {
            _databaseContext = context;
        }

        public ViewResult Index()
        {
            ViewData["Empty"] = "true";
            return View();
        }

    }
}
