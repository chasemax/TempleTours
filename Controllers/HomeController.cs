using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TempleTours.Models;

namespace TempleTours.Controllers
{
    public class HomeController : Controller
    {

        public IAppointmentRepo apptRepo { get; set; }
        public ISignupRepo signRepo { get; set; }

        public HomeController(IAppointmentRepo a, ISignupRepo s)
        {
            apptRepo = a;
            signRepo = s;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FillForm() // Use the fillform viewmodel for the parameter here eventually
        {
            return View();
        }

        /* Need to figure out how to make this different than the one above, or if we combine the two FillForms
        [HttpPost]
        public IActionResult FillForm() // Use the fillform viewmodel for the parameter here eventually
        {
            return View();
        }
        */

        [HttpGet]
        public IActionResult Appointments()
        {
            /*///////////// CODE FOR ADDING A NEW SIGNUP WITH AN EXISTING APPOINTMENT ////////////////////
            Appointment appt = apptRepo.Appointments.FirstOrDefault(x => x.Id == 3);
            Signup newSignup = new Signup();
            newSignup.Appointment = appt;
            newSignup.Email = "maxfield.chase@gmail.com";
            newSignup.GroupName = "Another Group";
            newSignup.GroupSize = 14;
            signRepo.AddSignup(newSignup);
            */
            

            var signups = signRepo.Signups;
            return View(signups);
        }

        [HttpPost]
        public IActionResult Appointments(int apptToDelete)
        {
            return View();
        }
    }
}
