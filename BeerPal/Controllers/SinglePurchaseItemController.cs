using BeerPal.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPal.Controllers
{
    public class SinglePurchaseItemController : Controller
    {
        private ApplicationDbContext _dbContext => HttpContext.GetOwinContext().Get<ApplicationDbContext>();

        // GET: SinglePurchaseItem
        public ActionResult Index()
        {

            return View();
        }

        // GET: SinglePurchaseItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SinglePurchaseItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SinglePurchaseItem/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SinglePurchaseItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SinglePurchaseItem/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SinglePurchaseItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SinglePurchaseItem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
