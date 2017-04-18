using FYP.Models;
using FYP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;

namespace FYP.Controllers
{
    public class ParticipantController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public CommunityMemberViewModel CommV { get; private set; }

        // GET: Paticipant
        public ActionResult Index()
        {
            var thisUser = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault();

            var myDashboard = new ProfileDataViewModel()
            {
                User = thisUser,
                CommunitiesList = db.CommunityUsers.Include(x => x.Community).Where(u => u.UserID==thisUser.Id).ToList()

            };


            return View();
        }


        //public List<JsonResult> SearchQuery(string SearchQuery)
        //{
        //   var a   = db.Communities.Where(s => s.CommunityName.Contains(SearchQuery));
        //    var b =  db.CommunityUsers.Where(y => y.Community.CommunityName == SearchQuery).Select(y => y.ID).ToList().Count();
        //    return JsonResult { new { b, a }}
        //}

        public bool IsMember(string UseriD, int CommID)
        {

            //return db.Users.Include("CommunityUsers").Any(u => u.Id == UserID && u.CommunityUsers.Any(c => c.CommunityID == CommunityID));

            return db.CommunityUsers.Any(u => u.UserID == UseriD && u.CommunityID == CommID);
        }


        public ActionResult SearchCommunity(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                
                var communityQuery = db.Communities.Include(x => x.CommunityUsers).Where(s => s.CommunityName.Contains(searchString)).ToList();
                //ViewBag.Membership = IsMember(UseriD, ViewBag.communityQuery).ToList();


                return View(communityQuery);

            }

            return HttpNotFound();

            
        }



        //public ActionResult ShowMyCommunity()
        //{
        //    var USiD = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault().Id;

        //    if (!String.IsNullOrEmpty(USiD))
        //    {

        //        //var ShowCommunity = db.Communities.Include(x => x.CommunityUsers).Where(u => ).ToList();
        //        var myCommunityList = db.CommunityUsers.Include(x => x.Community).Where(u => u.UserID == USiD).ToList();


        //        return PartialView("_ShowMyCommunityList", myCommunityList);

        //    }

        //    return HttpNotFound();


        //}





        [HttpPost]
        public ActionResult joinCommunity(int CommID)
        {


            var UseriD = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault().Id;


            var newUser = new CommunityUser()
            {

                UserID = UseriD,
                CommunityID = CommID
            };
            db.CommunityUsers.Add(newUser);
            db.SaveChanges();


            return RedirectToAction("Community", new { id = CommID });
        }




        public ActionResult leaveCommunity(int CommID)
        {


            var UseriD = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault().Id;


            var communityLeave = db.CommunityUsers.Where(u => u.UserID == UseriD && u.CommunityID == CommID).FirstOrDefault();


            if (communityLeave != null) {

                db.CommunityUsers.Remove(communityLeave);
                db.SaveChanges();

            }
            

            return RedirectToAction("Community", new { id = CommID });
        }





        public ActionResult Community(int id)
        {

            var UseriD = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault().Id;

            var Comm = db.Communities.Where(u => u.CommunityID == id).FirstOrDefault();


            var CommV = new CommunityMemberViewModel
            {

                Community = Comm,
                IsMember = IsMember(UseriD, Comm.CommunityID)

            };

            return View(CommV);

        }





    }






    
}