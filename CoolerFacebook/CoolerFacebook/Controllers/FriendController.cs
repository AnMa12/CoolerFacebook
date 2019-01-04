using CoolerFacebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolerFacebook.Controllers
{ 
    public class FriendController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Friend
        public ActionResult Index()
        {
            var friends = from friend in db.Friends
                         select friend;
            ViewBag.Friends = friends;
            return View();
        }
    }
}