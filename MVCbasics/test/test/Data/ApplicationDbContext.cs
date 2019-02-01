using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBasics.Data
{
    //represent the DB connection to/with the application
    public class ApplicationDbContext : DbContext
    {
        //this is a table in the database
        public  DbSet<SettingsDataModel> Settings { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=entityframework;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
