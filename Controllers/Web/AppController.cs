using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController (IMailService mailService, 
            IConfigurationRoot config, 
            IWorldRepository repository,
            ILogger<AppController> logger)
        {
            // we're injecting a new object that implements IMailService
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        //These methods return the views 
        public IActionResult Index () 
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            // page authenticated users can go to to view their trips
            try
            {
                var data = _repository.GetAllTrips();

                // returns view - takes arguments - in this case data we get from SQL we can manipulate in view
                return View(data);
            }
            catch (Exception ex)
            {
                /* Using the _logger */
                _logger.LogError($"Failed to get trips in index page: {ex.Message}");
                return Redirect("/error");
            }

        }

        public IActionResult Contact () 
        {
            //throw new InvalidOperationException("Bad things happen to good developers");
            return View();
        }

        [HttpPost]  // if someone posts from the contact view
        public IActionResult Contact (ContactViewModel model)
        {
            if (model.Email.Contains("aol.com")) ModelState.AddModelError("", "We don't support AOL addresses");
            if(ModelState.IsValid)
            {
                // overload for contact() method
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "Website Contact", model.Message);

                ViewBag.UserMessage = "Message Sent";
            }
            
            return View(); 
        }

        public IActionResult About ()
        {
            return View();
        }
       
    }
}
