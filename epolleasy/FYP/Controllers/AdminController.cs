
using FYP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;
using FYP.ViewModel;
using System.Threading.Tasks;
using FYP.Services;
using System.Web.Helpers;

namespace FYP.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _as;

        public AdminController() : this(new AdminService()) { }

        public AdminController(AdminService AdminS)
        {

            _as = AdminS;

        }

        public async Task<ActionResult> Index()
        {
            var a = await _as.GetCommunitiesAsync();


            return View(a);
        }


        public async Task<ActionResult> Create()
        {
            //var a = await _as.CreateCommunityAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Community objcom, HttpPostedFileBase file)
        {
            await _as.CreateCommunityPAsync(objcom, file);
            return RedirectToAction("Index", "Admin");
        }


        public async Task<ActionResult> ViewDetails(int id)
        {

            var b = await _as.ViewDetailAsync(id);

            ViewBag.Id = id;
            ViewBag.AppUser = new MultiSelectList(b.Au, "ID", "UserName");

            return View(b);
        }

        [HttpPost]

        public async Task<ActionResult> ViewDetails(int id, string[] AppUser)
        {

            if (AppUser != null)
            {
                await _as.ViewDetailsPAsync(id, AppUser);
            }

            return RedirectToAction("ViewDetails", new { id = id });
        }

        [HttpGet]
        public ActionResult AddForm(int id)
        {
            ViewBag.id = id;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AddForm(int c_id, QForm qform)
        {


            var p = await _as.AddFormAsync(c_id, qform);


            return RedirectToAction("AddQuestion", new { c_id = c_id, qf_id = p });

        }

        public ActionResult AddQuestion(int c_id, int qf_id)
        {

            ViewBag.qf_id = qf_id;

            ViewBag.c_id = c_id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddQuestion(int c_id, int qf_id, Question q_obj, string[] Multiple_Answer)
        {


            if (Multiple_Answer == null || q_obj == null)
            {

                ModelState.AddModelError("", "Fields are missing ");
                return View(q_obj);

            }

            await _as.AddQuestionAsync(c_id, qf_id, q_obj, Multiple_Answer);

            return RedirectToAction("AddQuestion", new { c_id = c_id, qf_id = qf_id });

        }

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var v = db.Communities.Where(a => a.CommunityID == id).FirstOrDefault();
        //    return View(v);
        //}

        //[HttpPost]
        //public ActionResult Edit(Community objcom)
        //{
        //    bool status = false;


        //    var v = db.Communities.Where(a => a.CommunityID == objcom.CommunityID).FirstOrDefault();
        //    if (v != null)
        //    {
        //        v.CommunityName = objcom.CommunityName;
        //        v.CommunityAbout = objcom.CommunityAbout;
        //        db.SaveChanges();
        //    }


        //    status = true;
        //    return new JsonResult { Data = new { status = status } };
        //}


        //[HttpGet]
        //public ActionResult Delete(int id)
        //{

        //    var v = db.Communities.Where(a => a.CommunityID == id).FirstOrDefault();

        //    if (v != null)
        //    {
        //        return View(v);
        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }

        //}
        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult Delete(int id, string url)
        //{
        //    bool status = false;

        //    var v = db.Communities.Where(a => a.CommunityID == id).FirstOrDefault();
        //    if (v != null)
        //    {
        //        db.Communities.Remove(v);
        //        db.SaveChanges();
        //        status = true;
        //    }

        //    return new JsonResult { Data = new { status = status } };
        //}

        ////public ActionResult SearchAdminCommunities(string cname)
        ////{
        ////    var v = db.Users.Where(u => u.UserName.Equals(User.Identity.Name)).FirstOrDefault();
        ////    //var a = (from c in db.Communities where ((c.CommunityAdmin == User.Identity.Name) && (c.CommunityName == cname)) select c.CommunityName).ToList();

        ////    return View(new CommunityUserViewModel {
        ////        appuser = v,
        ////        Com = db.Communities.Where(u => u.CommunityName.Equals(cname) && u.CommunityAdmin.Equals(User.Identity.Name)).ToList()
        ////        //Com = (from c in db.Communities where ((c.CommunityAdmin == User.Identity.Name) && (c.CommunityName == name)) select c.CommunityName).ToList()

        ////    });
        ////}







        public ActionResult ResultChart(int qid)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //var c = db.Questions.Where(u => u.QFormID.Equals(id)).FirstOrDefault();

            //var b = db.Questions.Where(u => u.QFormID.Equals(id)).ToList();

            //var ans = db.Answers.ToList();

            //var ans = db.Questions.Where(a=>a.QFormID);

           
                var group = db.Answers.Where(a => a.QuestionID.Equals(qid)).Select(x => new { x.AnswerStatement, x.AnsCount }).AsEnumerable();
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





        public ActionResult FormResult(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var b = db.Questions.Where(u => u.QFormID.Equals(id)).ToList();


            return View(new PFormViewModel {
                Ques = b
            });
        }






    }
}
