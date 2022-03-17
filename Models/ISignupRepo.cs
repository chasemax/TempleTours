using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleTours.Models
{
    public interface ISignupRepo
    {
        IQueryable<Signup> Signups { get; }
    }
}
