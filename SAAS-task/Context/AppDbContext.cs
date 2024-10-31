using Microsoft.EntityFrameworkCore;
using SAAS_task.User.Models;

namespace SAAS_task.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
        }
        public DbSet<Person> Persons { get; set; }
    }
}
