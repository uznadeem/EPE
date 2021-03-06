﻿using FYP.Models;
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

            var a = System.DateTime.Now;

            CommunityUserViewModel abc = new CommunityUserViewModel();

            abc.Com = await _db.Communities.Where(c => c.CommunityAdmin == value).ToListAsync();

            abc.appuser = await _db.Users.Where(u => u.UserName.Equals(value)).FirstOrDefaultAsync();

            //abc.fcom = await _db.FormsCommunity.Where(u => u.QForms.FormOwner.Equals(value)).ToListAsync();
             var active_fom = await _db.FormsCommunity.Where(u => u.QForms.FormOwner.Equals(value) && u.QForms.Expiry_Time > a).ToListAsync();

            var sealed_fom = await _db.FormsCommunity.Where(u => u.QForms.FormOwner.Equals(value) && u.QForms.Expiry_Time < a).ToListAsync();

            int Tform = active_fom.Count + sealed_fom.Count;

            abc.active_fom = active_fom;
            abc.sealed_fom = sealed_fom;
            abc.Tform = Tform;
            //abc.qf = await _db.QForms.Where(u => u.FormOwner.Equals(value)).ToListAsync();

            //abc.activeform = await (from c in _db.QForms where c.FormOwner == value && c.Expiry_Time > a select c).CountAsync();

            //abc.sealedform = await (from c in _db.QForms where c.FormOwner == value && c.Expiry_Time < a select c).CountAsync();


            return abc;
        }

        ///for api create community//
        public async Task CreateCommunity_temp_Async(Community obj_com)
        {

            var value = HttpContext.Current.User.Identity.Name;
            obj_com.CommunityAdmin = value;
            obj_com.CommunityLogo = "comm_default.jpg";
            _db.Communities.Add(obj_com);

            await _db.SaveChangesAsync();
            
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
            else
            {
                objcom.CommunityLogo = "comm_default.jpg";
                
            }


                 objcom.CommunityAdmin = HttpContext.Current.User.Identity.Name;

                 _db.Communities.Add(objcom);
                
                 await _db.SaveChangesAsync();
            
            
        }

        public async Task RemoveMemberAsync(string uid,int cid)
        {

           // var us = await _db.Users.Where(u=>u.UserName.Equals(uname)).FirstOrDefaultAsync();
            var a = await _db.CommunityUsers.Where(u=>u.UserID.Equals(uid)).FirstOrDefaultAsync();
            _db.CommunityUsers.Remove(a);
            await _db.SaveChangesAsync();

            //var fcom = await _db.FormsCommunity.Where(u => u.CommunityID.Equals(cid)).ToArrayAsync();

            //foreach(var c in fcom)
            //{ 
            //    var b = await _db.FormUsers.Where(t=>t.UserID.Equals(uid) && t.QFormID.Equals(c.QFormID)).FirstOrDefaultAsync();
            //    _db.FormUsers.Remove(b);
            //    await _db.SaveChangesAsync();

            //}
        }

        public async Task<Community> CommunitySearchAsync(int id)
        {

            //var us = await _db.Users.Where(u => u.UserName == HttpContext.Current.User.Identity.Name).SingleOrDefaultAsync();

            var Comm = await _db.Communities.Where(u => u.CommunityID == id).FirstOrDefaultAsync();


            //var CommV = new CommunityMemberViewModel
            //{

            //    Community = Comm,
            //    //IsMember = await IsMemberAsync(us.Id, Comm.CommunityID)

            //};

            return Comm;

        }

        public async Task<AdminSearchCommunityViewModel> SearchCommunityAsync(string searchString)
        {
            var us = HttpContext.Current.User.Identity.Name;
            var p = await _db.Communities.Include(x => x.CommunityUsers).Where(s => s.CommunityName.Contains(searchString)).ToListAsync();
            var com = await _db.Communities.Where(a=>a.CommunityAdmin.Equals(us)).ToArrayAsync();
            AdminSearchCommunityViewModel adscm = new AdminSearchCommunityViewModel();
            adscm.com = com;
            adscm.searchstr = p;
            adscm.a = 0;
            return adscm;
        }

        public async Task<Community> EditCommunity(int id)
        {

            var v = await _db.Communities.Where(a => a.CommunityID == id).FirstOrDefaultAsync();
            return v;

        }

        public async Task PEditCommunity(Community objcom)
        {

            var v = await _db.Communities.Where(a => a.CommunityID == objcom.CommunityID).FirstOrDefaultAsync();
            if (v != null)
            {
                //v.PrivacyID = objcom.PrivacyID;
                v.CommunityName = objcom.CommunityName;
                v.CommunityAbout = objcom.CommunityAbout;
                await _db.SaveChangesAsync();
            }

        }

        public async Task DeleteCommunity(int id)
        {

            var v = await _db.Communities.Where(a => a.CommunityID == id).FirstOrDefaultAsync();
            var t = await _db.FormsCommunity.Where(a => a.CommunityID == id).ToListAsync();

            var cu = await _db.CommunityUsers.Where(g => g.CommunityID.Equals(id)).ToListAsync();

            if (t.Count != 0)
            {
                for (var i = 0; i < t.Count; i++)
                {
                    var k = t[i].QFormID;
                    var q = await _db.Questions.Where(a => a.QFormID.Equals(k)).ToListAsync();
                    var fu = await _db.FormUsers.Where(a => a.QFormID.Equals(k)).ToListAsync();
                    for (var qc = 0; qc < q.Count; qc++)
                    {
                        var m = q[qc].QuestionID;
                        var ans = await _db.Answers.Where(a => a.QuestionID.Equals(m)).ToListAsync();

                        for (var aw = 0; aw < ans.Count; aw++)
                        {

                            _db.Answers.Remove(ans[aw]);
                        }
                        _db.Questions.Remove(q[qc]);
                    }

                    for (var w = 0; w < fu.Count; w++)
                    {
                        _db.FormUsers.Remove(fu[w]);
                    }
                    var n = t[i].QForms;
                    _db.QForms.Remove(n);
                    _db.FormsCommunity.Remove(t[i]);
                }
            }
            else
            {

            }
            if (v != null)
            {
                if (cu.Count != 0)
                {
                    for (var c = 0; c < cu.Count; c++)
                    {
                        _db.CommunityUsers.Remove(cu[c]);
                    }
                }

                _db.Communities.Remove(v);
            }

            await _db.SaveChangesAsync();
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

            var a = await _db.CommunityUsers.Where(p => p.CommunityID.Equals(c_id)).ToArrayAsync();

            //FormUser fu = new FormUser();


            //form community data_entry//

            FormCommunity fcom = new FormCommunity();

            fcom.CommunityID = c_id;
            fcom.QFormID = qform.QFormID;
            _db.FormsCommunity.Add(fcom);

            await _db.SaveChangesAsync();

            var qf_id = qform.QFormID;

            //foreach (var b in a)
            //{
            //    fu.UserID = b.UserID;
            //    fu.QFormID = qf_id;
            //    _db.FormUsers.Add(fu);
            //    await _db.SaveChangesAsync();
            //}



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
