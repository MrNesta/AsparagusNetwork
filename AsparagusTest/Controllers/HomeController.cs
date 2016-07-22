using AsparagusTest.Models.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace AsparagusTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: Home

        public ActionResult Index()
        {
            var foods = UserManager.GetAllFoods();

            return View(foods);
        }
    }
}