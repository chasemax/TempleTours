using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempleTours.Models
{
    public class TempleToursContext : DbContext 
    {
        public TempleToursContext(DbContextOptions<TempleToursContext> options) : base (options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Signup> Signups { get; set; }
    }
}
