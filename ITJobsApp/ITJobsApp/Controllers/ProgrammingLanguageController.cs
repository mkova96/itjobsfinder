using ITJobsApp.DLL;
using ITJobsApp.Model.Models;
using ITJobsApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Controllers
{
    public class ProgrammingLanguageController : Controller
    {
        private readonly ITJobsAppContext _databaseContext;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public ProgrammingLanguageController(ITJobsAppContext context,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _databaseContext = context;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public ViewResult Index()
        {
            ViewData["Success"] = TempData["Success"];
            IEnumerable<ProgrammingLanguage> dbs = _databaseContext.ProgrammingLanguage.ToList();
            return View(dbs);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View(new DataBaseViewModel());
        }


        [HttpPost]
        public IActionResult Create(DataBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                _databaseContext.ProgrammingLanguage.Add(new ProgrammingLanguage
                {
                    Name = model.Name

                });

                TempData["Success"] = true;
                _databaseContext.SaveChanges();
            }
            else
            {
                return View("Add", model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var db = _databaseContext.ProgrammingLanguage.First(g => g.Id == id);
            ViewData["Success"] = TempData["Success"];

            var model = new EditPLViewModel
            {
                PL = db
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, EditPLViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = _databaseContext.ProgrammingLanguage.First(g => g.Id == id);
                company.Name = model.PL.Name;

                TempData["Success"] = true;

                _databaseContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ses = _databaseContext.ProgrammingLanguage
            .FirstOrDefault(p => p.Id == id);

            try
            {
                _databaseContext.ProgrammingLanguage.Remove(ses);
                _databaseContext.SaveChanges();
                TempData[Constants.Message] = $"Programski jezik je obrisan";
                TempData[Constants.ErrorOccurred] = false;
            }
            catch (Exception exc)
            {
                TempData[Constants.Message] = $"Programski jezik nije moguće obrisati jer postoje korisnici koji ga sadrže.";
                TempData[Constants.ErrorOccurred] = true;
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
