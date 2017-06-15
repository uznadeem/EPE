using FYP.Models;
using FYP.ViewModel.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FYP.Controllers
{
    [Authorize]
    [Authorize(Roles = "Master")]
    public class MasterController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext(); 
        // GET: Master
        public ActionResult Index()
        {
            var u = User.Identity.Name;
            DashboardViewModel dvm = new DashboardViewModel();
            dvm.appuser = db.Users.Where(p=>p.UserName==u).FirstOrDefault() ;
            return View(dvm);
        }

        public ActionResult WebUser()
        {
            int[] user_count = new int[3];

            var par = db.Users.Where(u=>u.UserRole=="Participant").Count();
            var admin = db.Users.Where(g =>g.UserRole == "Admin").Count();
            var master = db.Users.Where(t=>t.UserRole == "Master").Count();

            user_count[0] = master;
            user_count[1] = admin;
            user_count[2] = par;
            
            new Chart(width: 400, height: 200).AddSeries(

                chartType: "Column",
                xValue: new[] { "Master","Admin","Participant"},
                yValues:user_count).Write();
            return null;


        }
        public ActionResult TotalCommunities()
        {
            
            int pub = db.Communities.Where(u => u.PrivacyID == 1).Count();
            int prv = db.Communities.Where(u => u.PrivacyID == 2).Count();

            
            new Chart(width: 400, height: 200).AddSeries(

                chartType: "Column",
                xValue: new[] { "Public", "Private"},
                yValues: new[] {pub,prv}).Write();
            return null;


        }



    }
}