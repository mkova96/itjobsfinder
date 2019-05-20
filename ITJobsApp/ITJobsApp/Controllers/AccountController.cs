using ITJobsApp.DLL;
using ITJobsApp.Model.Models;
using ITJobsApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITJobsApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly ITJobsAppContext _databaseContext;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger,
            ITJobsAppContext databaseContext,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _databaseContext = databaseContext;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterIndividual(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["DBs"] = _databaseContext.DataBase.OrderBy(p => p.Name).ToList();
            ViewData["PLs"] = _databaseContext.ProgrammingLanguage.OrderBy(p => p.Name).ToList();

            return View(new RegisterIndividualViewModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterCompany(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new RegisterCompanyViewModel());
        }
        


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterIndividual(RegisterIndividualViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["DBs"] = _databaseContext.DataBase.OrderBy(p => p.Name).ToList();
            ViewData["PLs"] = _databaseContext.ProgrammingLanguage.OrderBy(p => p.Name).ToList();

            if (!await _roleManager.RoleExistsAsync("Individual"))
            {
                var role = new IdentityRole("Individual");
                await _roleManager.CreateAsync(role);
            }

            var dbs = new List<IndividualDataBase>();
            var pls = new List<IndividualProgrammingLanguage>();

            if (ModelState.IsValid)
            {
                var user = new Individual
                {
                    UserName = model.Email,
                    Name = model.Name,
                    Email = model.Email,
                    About=model.About,
                    Address=model.Address,
                    City=model.City,
                    PhoneNumber=model.PhoneNumber,

                    Surname = model.Surname,
                    Skills=model.Skills,
                    DateOfBirth=model.DateOfBirth,
                };
                dbs = model.DBIds.Select(id => _databaseContext.DataBase.Find(id))
               .Select(u => new IndividualDataBase { DataBase = u,Individual = user }).ToList();

                pls = model.PLIds.Select(id => _databaseContext.ProgrammingLanguage.Find(id))
               .Select(u => new IndividualProgrammingLanguage { ProgrammingLanguage = u, Individual = user }).ToList();

                foreach (var dc in dbs)
                {
                    var IdbsInDb = _databaseContext.IndividualDataBase.SingleOrDefault(s => s.Individual.Id == dc.Individual.Id && s.DataBase.Id == dc.DataBase.Id);
                
                if (IdbsInDb == null)
                    {
                        _databaseContext.IndividualDataBase.Add(dc);
                    }
                }
                foreach (var dc in pls)
                {
                    var IplsInDb = _databaseContext.InduvidualProgrammingLanguage.SingleOrDefault(s => s.Individual.Id == dc.Individual.Id && s.ProgrammingLanguage.Id == dc.ProgrammingLanguage.Id);

                    if (IplsInDb == null)
                    {
                        _databaseContext.InduvidualProgrammingLanguage.Add(dc);
                    }
                }

                var x = _databaseContext.Individual.FirstOrDefault(g => g.Email == model.Email);
                var y = _databaseContext.Company.FirstOrDefault(g => g.Email == model.Email);

                if (x != null || y!=null)
                {
                    TempData[Constants.Message] = $"Korisnik s tim mailom već postoji.\n";
                    TempData[Constants.ErrorOccurred] = true;
                    return RedirectToAction(nameof(RegisterIndividual), new { retUrl = returnUrl });
                }

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Individual");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");

                    TempData[Constants.Message] = $"Uspješno ste se registrirali";
                    TempData[Constants.ErrorOccurred] = false;
                    return RedirectToAction("Index", "Ad");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!await _roleManager.RoleExistsAsync("Company"))
            {
                var role = new IdentityRole("Company");
                await _roleManager.CreateAsync(role);
            }

            if (ModelState.IsValid)
            {
                var user = new Company
                {
                    UserName = model.Email,
                    Name = model.Name,
                    Email = model.Email,
                    About = model.About,
                    Address = model.Address,
                    City = model.City,
                    PhoneNumber = model.PhoneNumber,

                    
                    BussinesType = model.BussinesType,
                    LogoLink = model.LogoLink,
                    WebAddress = model.WebAddress,
                    Established=model.Established,
                    NumberOfEmployees=model.NumberOfEmployees
                };

                var x = _databaseContext.Individual.FirstOrDefault(g => g.Email == model.Email);
                var y = _databaseContext.Company.FirstOrDefault(g => g.Email == model.Email);

                if (x != null || y != null)
                {
                    TempData[Constants.Message] = $"Korisnik s tim mailom već postoji.\n";
                    TempData[Constants.ErrorOccurred] = true;
                    return RedirectToAction(nameof(RegisterIndividual), new { retUrl = returnUrl });
                }

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Company");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");

                    TempData[Constants.Message] = $"Uspješno ste se registrirali";
                    TempData[Constants.ErrorOccurred] = false;
                    return RedirectToAction("Index", "Ad");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }





        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(DataBaseController.Index), "Ad");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Ad"); ;
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "UnregHome");
        }

        public ViewResult Show(string id)
        {
            Individual drug = _databaseContext.Individual.Where(i => i.Id.Equals(id.ToString())).FirstOrDefault();
            return View(drug);
        }
    }

}
