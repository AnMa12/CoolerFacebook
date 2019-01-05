using CoolerFacebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolerFacebook.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Group
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

            var groups = from grp in db.ProfileGroups
                         where grp.Profile.ProfileId == currentProfile.ProfileId
                         select grp.Group;

            if(groups.Count() != 0)
            {
                ViewBag.groups = groups;
            }

            return View();
        }
        [HttpPost]
        public ActionResult New(Group group)
        {

            try
            {
                db.Groups.Add(group);
                db.SaveChanges();

                var currentUserId = User.Identity.GetUserId();
                Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

                ProfileGroup pg = new ProfileGroup(currentProfile, group);
                db.ProfileGroups.Add(pg);
                db.SaveChanges();

            }
            catch (Exception e)
                    {
                        return View("Index");
                    }
            return RedirectToAction("Index");
        }        public ActionResult Show(string GroupId, string text)
        {
            int id = int.Parse(GroupId);
            Group grp = db.Groups.Find(id);
            ViewBag.group = grp;
            

            if(text != null)
            {
                var currentUserId = User.Identity.GetUserId();
                Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();
                
                Mesaj msj = new Mesaj(text, grp, currentProfile);
                try
                {
                    db.Mesajs.Add(msj);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return View("Index");
                }

            }

            return View();

        }        [HttpDelete]
        public ActionResult Delete(int id)
        {

            Group grp = db.Groups.Find(id);

            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

            ProfileGroup pg = (from profg in db.ProfileGroups
                               where profg.Profile.ProfileId == currentProfile.ProfileId && profg.Group.GroupId == grp.GroupId
                               select profg).FirstOrDefault();
            db.ProfileGroups.Remove(pg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}