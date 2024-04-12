using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Controllers;
using School_Management_System.Models;
using System.Data;

namespace School_Management_System.Context
{
    public class DatabseContext:IdentityDbContext
    {
        public DatabseContext(DbContextOptions<DatabseContext>options):base(options)
        {
            
        }
        public DbSet<Students> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
