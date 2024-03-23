﻿using etickets.Data;
using etickets.Data.Static;
using etickets.Data.ViewModel;
using etickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace etickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login() => View(new LoginVM());
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordcheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordcheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong Credentials, please try again";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong Credentials, please try again";
            return View(loginVM);
        }
        public IActionResult Register() => View(new RegisterVM());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user == null)
            {
                var newUser = new ApplicationUser()
                {
                    fullName = registerVM.fullName,
                    Email = registerVM.EmailAddress,
                    UserName = registerVM.EmailAddress
                };
                var newUserResult = await _userManager.CreateAsync(newUser, registerVM.Password);
                if (newUserResult.Succeeded) await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegistrationCompleted");
            }
            TempData["Error"] = "The email address already exist";
            return View(registerVM);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return RedirectToAction("Index", "Movies");
        }
    }
}
