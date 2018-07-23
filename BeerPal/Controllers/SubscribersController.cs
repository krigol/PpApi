using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerPal.Models;
using BeerPal.Services.ServiceInterfaces;
using Microsoft.AspNet.Identity.Owin;
using PayPal.Api;

namespace BeerPal.Controllers
{
    public class SubscribersController : Controller
    {
        private ApplicationDbContext _dbContext => HttpContext.GetOwinContext().Get<ApplicationDbContext>();

        private readonly IPayPalApiHelperService PayPalApiHelperService;

        public SubscribersController(IPayPalApiHelperService payPalApiHelperService)
        {
            PayPalApiHelperService = payPalApiHelperService;
        }

        public ActionResult Index()
        {
            var subscriptions = _dbContext.Subscriptions.OrderByDescending(x => x.StartDate).Take(50).ToList();

            return View(subscriptions);
        }

        public ActionResult Details(string id)
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            var agreement = Agreement.Get(apiContext, id);

            return View(agreement);
        }

        public ActionResult Suspend(string id)
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            Agreement.Suspend(apiContext, id, new AgreementStateDescriptor()
            {
                note = "Suspended"
            });

            return RedirectToAction("Details", new {id = id});
        }

        public ActionResult Reactivate(string id)
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            Agreement.ReActivate(apiContext, id, new AgreementStateDescriptor()
            {
                note = "Reactivated"
            });

            return RedirectToAction("Details", new {id = id});
        }

        public ActionResult Cancel(string id)
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            Agreement.Cancel(apiContext, id, new AgreementStateDescriptor()
            {
                note = "Cancelled"
            });            

            return RedirectToAction("Details", new {id = id});
        }
    }
}