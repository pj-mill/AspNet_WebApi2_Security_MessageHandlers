using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebService.DataAccess;
using WebService.Models;

namespace WebService.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) { }

        /// <summary>
        /// Where Identity data is accessed, this will help us to hide the details of how IdentityUser is stored throughout the application.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ApplicationUserManager Create()
        {
            var context = new ApplicationContext();

            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            return appUserManager;
        }
    }
}