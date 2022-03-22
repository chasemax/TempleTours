using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleTours.Models
{
    public class EFSignupRepo : ISignupRepo
    {
        public TempleToursContext _ttc { get; set; }

        public EFSignupRepo(TempleToursContext temp) => _ttc = temp;

        public IQueryable<Signup> Signups => _ttc.Signups.Include(x => x.Appointment);

        public void AddSignup (Signup s)
        {
            _ttc.Add(s);
            _ttc.SaveChanges();
        }

        public void UpdateSignup(Signup s)
        {
            _ttc.Update(s);
            _ttc.SaveChanges();
        }
        public void RemoveSignup(Signup s)
        {
            _ttc.Remove(s);
            _ttc.SaveChanges();
        }
    }
}
