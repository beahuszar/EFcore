using EntityFrameworkBasics.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EntityFrameworkBasics.Controllers
{
    //Homecontroller = .net will look for the Home folder and index html in it
    public class HomeController : Controller
    {
        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext mContext;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        public HomeController(ApplicationDbContext context)
        {
            mContext = context;
        }


        public IActionResult Index()
        {
            // Make sure we have the database
            mContext.Database.EnsureCreated();

            // If we have no settings already...
            if (!mContext.Settings.Any())
            {
                // Add a new setting
                mContext.Settings.Add(new SettingsDataModel
                {
                    Name = "BackgroundColor",
                    Value = "Red"
                });

                // Check to show the new setting is currently only local and not in the database
                var settingsLocally = mContext.Settings.Local.Count();
                var settingsDatabase = mContext.Settings.Count();
                var firstLocal = mContext.Settings.Local.FirstOrDefault();
                var firstDatabase = mContext.Settings.FirstOrDefault();

                // Commit setting to database
                mContext.SaveChanges();

                // Recheck to show its now in local and the actual database
                settingsLocally = mContext.Settings.Local.Count();
                settingsDatabase = mContext.Settings.Count();
                firstLocal = mContext.Settings.Local.FirstOrDefault();
                firstDatabase = mContext.Settings.FirstOrDefault();
            }

            return View();
        }
    }
}