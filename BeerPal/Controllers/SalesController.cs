using System.Web.Mvc;
using BeerPal.Services.ServiceInterfaces;
using PayPal.Api;

namespace BeerPal.Controllers
{
    public class SalesController : Controller
    {
        private readonly IPayPalApiHelperService PayPalApiHelperService;

        public SalesController(IPayPalApiHelperService payPalApiHelperService)
        {
            PayPalApiHelperService = payPalApiHelperService;
        }

        public ActionResult Index()
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            var sales = Payment.List(apiContext);

            return View(sales);
        }

        public ActionResult Refund(string saleId)
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            var sale = new Sale()
            {
                id = saleId
            };

            // A refund with no details refunds the entire amount.
            var refund = sale.Refund(apiContext, new Refund());

            return RedirectToAction("Index");
        }
    }
}