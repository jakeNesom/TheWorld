using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            /* When using asyncronous methods, your parent method must also be asyncronous 
              and a Task */
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                                                      vm.Password,
                                                                      true, false);

                if (signInResult.Succeeded)
                {
                    // This makes sure the user goes where they intended to go 
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        /* if we use authorization on other controllers
                        redirecting witht the returnURL will keep users
                        in the same spot they were accessing */
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    // we want the error to be at the entire model state so 
                    // we leave the model blank
                    ModelState.AddModelError("", "Username or password is incorrect");
                }
            }

            return View();

        }

        public async Task<IActionResult> Logout () 
        {
            if(User.Identity.IsAuthenticated) 
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }
    }
}
