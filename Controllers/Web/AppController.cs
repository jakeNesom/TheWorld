using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        public AppController (IMailService mailService, IConfigurationRoot config )
        {
            // we're injecting a new object that implements IMailService
            _mailService = mailService;
            _config = config;
        }

        //These methods return the views 
        public IActionResult Index () 
        {
            // this looks to return a view
            // by default mvc looks in the ./views/app folder for the 'razor' page
            return View();
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
