using Microsoft.EntityFrameworkCore;
using Uplers_Exam.Models;

namespace Uplers_Exam.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ToDoModel> Tasks { get; set; }
    }
}
