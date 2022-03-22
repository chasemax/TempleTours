using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;

            var nextYear = year + 1;

            var daysInMonth = DateTime.DaysInMonth(year, month);

            var futureMonth1 = 0;
            var futureMonth2 = 0;
            var futureMonth3 = 0;

            var daysFutureMonth1 = 0;
            var daysFutureMonth2 = 0;
            var daysFutureMonth3 = 0;

            var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

            if (month == 12)
            {
                futureMonth1 = 1;
                futureMonth2 = 2;
                futureMonth3 = 3;

                daysFutureMonth1 = DateTime.DaysInMonth(nextYear, futureMonth1);
                daysFutureMonth2 = DateTime.DaysInMonth(nextYear, futureMonth2);
                daysFutureMonth3 = DateTime.DaysInMonth(nextYear, futureMonth3);
            }
            else if (month == 11)
            {
                futureMonth1 = month + 1;
                futureMonth2 = 1;
                futureMonth3 = 2;

                daysFutureMonth1 = DateTime.DaysInMonth(year, futureMonth1);
                daysFutureMonth2 = DateTime.DaysInMonth(nextYear, futureMonth2);
                daysFutureMonth3 = DateTime.DaysInMonth(nextYear, futureMonth3);
            }
            else if (month == 10)
            {
                futureMonth1 = month + 1;
                futureMonth2 = month + 2;
                futureMonth3 = 1;

                daysFutureMonth1 = DateTime.DaysInMonth(year, futureMonth1);
                daysFutureMonth2 = DateTime.DaysInMonth(year, futureMonth2);
                daysFutureMonth3 = DateTime.DaysInMonth(nextYear, futureMonth3);
            }
            else
            {
                futureMonth1 = month + 1;
                futureMonth2 = month + 2;
                futureMonth3 = month + 3;

                daysFutureMonth1 = DateTime.DaysInMonth(year, futureMonth1);
                daysFutureMonth2 = DateTime.DaysInMonth(year, futureMonth2);
                daysFutureMonth3 = DateTime.DaysInMonth(year, futureMonth3);
            }

            ViewBag.day = day;
            ViewBag.month = month;
            ViewBag.year = year;
            ViewBag.daysInMonth = daysInMonth;

            ViewBag.daysFutureMonth1 = daysFutureMonth1;
            ViewBag.daysFutureMonth2 = daysFutureMonth2;
            ViewBag.daysFutureMonth3 = daysFutureMonth3;

            ViewBag.futureMonth1 = futureMonth1;
            ViewBag.futureMonth2 = futureMonth2;
            ViewBag.futureMonth3 = futureMonth3;

            ViewBag.monthName = monthName;
            ViewBag.futureMonthName1 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(futureMonth1);
            ViewBag.futureMonthName2 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(futureMonth2);
            ViewBag.futureMonthName3 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(futureMonth3);

            var booked = apptRepo.Appointments
                .Where(x => x.IsBooked == true)
                .ToList();

            return View(booked);
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
            return View();
        }

        [HttpPost]
        public IActionResult Appointments(int apptToDelete)
        {
            return View();
        }
    }
}
