using HelpDeskSystem.Data;
using HelpDeskSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;

namespace HelpDeskSystem.Controllers
{
    public class UsersController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;    
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext   _context;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var users = await _context.Users.ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ApplicationUser registeredUser = new ApplicationUser();
                registeredUser.UserName = user.UserName;
                registeredUser.Email = user.Email;
                registeredUser.FirstName = user.FirstName;
                registeredUser.LastName = user.LastName;
                registeredUser.MiddleName= user.MiddleName;
                registeredUser.PhoneNumber= user.PhoneNumber;
               
                registeredUser.Email = user.Email;
                registeredUser.NormalizedEmail=user.NormalizedEmail;
                registeredUser.PhoneNumber=user.PhoneNumber;
                registeredUser.EmailConfirmed=true;
                registeredUser.Country= user.Country;   
                registeredUser.Gender= user.Gender;
                registeredUser.City= user.City;

                var result = await _userManager.CreateAsync(registeredUser,user.PasswordHash);
                if (result.Succeeded) {
                    return RedirectToAction(nameof(Index));
                        }
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
    }
}
