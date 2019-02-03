using Fasetto.Word.Web.Server.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Fasetto.Word.Web.Server.Controllers
{
    //Homecontroller = .net will look for the Home folder and index html in it
    public class HomeController : Controller
    {

        #region Protected members
        protected ApplicationDbContext mContext;

        //User creation, deletion, searching, roles etc.
        protected UserManager<ApplicationUser> mUserManager;

        //signin and out
        protected SignInManager<ApplicationUser> mSignInManager;
        #endregion

        #region Constructor
        public HomeController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }
        #endregion

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

        [Route("create")]
        public async Task<IActionResult> CreateUserAsync()
        {
            var result = await mUserManager.CreateAsync(new ApplicationUser
            {
                UserName = "bea",
                Email = "bea@bea.com"
            }, "password");

            if (result.Succeeded)
                return Content("User was created", "text/html");
            return Content("User creation failed", "text/html");
        }

        [Authorize]
        [Route("private")]
        public IActionResult Private()
        {
            return Content($"this is private, welcome {HttpContext.User.Identity.Name}", "text/html");
        }

        /// <summary>
        /// Auto login page for testing
        /// </summary>
        /// <param name="returnUrl"> the url to return to if successfully logged in</param>
        /// <returns></returns>
        [Route("login")]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            //sign out any previous sessions
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);


            var result = await mSignInManager.PasswordSignInAsync("bea", "password", true, false);

            if (result.Succeeded)
            {

                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction(nameof(Index));
                        
                return Redirect(returnUrl);
            }

            return Content("Failed to login", "text/html");
        }

        [Route("logout")]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Content("done");
        }
    }
}