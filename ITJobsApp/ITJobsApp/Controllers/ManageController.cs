using ITJobsApp.DLL;
using ITJobsApp.Model.Models;
using ITJobsApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;


namespace ITJobsApp.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly ITJobsAppContext _databaseContext;
        private readonly UrlEncoder _urlEncoder;
        private readonly ITJobsAppContext _context;



        public ManageController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder,
          ITJobsAppContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _databaseContext = context;
            _urlEncoder = urlEncoder;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> IndexIndividual()
        {
            ViewData["DBs"] = _databaseContext.DataBase.OrderBy(p => p.Name).ToList();
            ViewData["PLs"] = _databaseContext.ProgrammingLanguage.OrderBy(p => p.Name).ToList();

            var user = (Individual)await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }
            var model = new IndexIndividualViewModel
            {
                Username = user.UserName,
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                StatusMessage = StatusMessage,
                About = user.About,

                City = user.City,
                DateOfBirth = user.DateOfBirth,
                Skills = user.Skills,
                Surname = user.Surname,
                PhoneNumber=user.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexIndividual(IndexIndividualViewModel model)
        {
            ViewData["DBs"] = _databaseContext.DataBase.OrderBy(p => p.Name).ToList();
            ViewData["PLs"] = _databaseContext.ProgrammingLanguage.OrderBy(p => p.Name).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = (Individual)await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var x = _databaseContext.Individual.FirstOrDefault(g => g.Email == model.Email);
                var y = _databaseContext.Company.FirstOrDefault(g => g.Email == model.Email);

                if (x != null || y != null)
                {
                    TempData[Constants.Message] = $"Korisnik s tim mailom već postoji.\n";
                    TempData[Constants.ErrorOccurred] = true;
                    return RedirectToAction(nameof(IndexIndividual));
                }

                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Neočekivana greška se javila prilikom postavljanja e-mail adrese korisnika s ID-em '{user.Id}'.");
                }
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Neočekivana greška se javila prilikom postavljanja e-mail adrese korisnika s ID-em '{user.Id}'.");
                }
            }


            var firstName = user.Name;
            if (model.Name != firstName)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.Name = model.Name;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var lastName = user.Surname;
            if (model.Surname != lastName)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.Surname = model.Surname;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var address = user.Address;
            if (model.Address != address)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.Address = model.Address;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var about = user.About;
            if (model.About != about)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.About = model.About;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var city = user.City;
            if (model.City != city)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.City = model.City;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var date = user.DateOfBirth;
            if (model.DateOfBirth != date)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.DateOfBirth = model.DateOfBirth;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var skills = user.Skills;
            if (model.Skills != skills)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.Skills = model.Skills;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }
            var phone = user.PhoneNumber;
            if (model.PhoneNumber != phone)
            {
                Individual appuser = _context.Individual.FirstOrDefault(u => u.Id == user.Id);
                appuser.PhoneNumber = model.PhoneNumber;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            TempData[Constants.Message] = $"Vaš profil je promijenjen";
            TempData[Constants.ErrorOccurred] = false;
            return RedirectToAction(nameof(IndexIndividual));


        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);


            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Vaša lozinka je uspješno promijenjena.";

            return RedirectToAction(nameof(ChangePassword));
        }

        /*[HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Vaša lozinka je postavljena.";

            return RedirectToAction(nameof(SetPassword));
        }*/

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion


        [HttpGet]
        public async Task<IActionResult> IndexCompany()
        {

            var user = (Company)await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }
            var model = new IndexCompanyViewModel
            {
                Username = user.UserName,
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                StatusMessage = StatusMessage,
                About = user.About,
                City=user.City,

                BussinesType= user.BussinesType,
                Established = user.Established,
                LogoLink = user.LogoLink,
                PhoneNumber = user.PhoneNumber,
                NumberOfEmployees=user.NumberOfEmployees,
                WebAddress=user.WebAddress
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexCompany(IndexCompanyViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = (Company)await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Nema korisnika sa zadanim ID-em '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var x = _databaseContext.Individual.FirstOrDefault(g => g.Email == model.Email);
                var y = _databaseContext.Company.FirstOrDefault(g => g.Email == model.Email);

                if (x != null || y != null)
                {
                    TempData[Constants.Message] = $"Korisnik s tim mailom već postoji.\n";
                    TempData[Constants.ErrorOccurred] = true;
                    return RedirectToAction(nameof(IndexIndividual));
                }

                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Neočekivana greška se javila prilikom postavljanja e-mail adrese korisnika s ID-em '{user.Id}'.");
                }
                var setUserNameResult = await _userManager.SetUserNameAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Neočekivana greška se javila prilikom postavljanja e-mail adrese korisnika s ID-em '{user.Id}'.");
                }
            }


            var firstName = user.Name;
            if (model.Name != firstName)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.Name = model.Name;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var address = user.Address;
            if (model.Address != address)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.Address = model.Address;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var city = user.City;
            if (model.City != city)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.City = model.City;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var date = user.Established;
            if (model.Established != date)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.Established = model.Established;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var skills = user.About;
            if (model.About != skills)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.About = model.About;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }
            var phone = user.PhoneNumber;
            if (model.PhoneNumber != phone)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.PhoneNumber = model.PhoneNumber;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var a = user.BussinesType;
            if (model.BussinesType != a)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.BussinesType = model.BussinesType;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var b = user.LogoLink;
            if (model.LogoLink != b)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.LogoLink = model.LogoLink;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var c = user.WebAddress;
            if (model.WebAddress != c)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.WebAddress = model.WebAddress;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var d = user.NumberOfEmployees;
            if (model.NumberOfEmployees != d)
            {
                Company appuser = _context.Company.FirstOrDefault(u => u.Id == user.Id);
                appuser.NumberOfEmployees = model.NumberOfEmployees;
                _context.Entry(appuser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            TempData[Constants.Message] = $"Vaš profil je promijenjen";
            TempData[Constants.ErrorOccurred] = false;
            return RedirectToAction(nameof(IndexCompany));


        }

    }
}
