using System.Linq;
using System.Web.Mvc;
using BeerPal.Services.ServiceInterfaces;
using PayPal.Api;

namespace BeerPal.Controllers
{
    public class WebExperienceProfilesController : Controller
    {
        private readonly IPayPalApiHelperService PayPalApiHelperService;

        public WebExperienceProfilesController(IPayPalApiHelperService payPalApiHelperService)
        {
            PayPalApiHelperService = payPalApiHelperService;
        }

        public ActionResult Index()
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            var list = WebProfile.GetList(apiContext);

            if (!list.Any())
            {
                SeedWebProfiles(apiContext);
                list = WebProfile.GetList(apiContext);
            }

            return View(list);
        }

        /// <summary>
        /// Create the default web experience profiles for this example website
        /// </summary>
        private void SeedWebProfiles(APIContext apiContext)
        {
            var digitalGoods = new WebProfile()
            {
                name = "DigitalGoods",
                input_fields = new InputFields()
                {
                    no_shipping = 1
                }
            };
            WebProfile.Create(apiContext, digitalGoods);
        }
    }
}