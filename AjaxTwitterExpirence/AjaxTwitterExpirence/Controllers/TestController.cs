using AjaxTwitterExpirence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Specialized;

namespace AjaxTwitterExpirence.Controllers
{
    public class TestController : Controller
    {
        HistoryContext HistoryDb = new HistoryContext();
        string _consumerKey = WebConfigurationManager.AppSettings["consumerkey"];
        string _consumerSecret = WebConfigurationManager.AppSettings["consumersecret"];
        string _accessToken = WebConfigurationManager.AppSettings["accesstoken"];
        string _accessTokenSecret = WebConfigurationManager.AppSettings["accesstokensecret"];

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New(string title, string count)
        {
            ViewBag.List = HistoryDb.History.ToList();
            History HistoryExem = new History();
            int Ncount = int.Parse(count);
            //string _consumerKey = "HxaukPBNrpgm0nuoS2fZUQ";
            //string _consumerSecret = "Gv2rF7OTL1HrCNSWFGrvhGGoFeYkYgEuO7mdEhNM";
            //string _accessToken = "1688407184-U0rLhXQxOae0AViZVSPTXnPQ9pXq4TZtbQXnH6K";
            //string _accessTokenSecret = "gJIHKIJJS2Z0jx1tJiIbeNVAa8OTO2aOrdxEvezF9dA";
            var service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);
            var options = new SearchForUserOptions { IncludeEntities = false, Q = title };
            var users = service.SearchForUser(options);
            var currentTweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { UserId = users.First().Id, Count = Ncount });
            HistoryExem.Count = Ncount;
            HistoryExem.Name = title;
            HistoryDb.History.Add(HistoryExem);
            HistoryDb.SaveChanges();
            ViewBag.Name = title;
            ViewBag.Count = Ncount + 10;
            return PartialView("Task", currentTweets);
        }
        [HttpPost]
        public ActionResult Linkes(string title, string count)
        {
            return View(New(title,count));
        }

    }
}
