using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleTours.Models
{
    public interface IAppointmentRepo
    {
        IQueryable<Appointment> Appointments { get; }

        public void AddAppt(Appointment a);
    }
}
