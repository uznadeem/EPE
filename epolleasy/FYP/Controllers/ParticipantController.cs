using FYP.Models;
using FYP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;
using FYP.Services;
using System.Threading.Tasks;

namespace FYP.Controllers
{
    [Authorize]
    [Authorize(Roles ="Participant")]
        
    public class ParticipantController : Controller
    {


        private readonly ParticipantService _ps;

        public ParticipantController() : this(new ParticipantService()) { }

        public ParticipantController(ParticipantService ParticipantS)
        {

            _ps = ParticipantS;

        }
      
        // GET: Paticipant
        public async Task<ActionResult> Index()
        {


            var dashboard = await _ps.GetParticipantPanelAsync();

            return View(dashboard);
        }


        //public List<JsonResult> SearchQuery(string SearchQuery)
        //{
        //   var a   = db.Communities.Where(s => s.CommunityName.Contains(SearchQuery));
        //    var b =  db.CommunityUsers.Where(y => y.Community.CommunityName == SearchQuery).Select(y => y.ID).ToList().Count();
        //    return JsonResult { new { b, a }}
        //}

        //public bool IsMember(string UseriD, int CommID)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    //return db.Users.Include("CommunityUsers").Any(u => u.Id == UserID && u.CommunityUsers.Any(c => c.CommunityID == CommunityID));


        //    return db.CommunityUsers.Any(u => u.UserID == UseriD && u.CommunityID == CommID);
        //}

        [HttpGet]
        public async Task<ActionResult> SearchCommunity(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {

                var communityQuery = await _ps.SearchCommunityAsync(searchString);
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
        public async Task<ActionResult> joinCommunity(int CommID)
        {

            
            await _ps.joinCommunityAsync(CommID);


            return RedirectToAction("ParticipantViewDetail", new { id = CommID });
        }




        public async Task <ActionResult> leaveCommunity(int CommID)
        {




            await _ps.leaveCommunityAsync(CommID);


            return RedirectToAction("ParticipantViewDetail", new { id = CommID });
        }





        public async Task<ActionResult> Community(int id)
        {


            var cm = await _ps.CommunityAsync(id);

            return View(cm);

        }

        public async Task <ActionResult> ParticipantViewDetail(int id)
        {

            var da = await _ps.ParticipantViewDetailAsync(id);

            ViewBag.Id = id;
            return View(da);
        }


        public async Task<ActionResult> FormParticipate(int id,int c_id)
        {
            if ((await CheckUserFormParticipation(id)).Equals(true))
            {
                return PartialView("_UserAlreadyParticipateInForm");
                
            }
            else
            {
                var v = await _ps.FormParticipateAsync(id);
                
                ViewBag.Id = id;
                ViewBag.cid = c_id;
                return View(v);
            }
        }

        [HttpPost]

        public async  Task<ActionResult> FormParticipate(int id, PFormViewModel obj)
        {

            await _ps.FormParticipateAsyncP(id,obj);
            return RedirectToAction("Index");
        }

        public async Task<bool> CheckUserFormParticipation(int id)
        {
            int n=-1;
            var p = await _ps.CheckUserParticipationAsync(id);
            var usid = p.au.Id;
            for(var i = 0; i < p.Fu.Count; i++)
            {
                if (p.Fu[i].QFormID.Equals(id) && p.Fu[i].UserID.Equals(usid))
                {
                   return true;
                }
                
            }
           return false;
           
            
        }

        public ActionResult ResultChart(int qid)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var group = db.Answers.Where(a => a.QuestionID.Equals(qid)).Select(x => new { x.AnswerStatement, x.AnsCount }).AsEnumerable();

            //var group = await _as.ResultChartAsync(qid);
            new Chart(width: 400, height: 200).AddSeries(

                chartType: "column",
                xValue: group, xField: "AnswerStatement",
                yValues: group, yFields: "AnsCount").Write();
            return null;
            //return View(new QuestionAndAnswerViewModel {
            //    Quest = b ,
            //    Answ  = ans

            //});
            // }

        }



        public async Task<ActionResult> PFormResult(int c_id, int id)
        {
            var grp = await _ps.FormResultAsync(c_id, id);

            return View(grp);
        }



    }
    
}