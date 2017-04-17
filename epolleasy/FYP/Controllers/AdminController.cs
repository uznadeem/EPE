
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
            //              member = u.UserID.Equals(abc.CommunityID).ToString(),
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
        public ActionResult Delete(int id,string url)
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
            ViewBag.Id = id;
            ViewBag.AppUser = new MultiSelectList(db.Users.Where(u => u.UserRole.Equals("Participant")), "ID", "UserName");
            return View(v);
        }

        [HttpPost]

        public ActionResult ViewDetails( int id, string[] AppUser)
        {
            CommunityUser cu = new CommunityUser();

            if (AppUser != null)
            {
                foreach (var item in AppUser)
                {

                    cu.UserID = item;
                    cu.CommunityID = id;
                    db.CommunityUsers.Add(cu);
                    db.SaveChanges();
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
        public ActionResult AddForm(int id,QForm qform)
        {
            
            
            qform.FormOwner = User.Identity.Name;
            qform.Creation_Time = System.DateTime.Now;
            db.QForms.Add(qform);
            
            db.SaveChanges();

            //form community data_entry//
            FormCommunity fcom = new FormCommunity();
            fcom.CommunityID = id;
            fcom.QFormID = qform.QFormID;
            db.FormsCommunity.Add(fcom);

            db.SaveChanges();

            var qformholder = qform.QFormID;

            return RedirectToAction("AddQuestion", new {qformholder = qformholder});

        }

        public ActionResult AddQuestion(int qformholder)
        {
           
            ViewBag.qf_id = qformholder;
             
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(int qformholder, QuestionAnswerViewModel abc)
        {

            abc.Ques.QFormID = qformholder;

            db.Questions.Add(abc.Ques);

            db.SaveChanges();

            return RedirectToAction("AddQuestion", new {qformid = qformholder });
        }



    }
}