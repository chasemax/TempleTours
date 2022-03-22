using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleTours.Models
{
    public class EFAppointmentRepo : IAppointmentRepo
    {
        public TempleToursContext _ttc { get; set; }

        public EFAppointmentRepo(TempleToursContext temp) => _ttc = temp;

        public IQueryable<Appointment> Appointments => _ttc.Appointments;

        public void AddAppt (Appointment a)
        {
            _ttc.Add(a);
            _ttc.SaveChanges();
        }

        public void UpdateAppt (Appointment a)
        {
            _ttc.Update(a);
            _ttc.SaveChanges();
        }
        public void RemoveAppt(Appointment a)
        {
            _ttc.Remove(a);
            _ttc.SaveChanges();
        }
    }
}
