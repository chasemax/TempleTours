using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
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

        //public void ThisMonth(int year, int month)
        //{
        //    int Month = month;
        //    int Year = year;

        //    DateTime.DaysInMonth(year, month);
        //}

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            DateTime tempdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime thisTempDate = tempdate;
            int[] hourlist = { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            for (int i = 1; i <= 90; i++)
            {
                foreach (int appt in hourlist)
                {
                    DateTime checkdate = new DateTime(thisTempDate.Year, thisTempDate.Month, thisTempDate.Day, appt, 0, 0);

                    if (apptRepo.Appointments.FirstOrDefault(x => x.Time == checkdate) == null)
                    {
                        Appointment newAppt = new Appointment();
                        newAppt.Time = checkdate;
                        newAppt.IsBooked = false;

                        apptRepo.AddAppt(newAppt);
                    }
                }

                thisTempDate = thisTempDate.AddDays(1);
            }

            Dictionary<string, Dictionary<int, Dictionary<int, Appointment>>> AllAppts = new Dictionary<string, Dictionary<int, Dictionary<int, Appointment>>>();


            foreach (Appointment a in apptRepo.Appointments.Where(x => x.Time >= tempdate && x.Time <= tempdate.AddDays(90)))
            {
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(a.Time.Month);
                int day = a.Time.Day;
                int hour = a.Time.Hour;

                if (!AllAppts.ContainsKey(month))
                {
                    AllAppts.Add(month, new Dictionary<int, Dictionary<int, Appointment>>());
                }

                if (!AllAppts[month].ContainsKey(day))
                {
                    AllAppts[month].Add(day, new Dictionary<int, Appointment>());
                }

                AllAppts[month][day].Add(hour, a);
            }

            return View(AllAppts);
        }

        [HttpGet]
        public IActionResult FillForm(int id) // Use the fillform viewmodel for the parameter here eventually
        {
            Signup su = new Signup();
            su.Appointment = apptRepo.Appointments.FirstOrDefault(x => x.Id == id);
            su.AppointmentId = su.Appointment.Id;
            ViewBag.check = 0;
            ViewBag.Time = apptRepo.Appointments.FirstOrDefault(x => x.Id == su.AppointmentId).Time.ToString("MMMM d yyyy h:mm tt");

            return View(su);
        }

        [HttpPost]
        public IActionResult FillForm(Signup su) // Use the fillform viewmodel for the parameter here eventually
        {
            if (ModelState.IsValid)
            {
                su.Appointment = apptRepo.Appointments.FirstOrDefault(x => x.Id == su.AppointmentId);
                signRepo.AddSignup(su);
                su.Appointment.IsBooked = true;
                apptRepo.UpdateAppt(su.Appointment);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.check = 0;
                ViewBag.Time = apptRepo.Appointments.FirstOrDefault(x => x.Id == su.AppointmentId).Time.ToString("MMMM d yyyy h:mm tt");

                return View(su);
            }
        }

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
            Signup s = signRepo.Signups.FirstOrDefault(x => x.Id == apptToDelete);
            s.Appointment.IsBooked = false;
            apptRepo.UpdateAppt(s.Appointment);
            signRepo.RemoveSignup(s);
            return RedirectToAction("Appointments");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var signup = signRepo.Signups.Single(x => x.Id == id);
            ViewBag.check = 1;
            ViewBag.Time = apptRepo.Appointments.FirstOrDefault(x => x.Id == signup.AppointmentId).Time.ToString("MMMM d yyyy h:mm tt");

            return View("FillForm", signup);
        }

        [HttpPost]
        public IActionResult Edit(Signup signup)
        {
            if (ModelState.IsValid)
            {
                signRepo.UpdateSignup(signup);

                return RedirectToAction("Appointments");
            }
            else
            {
                return View("FillForm", signup);
            }
        }
    }

}