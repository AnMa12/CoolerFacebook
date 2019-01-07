using CoolerFacebook.Models;
using Microsoft.AspNet.Identity;
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
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();
           
            var friends = from friend in db.Friends
                          where friend.Friend1.ProfileId == currentProfile.ProfileId
                          select friend;
            ViewBag.friends = friends;

            var requests = from fr in db.FriendRequests
                           where fr.Receiver.ProfileId == currentProfile.ProfileId
                           select fr;
            ViewBag.friendRequests = requests;
            return View();
        }

        [HttpPost]
        public ActionResult AddRequest(int id)
        {
            Profile profileReceiver = db.Profiles.Find(id);
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

            FriendRequest fr = new FriendRequest(currentProfile, profileReceiver);
            
            try
            {
                db.FriendRequests.Add(fr);
                db.SaveChanges();
                return Redirect("~/Profile/Show/"+id);
            }
            catch (Exception e)
            {
                return View("Index");
            }

        }

        [HttpPost]
        public ActionResult AddFriend(int id)
        {
            Profile profileReceiver = db.Profiles.Find(id);
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

            FriendRequest friendsRequest = (from fr in db.FriendRequests
                                            where fr.Sender.ProfileId == currentProfile.ProfileId && fr.Receiver.ProfileId == profileReceiver.ProfileId
                                            select fr).FirstOrDefault();

            FriendRequest friendsRequest2 = (from fr in db.FriendRequests
                                             where fr.Sender.ProfileId == profileReceiver.ProfileId && fr.Receiver.ProfileId == currentProfile.ProfileId
                                             select fr).FirstOrDefault();

            Friend fr1 = new Friend(currentProfile, profileReceiver);
            Friend fr2 = new Friend(profileReceiver, currentProfile);

            try
            {
                if(friendsRequest != null)
                {
                    db.FriendRequests.Remove(friendsRequest);

                }
                if (friendsRequest2 != null)
                {
                    db.FriendRequests.Remove(friendsRequest2);

                }
                db.Friends.Add(fr1);
                db.Friends.Add(fr2);
                db.SaveChanges();
                return Redirect("~/Profile/Show/" + id);
            }
            catch (Exception e)
            {
                return View("Index");
            }

        }
        [HttpPost]
        public ActionResult DeleteFriend(int id)
        {
            Profile profileReceiver = db.Profiles.Find(id);
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

            Friend friends1 = (from friend in db.Friends
                              where friend.Friend1.ProfileId == currentProfile.ProfileId && friend.Friend2.ProfileId == profileReceiver.ProfileId
                              select friend).FirstOrDefault();

            Friend friends2 = (from friend in db.Friends
                              where friend.Friend1.ProfileId == currentProfile.ProfileId && friend.Friend2.ProfileId == profileReceiver.ProfileId
                              select friend).FirstOrDefault();

            try
            {
                db.Friends.Remove(friends1);
                db.Friends.Remove(friends2);
                db.SaveChanges();
                return Redirect("~/Profile/Show/" + id);
            }
            catch (Exception e)
            {
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult CancelRequest(int id)
        {
            Profile profileReceiver = db.Profiles.Find(id);
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

            FriendRequest friendsRequest = (from fr in db.FriendRequests
                                            where fr.Sender.ProfileId == currentProfile.ProfileId && fr.Receiver.ProfileId == profileReceiver.ProfileId
                                            select fr).FirstOrDefault();

            try
            {
                db.FriendRequests.Remove(friendsRequest);
                db.SaveChanges();
                return Redirect("~/Profile/Show/" + id);
            }
            catch (Exception e)
            {
                return View("Index");
            }

        }

        public ActionResult SearchFriend(string lastName, string firstName)
        {
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();
            if (User.IsInRole("Administrator"))
            {
                ViewBag.adm = "true";
            }
            //ViewBag.Profiles = from profile in db.Profiles
            //                   select profile;
            if (lastName != null && firstName != null)
            {
                if(lastName != "" && firstName != "")
                {
                    var profiles = from profile in db.Profiles
                                   where profile.FirstName == firstName && profile.LastName == lastName && profile.ProfileId != currentProfile.ProfileId
                                   select profile;
                    ViewBag.profiles = profiles;
                }
            else
            if (lastName != "")
                {
                    var profiles = from profile in db.Profiles
                                   where profile.LastName == lastName && profile.ProfileId != currentProfile.ProfileId
                                   select profile;
                    ViewBag.profiles = profiles;
                }
                else
                if (firstName != "")
                {
                    var profiles = from profile in db.Profiles
                                   where profile.FirstName == firstName && profile.ProfileId != currentProfile.ProfileId
                                   select profile;
                    ViewBag.profiles = profiles;
                }

                if (ViewBag.profiles != null)
                {
                    foreach (var prf in ViewBag.profiles)
                    {
                        Profile friendProfile = db.Profiles.Find(prf.ProfileId);
                        Friend friends = (from friend in db.Friends
                                          where friend.Friend1.ProfileId == currentProfile.ProfileId && friend.Friend2.ProfileId == friendProfile.ProfileId
                                          select friend).FirstOrDefault();

                        if (friends == null)
                        {
                            prf.Path = "Nu sunteti prieteni";
                        }
                        else
                        {
                            prf.Path = "Sunteti prieteni";
                        }
                    }
                }

            }

            return View();
        }
    }
}