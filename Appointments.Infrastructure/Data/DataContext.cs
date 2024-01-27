using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointments.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Trainee> Trainees { get; set; }
    }
}