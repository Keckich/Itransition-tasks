using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using test5.Data;
using test5.Models;

namespace test5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = _userManager.GetUserAsync(this.User).Result;
            if (user == null || user.Status == "Blocked")
            {
                await _signInManager.SignOutAsync();
                return RedirectPermanent("/Identity/Account/Login");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(string[] selectedUsers, string btn)
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> BlockUser(string[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                
                foreach (var id in selectedUsers)
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(id);                    
                    user.Status = "Blocked";
                    var res = await _userManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {   
                        user.LockoutEnd = DateTime.Now.AddYears(200);
                        await _userManager.UpdateAsync(user);
                    }
                    if (user == _userManager.GetUserAsync(this.User).Result)
                    {
                        await _signInManager.SignOutAsync();
                    }
                }
                if (selectedUsers.Contains(_userManager.GetUserId(this.User)))
                {
                    await _signInManager.SignOutAsync();
                    return RedirectPermanent("/Identity/Account/Login");
                }
            }
            return View("Index");
        }

        [Authorize]
        public async Task<IActionResult> UnblockUser(string[] selectedUsers)
        {            
            foreach (var id in selectedUsers)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                user.Status = "Unblocked";
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    user.LockoutEnd = null;
                    await _userManager.UpdateAsync(user);
                }
            }
            return View("Index");
        }

        [Authorize]
        public async Task<IActionResult> DeleteUser(string[] selectedUsers)
        {
            
            foreach (var id in selectedUsers)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);

                await _userManager.DeleteAsync(user);
                if (user == _userManager.GetUserAsync(this.User).Result)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectPermanent("/Identity/Account/Login");
                }
            }
            return RedirectPermanent("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
