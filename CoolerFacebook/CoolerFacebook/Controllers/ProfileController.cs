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
            //AICI TREBUIE IN FUNCTIE DE USER SA LUAM ID-UL PROFILULUI!!!
            var currentUserId = User.Identity.GetUserId();
            Profile profile = db.Profiles.Where( i => i.User.Id == currentUserId).FirstOrDefault();
            ViewBag.Profile = profile;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }
            if (profile != null)
            ViewBag.Albums = profile.Albums;
            return View();
        }

        // GET: Profile by profileId
        public ActionResult Show(int id)
        {
            Profile profile = db.Profiles.Find(id);
            ViewBag.Profile = profile;
            if (profile != null)
            ViewBag.Albums = profile.Albums;
            return View();
        }

        // CREATE: Profile
        public ActionResult New()
        {
            var currentUserId = User.Identity.GetUserId();
            ViewBag.user = db.Users.Find(currentUserId);
            return View();
        }

        [HttpPost]
        public ActionResult New(Profile profile)
        {
            var currentUserId = User.Identity.GetUserId();
            try
            {
                
                profile.User = db.Users.Find(currentUserId);
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
        public ActionResult Edit(int id)
        {
            Profile profile = db.Profiles.Find(id);
            ViewBag.Profile = profile;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, Profile requestProfile)
        {
            try
            {
                Profile profile = db.Profiles.Find(id);
                if (TryUpdateModel(profile))
                {
                    TempData["message"] = "S-a modificat " + profile.FirstName;
                    profile.FirstName = requestProfile.FirstName;
                    profile.LastName = requestProfile.LastName;
                    profile.Description = "lavinia";
                        //requestProfile.Description;
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                TempData["message"] = "NU s-a modificat " + profile.ProfileId + id;
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}