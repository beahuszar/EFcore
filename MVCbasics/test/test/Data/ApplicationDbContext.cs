using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBasics.Data
{
    //represent the DB connection to/with the application
    public class ApplicationDbContext : DbContext
    {

        //this is a table in the database
        public  DbSet<SettingsDataModel> Settings { get; set; }

        //pass them into the base Db context
        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Fluent API (.IsUnique() at the end = [required] in the model class above the prop
            //All DB prop settings can be done in fluent api while indexing does not have all these possibilities
            modelBuilder.Entity<SettingsDataModel>().HasIndex(a => a.Name);
        }
    }
}
