using FYP.Models;
using FYP.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace FYP.Services
{
    public class AdminService
    {
        private readonly ApplicationDbContext _db;

        public AdminService(ApplicationDbContext context)
        {

            _db = context;
            
            
         }
        public AdminService():this(new ApplicationDbContext())
        {

        }


        public async Task<CommunityUserViewModel> GetCommunitiesAsync()
        {

            var value = HttpContext.Current.User.Identity.Name;

            CommunityUserViewModel abc = new CommunityUserViewModel();

            abc.Com = await _db.Communities.Where(c => c.CommunityAdmin == value).ToListAsync();

            abc.appuser = await _db.Users.Where(u => u.UserName.Equals(value)).FirstOrDefaultAsync();

            abc.fcom = await _db.FormsCommunity.Where(u => u.QForms.FormOwner.Equals(value)).ToListAsync();


            abc.qf = await _db.QForms.Where(u => u.FormOwner.Equals(value)).ToListAsync();

            return abc;
        }


        public async Task<IList> CreateCommunityAsync()
        {
            var b = await _db.PrivacyLevels.ToArrayAsync();
            return b;
            
        }

        public async Task CreateCommunityPAsync(Community objcom, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                //  string path = Path.Combine(Server.MapPath("~/CommunityImages/"), Path.GetFileName(file.FileName));
                string path = HostingEnvironment.MapPath(Path.Combine("~/CommunityImages/", filename));
                file.SaveAs(path);
                objcom.CommunityLogo = filename;
            }


                 objcom.CommunityAdmin = HttpContext.Current.User.Identity.Name;

                 _db.Communities.Add(objcom);
                
                 await _db.SaveChangesAsync();
            //if (ModelState.IsValid)
            //{

            //return RedirectToAction("Index", "Admin");
            //}


        }


        public async Task<ViewDetailViewModel> ViewDetailAsync(int id)
        {

            var v= await _db.Communities.Where(u => u.CommunityID == id).FirstOrDefaultAsync();

            var fc = await _db.FormsCommunity.Where(u => u.CommunityID.Equals(id)).ToListAsync();
            
            var cu = await _db.CommunityUsers.Where(u => u.CommunityID.Equals(id)).ToListAsync();

            var au = await _db.Users.Where(u => u.UserRole.Equals("Participant")).ToListAsync();

            //var b = new MultiSelectList(au, "ID", "UserName");

            ViewDetailViewModel vd = new ViewDetailViewModel();

            vd.comm = v;

            vd.fcom = fc;

            vd.c_usr = cu;

            vd.Au = au;

            return vd;
        }

        public async Task ViewDetailsPAsync(int id , string[] AppUser)
        {
            CommunityUser cu = new CommunityUser();
            foreach (var item in AppUser)
            {
                var memchk =  await (from cmu in  _db.CommunityUsers where cmu.UserID == item && cmu.CommunityID == id select cmu.ID).ToListAsync();

                if (memchk.Count == 0)
                {

                    cu.UserID = item;
                    cu.CommunityID = id;
                    _db.CommunityUsers.Add(cu);
                    await _db.SaveChangesAsync();

                }
                else
                {

                }

            }


        }
        
        
        public async Task<int> AddFormAsync(int c_id, QForm qform,int ft)
        {

            qform.FormOwner = HttpContext.Current.User.Identity.Name;
            qform.Creation_Time = System.DateTime.Now;
            qform.FormType = ft;
            _db.QForms.Add(qform);

            await _db.SaveChangesAsync();

            //form community data_entry//

            FormCommunity fcom = new FormCommunity();

            fcom.CommunityID = c_id;
            fcom.QFormID = qform.QFormID;
            _db.FormsCommunity.Add(fcom);

            await _db.SaveChangesAsync();

            var qf_id = qform.QFormID;

            return qf_id;


        }


        public async Task<FormResultViewModel> FormResultAsync(int c_id, int id)
        {

            var qu = await _db.Questions.Where(u => u.QFormID.Equals(id)).ToListAsync();
            var ft = await (from a in _db.QForms where a.QFormID == id select a.FormType).FirstOrDefaultAsync();
            FormResultViewModel fr = new FormResultViewModel();

            fr.Ques = qu;
            fr.qf_id = id;
            fr.comid = c_id;
            fr.ft = ft;
            return fr;

        }


        //public async Task<> ResultChartAsync(int qid)
        //{

        //    var group = await _db.Answers.Where(a => a.QuestionID.Equals(qid)).Select(x => new { x.AnswerStatement, x.AnsCount }).FirstOrDefaultAsync;
        //    return group;
        //}



        public async Task AddQuestionAsync(int c_id, int qf_id, Question q_obj, string[] Multiple_Answer)
        {
            Answer ans_obj = new Answer();

            int a = 1;

             q_obj.QFormID = qf_id;
            _db.Questions.Add(q_obj);

            foreach (var v in Multiple_Answer)
            {
                ans_obj.AnswerStatement = v;

                ans_obj.OptionNo = a;

                ans_obj.QuestionID = q_obj.QuestionID;
                _db.Answers.Add(ans_obj);

                await _db.SaveChangesAsync();

                a++;


            }


        }
        public async Task FrequencyQuestionAsync(int c_id, int qf_id, Question q_obj)
        {
            Answer ans_obj = new Answer();

            string[] fre = new string[4];

            fre[0] = "agree";
            fre[1] = "Strongly agree";
            fre[2] = "Disagree";
            fre[3] = "Strongly disagree";
            
            int a = 1;

            q_obj.QFormID = qf_id;
            _db.Questions.Add(q_obj);


            foreach (var v in fre )
            {

                ans_obj.AnswerStatement = v;

                ans_obj.OptionNo = a;

                ans_obj.QuestionID = q_obj.QuestionID;
                _db.Answers.Add(ans_obj);

                await _db.SaveChangesAsync();

                a++;
            }

          }

        }
    }
