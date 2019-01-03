using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolerFacebook.Models;
using Microsoft.AspNet.Identity;

namespace CoolerFacebook.Controllers
{
    public class AlbumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Album
        public ActionResult Index()
        {
            var albums = from album in db.Albums
                           orderby album.Title
                           select album;
            ViewBag.Albums = albums;
            return View();
        }

        public ActionResult Show(int id)
        {
            Album album = db.Albums.Find(id);
            ViewBag.Albums = album;
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
        public ActionResult New(Album album)
        {
            var currentUserId = User.Identity.GetUserId();
            try
            {
                var userId = User.Identity.GetUserId();
                Profile profile = db.Profiles.Where(i => i.User.Id == userId).FirstOrDefault();
                album.Profile = profile;
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }


}