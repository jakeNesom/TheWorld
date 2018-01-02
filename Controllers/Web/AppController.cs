using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        public AppController ()
        {
            
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
        public IActionResult Contact (ContactViewModel model) {

            return View();
        }

        public IActionResult About ()
        {
            return View();
        }
       
    }
}
