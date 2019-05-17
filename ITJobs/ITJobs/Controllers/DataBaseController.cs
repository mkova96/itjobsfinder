﻿using ITJobs.DLL;
using ITJobs.Model.Models;
using ITJobs.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITJobs.Controllers
{
    public class DataBaseController:Controller
    {
        private readonly ITJobsContext _databaseContext;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public DataBaseController(ITJobsContext context,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _databaseContext = context;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public ViewResult Index()
        {
            ViewData["Success"] = TempData["Success"];
            IEnumerable<DataBase> dbs = _databaseContext.DataBase.ToList();
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
                _databaseContext.DataBase.Add(new DataBase
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
            var db = _databaseContext.DataBase.First(g => g.Id == id);
            ViewData["Success"] = TempData["Success"];

            var model = new EditDataBaseViewModel
            {
                DataBase = db
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, EditDataBaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = _databaseContext.DataBase.First(g => g.Id == id);
                company.Name = model.DataBase.Name;
                
                TempData["Success"] = true;

                _databaseContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ses = _databaseContext.DataBase
            .FirstOrDefault(p => p.Id == id);

            try
            {
                _databaseContext.DataBase.Remove(ses);
                _databaseContext.SaveChanges();
                //TempData[Constants.Message] = $"Mjerna jedinica je obrisana";
                //TempData[Constants.ErrorOccurred] = false;
            }
            catch (Exception exc)
            {
                //TempData[Constants.Message] = $"Mjernu jedinicu nije moguće obrisati jer postoje pakiranja koji ju sadrže.";
                //TempData[Constants.ErrorOccurred] = true;
            }
            var x = _databaseContext.ProgrammingLanguage.ToList().Count;

            return RedirectToAction(nameof(Index));

        }




    }
}
