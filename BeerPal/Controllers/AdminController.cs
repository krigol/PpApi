using System.Web.Mvc;

namespace BeerPal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Sales");
        }        
    }
}
