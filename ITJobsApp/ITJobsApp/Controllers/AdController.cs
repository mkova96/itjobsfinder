using ITJobsApp.DLL;
using ITJobsApp.Model.Models;
using ITJobsApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Controllers
{
    public class AdController : Controller
    {
        private readonly ITJobsAppContext _databaseContext;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly UserManager<User> _userManager;


        public AdController(ITJobsAppContext context,
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, UserManager<User> userManager)
        {
            _databaseContext = context;
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _userManager = userManager;

        }

        public async Task<ViewResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["id"] = user.Id.ToString();
            IEnumerable<Ad> dbs = _databaseContext.Ad.Include(t => t.Company).Include(a => a.AdCategory).
    Include(i => i.AdIndividual).ThenInclude(i => i.Individual).ToList();
            return View(dbs);
        }

        public async Task<ViewResult> MyAds()
        {
            ViewData["Success"] = TempData["Success"];
            var user = (Company)await _userManager.GetUserAsync(User);
            IEnumerable<Ad> dbs = _databaseContext.Ad.Include(t => t.Company).Include(a => a.AdCategory).
                Include(i => i.AdIndividual).ThenInclude(i => i.Individual).Where(t => t.Company.Id == user.Id).ToList();
            return View(dbs);
        }

        public async Task<ViewResult> MyApplys()
        {
            ViewData["Success"] = TempData["Success"];
            var user = (Individual)await _userManager.GetUserAsync(User);
            ViewData["id"] = user.Id.ToString();
            var u = _databaseContext.Individual.Include(t=>t.AdIndividual).Where(t => t.Id.Equals(user.Id)).FirstOrDefault();
         
            ICollection<AdIndividual> adi = _databaseContext.AdIndividual.Include(t=>t.Individual).Include(p=>p.Ad).ThenInclude(o=>o.Company)
                .Include(p => p.Ad).ThenInclude(i=>i.AdCategory).Where(i => i.Individual == u).ToList();
            return View(adi);
        }

        [Authorize(Roles = "Company")]
        [HttpGet]
        public ViewResult Add()
        {
            ViewData["Cats"] = _databaseContext.AdCategory.ToList();

            return View(new AdViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(AdViewModel model)
        {
            ViewData["Cats"] = _databaseContext.AdCategory.ToList();
            if (ModelState.IsValid)
            {
                var val = _databaseContext.AdCategory.FirstOrDefault(m => m.Id == model.AdCategoryId);
                var user = (Company)await _userManager.GetUserAsync(User);


                _databaseContext.Ad.Add(new Ad
                {
                    Title = model.Title,
                    JobSummary = model.JobSummary,
                    RequiredSkills = model.RequiredSkills,
                    Location = model.Location,
                    NumberOfWorkingHours = model.NumberOfWorkingHours,
                    Date = DateTime.Now,
                    AdCategory = val,
                    Company = user
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

        [Authorize(Roles = "Company")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ad = _databaseContext.Ad.FirstOrDefault(t => t.Id == id);

            var inds = _databaseContext.AdIndividual.Where(i => i.Ad == ad);
            foreach (var z in inds)
            {
                _databaseContext.AdIndividual.Remove(z);
            }
            _databaseContext.SaveChanges();

            try
            {
                _databaseContext.Ad.Remove(ad);
                _databaseContext.SaveChanges();
                TempData[Constants.Message] = $"Oglas je uspješno obrisan";
                TempData[Constants.ErrorOccurred] = false;
            }
            catch (Exception exc)
            {
                TempData[Constants.Message] = $"Oglas nije moguće izbrisati.";
                TempData[Constants.ErrorOccurred] = true;

                System.Diagnostics.Debug.WriteLine(exc.Source);
            }
            return RedirectToAction(nameof(MyAds));
        }

        [Authorize(Roles = "Individual")]
        [HttpPost]
        public async Task<IActionResult> ApplyTo(int id)
        {
            var ad = _databaseContext.Ad.FirstOrDefault(t => t.Id == id);
            var user = (Individual)await _userManager.GetUserAsync(User);

            var x = new AdIndividual { Ad = ad, Individual = user };

            _databaseContext.AdIndividual.Add(x);
            _databaseContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Individual")]
        [HttpPost]
        public async Task<IActionResult> CancelApplyTo(int id)
        {
            var ad = _databaseContext.Ad.FirstOrDefault(t => t.Id == id);
            var user = (Individual)await _userManager.GetUserAsync(User);

            var x = _databaseContext.AdIndividual.Where(t => t.Ad == ad).Where(d => d.Individual == user).FirstOrDefault();

            _databaseContext.AdIndividual.Remove(x);
            _databaseContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Individual")]
        [HttpPost]
        public async Task<IActionResult> CancelApplyTo2(int id)
        {
            var ad = _databaseContext.Ad.FirstOrDefault(t => t.Id == id);
            var user = (Individual)await _userManager.GetUserAsync(User);

            var x = _databaseContext.AdIndividual.Where(t => t.Ad == ad).Where(d => d.Individual == user).FirstOrDefault();

            _databaseContext.AdIndividual.Remove(x);
            _databaseContext.SaveChanges();
            return RedirectToAction(nameof(MyApplys));

        }

        public async Task<ViewResult> Show(int id)
        {

            var user = await _userManager.GetUserAsync(User);
            ViewData["id"] = user.Id.ToString();
            Ad drug = _databaseContext.Ad.Include(t => t.Company).Include(a => a.AdCategory).
                Include(i => i.AdIndividual).ThenInclude(i => i.Individual).Where(i => i.Id==id).FirstOrDefault();
            return View(drug);
        }

        public async Task<ViewResult> CompanyAds(string id)
        {

            var user = await _userManager.GetUserAsync(User);
            ViewData["id"] = user.Id.ToString();
            IEnumerable<Ad> ads = _databaseContext.Ad.Include(t => t.Company).Include(a => a.AdCategory).
                Include(i => i.AdIndividual).ThenInclude(i => i.Individual).Where(i => i.Company.Id == id).ToList();
            return View(ads);
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewData["Cats"] = _databaseContext.AdCategory.ToList();

            var ses = _databaseContext.Ad.Include(t => t.AdCategory)
            .FirstOrDefault(p => p.Id == id);

            ViewData["Success"] = TempData["Success"];

            var model = new EditAdViewModel
            {
                JobSummary = ses.JobSummary,
                Id = ses.Id,
                Location = ses.Location,
                NumberOfWorkingHours = ses.NumberOfWorkingHours,
                RequiredSkills=ses.RequiredSkills,
                Title=ses.Title
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(int id, EditAdViewModel model)
        {
            ViewData["Cats"] = _databaseContext.AdCategory.ToList();
            if (id != 0)
            {
                model.Id = id;
            }

            if (ModelState.IsValid)
            {
                var ses = _databaseContext.Ad.Include(p => p.AdCategory).FirstOrDefault(m => m.Id == id);
                ses.AdCategory = _databaseContext.AdCategory.ToList().First(c => c.Id == model.AdCategoryId);

                ses.JobSummary = model.JobSummary;
                ses.Location = model.Location;
                ses.NumberOfWorkingHours = model.NumberOfWorkingHours;
                ses.RequiredSkills = model.RequiredSkills;
                ses.Title = model.Title;

                TempData["Success"] = true;
                _databaseContext.SaveChanges();

                TempData[Constants.Message] = $"Oglas je promijenjen";
                TempData[Constants.ErrorOccurred] = false;
                return RedirectToAction(nameof(Index));

            }
            else
            {
                ViewData["Cats"] = _databaseContext.AdCategory.ToList();
                return View("Edit", model);

            }
        }


    }
}
