using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ntils.Models;

namespace Ntils.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [Route("initialMessages")]
        public JsonResult InitialMessages()
        {
            var initialMessages = FakeMessageStore.FakeMessages.OrderByDescending(m => m.Date).Take(2).ToArray();

            var initialValues = new ClientState()
            {
                Messages = initialMessages,
                LastFetchedMessageDate = initialMessages.Last().Date
            };

            return Json(initialValues);
        }

        [Route("fetchMessages")]
        public JsonResult FetchMessages(DateTime lastFetchedMessageDate)
        {
            return Json(FakeMessageStore.FakeMessages.OrderByDescending(m => m.Date)
                .SkipWhile(m => m.Date >= lastFetchedMessageDate).Take(1));
        }
    }
}