
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


namespace FYP.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //ViewBag.Name = db.Communities.ToList();
            return View();
        }


        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ShowCommunity()  //Gets the todo Lists.
        {

            var com = db.Communities.Where(u => u.CommunityAdmin.Equals(User.Identity.Name));

            //var user = User.Identity.Name;
            //var abc = db.Communities.Where(a => a.CommunityAdmin == user).FirstOrDefault();


            //var com = from c in db.Communities
            //          join u in db.CommunityUsers
            //          on c.CommunityID equals u.CommunityID
            //          select new
            //          {
            //              member = u.UserID.Where(c.CommunityID.Equals(abc.CommunityID)),
            //              nameCom = c.CommunityName.Equals(abc.CommunityName)
            //          };





            var jsonData = new
            {
                data = com
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Name = new SelectList(db.PrivacyLevels.ToList(), "PrivacyLevelID", "PrivacyLevelID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Community objcom, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                //  string path = Path.Combine(Server.MapPath("~/CommunityImages/"), Path.GetFileName(file.FileName));
                string path = HostingEnvironment.MapPath(Path.Combine("~/CommunityImages/", filename));
                file.SaveAs(path);
                objcom.CommunityLogo = filename;
            }


            objcom.CommunityAdmin = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Communities.Add(objcom);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }

            return View(objcom);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var v = db.Communities.Where(a => a.CommunityID == id).FirstOrDefault();
            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Community objcom)
        {
            bool status = false;
            //if (ModelState.IsValid)
            //{


            //Edit 
            var v = db.Communities.Where(a => a.CommunityID == objcom.CommunityID).FirstOrDefault();
            if (v != null)
            {
                v.CommunityName = objcom.CommunityName;
                v.CommunityAbout = objcom.CommunityAbout;
                db.SaveChanges();
            }
            //}

            status = true;
            return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {

            var v = db.Communities.Where(a => a.CommunityID == id).FirstOrDefault();

            if (v != null)
            {
                return View(v);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int id, string url)
        {
            bool status = false;

            var v = db.Communities.Where(a => a.CommunityID == id).FirstOrDefault();
            if (v != null)
            {
                db.Communities.Remove(v);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult ViewDetails(int id)
        {
            var v = db.Communities.Where(u => u.CommunityID == id).FirstOrDefault();
            ViewBag.ComData = db.Communities.Where(u => u.CommunityID == id).FirstOrDefault();
            var b = db.FormsCommunity.Where(u => u.CommunityID.Equals(id)).ToList();
            ViewBag.Id = id;
            ViewBag.AppUser = new MultiSelectList(db.Users.Where(u => u.UserRole.Equals("Participant")), "ID", "UserName");
            return View(new ViewDetailViewModel
            {
                fcom = b,

                comm = v

            });
        }

        [HttpPost]

        public ActionResult ViewDetails(int id, string[] AppUser)
        {
            CommunityUser cu = new CommunityUser();

            if (AppUser != null)
            {
                foreach (var item in AppUser)
                {
                    var memchk = (from cmu in db.CommunityUsers where cmu.UserID == item && cmu.CommunityID == id select cmu.ID).ToList();

                    if (memchk.Count == 0)
                    {

                        cu.UserID = item;
                        cu.CommunityID = id;
                        db.CommunityUsers.Add(cu);
                        db.SaveChanges();

                    }
                    else
                    {

                    }

                }
            }

            return RedirectToAction("ViewDetails", new { id = id });
        }


        public ActionResult AddForm(int id)
        {
            ViewBag.id = id;
            // var getrecdate = System.DateTime.Now;
            return View();
        }
        [HttpPost]
        public ActionResult AddForm(int c_id, QForm qform)
        {


            qform.FormOwner = User.Identity.Name;
            qform.Creation_Time = System.DateTime.Now;
            db.QForms.Add(qform);

            db.SaveChanges();

            //form community data_entry//
            FormCommunity fcom = new FormCommunity();
            fcom.CommunityID = c_id;
            fcom.QFormID = qform.QFormID;
            db.FormsCommunity.Add(fcom);

            db.SaveChanges();

            var qf_id = qform.QFormID;

            return RedirectToAction("AddQuestion", new { c_id = c_id, qf_id = qf_id });

        }

        public ActionResult AddQuestion(int c_id, int qf_id)
        {

            ViewBag.qf_id = qf_id;

            ViewBag.c_id = c_id;
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(int c_id, int qf_id, Question q_obj /*, string[] Multiple_Question*/, string[] Multiple_Answer)
        {


            Answer ans_obj = new Answer();
            int a = 1;

            if (Multiple_Answer == null || q_obj == null)
            {

                ModelState.AddModelError("", "Fields are missing ");
                return View(q_obj);

            }
            q_obj.QFormID = qf_id;
            db.Questions.Add(q_obj);

            foreach (var v in Multiple_Answer)
            {
                ans_obj.AnswerStatement = v;

                ans_obj.OptionNo = a;

                ans_obj.QuestionID = q_obj.QuestionID;
                db.Answers.Add(ans_obj);

                db.SaveChanges();

                a++;


            }

            return RedirectToAction("AddQuestion", new { c_id = c_id, qf_id = qf_id });

        }


        //public ActionResult Publish(int c_id)
        //{



        //    return RedirectToAction("ViewDetail",);
        //}



    }
}