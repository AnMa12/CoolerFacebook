using CoolerFacebook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolerFacebook.Controllers
{
    public class ChatController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chat
         public ActionResult Index(string idProfile, string text)
        {
            var currentUserId = User.Identity.GetUserId();
            Profile currentProfile = db.Profiles.Where(i => i.User.Id == currentUserId).FirstOrDefault();

            var friends = from friend in db.Friends
                          where friend.Friend1.ProfileId == currentProfile.ProfileId
                          select friend;
            ViewBag.friends = friends;

            if(idProfile != null)
            {
                ViewBag.friendProfileId = idProfile;

                /* anna - add chattingFriend to get the chattingFriend name */
                Profile chattingFriend = db.Profiles.Find(Int32.Parse(idProfile));
                ViewBag.chattingFriend = chattingFriend;
                /* anna - end */

                int id = int.Parse(idProfile);
                Profile friendProfile = db.Profiles.Find(id);
 
                Chat chat1 = (from chat in db.Chats
                              where chat.Friend1.ProfileId == currentProfile.ProfileId && chat.Friend2.ProfileId == friendProfile.ProfileId
                              select chat).FirstOrDefault();

                Chat chat2 = (from chat in db.Chats
                              where chat.Friend1.ProfileId == friendProfile.ProfileId && chat.Friend2.ProfileId == currentProfile.ProfileId
                              select chat).FirstOrDefault();

               
                if (chat1 == null && chat2 == null)
                {
                    try
                    {
                            Chat chat = new Chat(currentProfile, friendProfile);
                        
                            db.Chats.Add(chat);
                            db.SaveChanges();
                        
                    }
                    catch (Exception e)
                    {
                        return View("Index");
                    }

             
                }
                else
                    if(chat1 != null)
                {
                    if (text != null)
                    {
                        try
                        {
                            Message mes = new Message(text, chat1, currentProfile);

                            db.Messages.Add(mes);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return View("Index");
                        }
                    }
                    ViewBag.messages = from mes in db.Messages
                                       where mes.Chat.ChatId == chat1.ChatId
                                       orderby mes.Date
                                       select mes;

                }
                else if( chat2 != null)
                {
                    if (text != null)
                    {
                        try
                        {
                            Message mes = new Message(text, chat2, currentProfile);

                            db.Messages.Add(mes);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return View("Index");
                        }
                    }
                    //ViewBag.messages = chat2.Messages;
                    ViewBag.messages = from mes in db.Messages
                                       where mes.Chat.ChatId == chat2.ChatId
                                       orderby mes.Date
                                       select mes;
                }

            }

            
            return View();
        }

      
    }
}