using Microsoft.EntityFrameworkCore;

namespace Third_App.DAL;

public class AppDbContext: DbContext   
{   
    public DbSet<Instructor> Instructors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=JSB-3;User Id=SA;Password=YourPassword123;Encrypt=False;");
    }
}