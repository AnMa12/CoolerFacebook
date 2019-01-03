using CoolerFacebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoolerFacebook.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        // CREATE: Profile
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Profile profile)
        {
            try
            {
                //var user = User.Identity.GetUserId();
                //profile.User.Id = user;
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // EDIT: Profile
        public ActionResult Edit(int ProfileId)
        {
            Profile profile = db.Profiles.Find(ProfileId);
            ViewBag.Profile = profile;
            return View();
        }
    }
}