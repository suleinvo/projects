using AjaxTwitterExpirence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;

namespace AjaxTwitterExpirence.Controllers
{
    public class HomeController : Controller
    {
        HistoryContext HistoryDb = new HistoryContext();
        
        public ActionResult Index()
        {
            var p = HistoryDb.History.ToList();
            ViewBag.List = HistoryDb.History.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult Index(string name, string scount)
        {
            IEnumerable<TwitterStatus> currentTweets = new List<TwitterStatus>();
            if (name != null || scount != null)
            {
                ViewBag.List = HistoryDb.History.ToList();
                History HistoryExem = new History();
                int count = 10;
                if (scount != null)
                    count = int.Parse(scount);
                string _consumerKey = "HxaukPBNrpgm0nuoS2fZUQ";
                string _consumerSecret = "Gv2rF7OTL1HrCNSWFGrvhGGoFeYkYgEuO7mdEhNM";
                string _accessToken = "1688407184-U0rLhXQxOae0AViZVSPTXnPQ9pXq4TZtbQXnH6K";
                string _accessTokenSecret = "gJIHKIJJS2Z0jx1tJiIbeNVAa8OTO2aOrdxEvezF9dA";
                var service = new TwitterService(_consumerKey, _consumerSecret);
                service.AuthenticateWith(_accessToken, _accessTokenSecret);
                var options = new SearchForUserOptions { IncludeEntities = false, Q = name };
                var users = service.SearchForUser(options);
                currentTweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { UserId = users.First().Id, Count = count });
                HistoryExem.Count = count;
                HistoryExem.Name = name;
                HistoryDb.History.Add(HistoryExem);
                HistoryDb.SaveChanges();
                ViewBag.Name = name;
                bool p = Request.IsAjaxRequest();
                if (Request.IsAjaxRequest())
                    return PartialView("Index");
            }
            return View(currentTweets);
        }
    }
}
