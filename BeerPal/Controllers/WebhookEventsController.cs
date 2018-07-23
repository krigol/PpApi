using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeerPal.Extras;
using BeerPal.Models;
using BeerPal.Services.ServiceInterfaces;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PayPal.Api;

namespace BeerPal.Controllers
{
    public class WebhookEventsController : Controller
    {
        private ApplicationDbContext _dbContext => HttpContext.GetOwinContext().Get<ApplicationDbContext>();
        private readonly IPayPalApiHelperService PayPalApiHelperService;

        public WebhookEventsController(IPayPalApiHelperService payPalApiHelperService)
        {
            PayPalApiHelperService = payPalApiHelperService;
        }

        public ActionResult Index()
        {
            var webhookEvents = _dbContext.WebhookEvents.OrderByDescending(x => x.DateReceived).Take(50).ToList();

            return View(webhookEvents);
        }

        [HttpPost]
        public ActionResult SimulateEvent(string eventType)
        {
            var simlulatableWebhook = new SimulatableWebhook();
            var webhookEvent = simlulatableWebhook.SimulateEvent(eventType, "8S505795N37043444");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Receive()
        {
            var apiContext = PayPalApiHelperService.GetApiContext();

            var postBody = new StreamReader(Request.InputStream).ReadToEnd();
            var webhookEvent = JsonConvert.DeserializeObject<WebhookEvent>(postBody);

            // Ensure this event was genuinely sent by PayPal
            var isValid = WebhookEvent.ValidateReceivedEvent(apiContext, Request.Headers, postBody, "8S505795N37043444"); // Last parameter is your Webhook ID

            if (!isValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var localEvent = new Entities.WebhookEvent()
            {
                EventType = webhookEvent.event_type,
                PayPalWebHookEventId = webhookEvent.id,
                ResourceType = webhookEvent.resource_type,
                Summary = webhookEvent.summary,
                ResourceJson = JsonConvert.SerializeObject(webhookEvent.resource),
                DateCreated = DateTime.Parse(webhookEvent.create_time),
                DateReceived = DateTime.UtcNow
            };
            _dbContext.WebhookEvents.Add(localEvent);
            _dbContext.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}