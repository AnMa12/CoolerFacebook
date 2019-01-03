using System;
using System.Collections.Generic;
using System.IO;
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
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Add(int Id)
        {
            ViewBag.AlbumId = Id;
               
            return View();
        }


        [HttpPost]
        public ActionResult Add(HttpPostedFileBase file, string AlbumId)
        {
            int id = int.Parse(AlbumId);
            var album = db.Albums.Find(id);
            var path = FilesHandler.saveImage(file, Server);
            Photo img = new Photo(path, album);
            try
            {

                db.Photos.Add(img);
                db.SaveChanges();
                //return RedirectToAction("ShowPhotos");
                return Redirect("/Album/ShowPhotos/" + id);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult ShowPhotos(int Id)
        {

            var album = db.Albums.Find(Id);
            ViewBag.album = album;
            ViewBag.pics = album.Photos;
            return View();

        }

        [HttpPost]
        public ActionResult AddComment( string Text)
        {
           /* var currentUserId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(currentUserId);
            int id = int.Parse(PhotoId);
            Photo pic = db.Photos.Find(id);
            Comment comment = new Comment("lavinia", pic, user);
                 
            try
            {

                db.Comments.Add(comment);
                db.SaveChanges();
                //return RedirectToAction("ShowPhotos");
                return Redirect("/Album/ShowPhotos/" + pic.Album.AlbumId);
                //return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                return View("New");
            }*/
            return View("New");

        }


    }

    public class FilesHandler
    {
        public static String aplicationPath = "~/Content";
        public static String relativeFolder = Path.GetFileName("Images");
        public static String saveImage(HttpPostedFileBase picture, HttpServerUtilityBase server)
        {

            string subPath = Path.Combine(server.MapPath(aplicationPath), relativeFolder);
            if (!System.IO.Directory.Exists(subPath))
                System.IO.Directory.CreateDirectory(subPath);

            string fullPath = Path.Combine(subPath, Path.GetFileName(picture.FileName));
            try
            {
                picture.SaveAs(fullPath);
            }
            catch (Exception ex)
            {
                return null;
            }

            string relativePath = "/Content/" + "Images/" + picture.FileName;
            return relativePath;
        }
    }
}


