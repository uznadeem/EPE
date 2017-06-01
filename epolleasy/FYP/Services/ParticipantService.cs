using FYP.Models;
using FYP.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FYP.Services
{
    public class ParticipantService
    {
        private readonly ApplicationDbContext _db;

        public ParticipantService(ApplicationDbContext context)
        {

            _db = context;


        }
        public ParticipantService():this(new ApplicationDbContext())
        {

        }


        public async Task<ProfileDataViewModel> GetParticipantPanelAsync()
        {

            var us = await _db.Users.Where(u => u.UserName == HttpContext.Current.User.Identity.Name).SingleOrDefaultAsync();
            var comlst = await _db.CommunityUsers.Include(x => x.Community).Where(u => u.UserID == us.Id).ToListAsync();

            var a = System.DateTime.Now;
            //var wf = await (from fc in _db.FormsCommunity join cu in _db.CommunityUsers on fc.CommunityID equals cu.CommunityID select new {frm = fc.QForms}).ToListAsync();

            //var fc = await _db.FormsCommunity

            //var pf = await (from c in _db.CommunityUsers where c.UserID.Equals(us.Id) select c.Community).T;

            var qf = await (from cu in _db.CommunityUsers join fc in _db.FormsCommunity on cu.CommunityID equals fc.CommunityID where (cu.UserID.Equals(us.Id)) select fc).ToListAsync();

            ProfileDataViewModel pd = new ProfileDataViewModel();
            pd.UserT = us;
            pd.CommunitiesList = comlst;
            pd.fc = qf;

            pd.activeform = await (from c in _db.FormUsers where c.UserID == us.Id && c.QForm.Expiry_Time > a select c.QForm).CountAsync();

            pd.sealedform = await (from c in _db.FormUsers where c.UserID == us.Id && c.QForm.Expiry_Time < a select c.QForm).CountAsync();


            return pd;

        }

        public async Task<bool> IsMemberAsync(string UseriD, int CommID)
        {

            var p = await _db.CommunityUsers.Where(u => u.UserID == UseriD && u.CommunityID == CommID).FirstOrDefaultAsync();
            if(p!=null)
            {
                return true;

            }
            else
            {
                return false;

            }

            
        }

        public async Task<IList<Community>> SearchCommunityAsync(string searchString)
        {

            var p = await _db.Communities.Include(x => x.CommunityUsers).Where(s => s.CommunityName.Contains(searchString)).ToListAsync();
            return p;
        }


        public async Task joinCommunityAsync(int CommID)
        {

            var UseriD = await _db.Users.Where(u => u.UserName == HttpContext.Current.User.Identity.Name).SingleOrDefaultAsync();

            CommunityUser newUser = new CommunityUser();
            newUser.UserID = UseriD.Id;
            newUser.CommunityID = CommID;
            
            _db.CommunityUsers.Add(newUser);
            await _db.SaveChangesAsync();


        }


        public async Task leaveCommunityAsync(int CommID)
        {

            var UseriD = await _db.Users.Where(u => u.UserName == HttpContext.Current.User.Identity.Name).SingleOrDefaultAsync();


            var communityLeave = await _db.CommunityUsers.Where(u => u.UserID == UseriD.Id && u.CommunityID == CommID).FirstOrDefaultAsync();


            if (communityLeave != null)
            {

                 _db.CommunityUsers.Remove(communityLeave);
                await _db.SaveChangesAsync();

            }


        }
        public async Task<CommunityMemberViewModel> CommunityAsync(int id)
        {

            var us = await _db.Users.Where(u => u.UserName == HttpContext.Current.User.Identity.Name).SingleOrDefaultAsync();

            var Comm = await _db.Communities.Where(u => u.CommunityID == id).FirstOrDefaultAsync();


            var CommV = new CommunityMemberViewModel
            {

                Community = Comm,
                IsMember = await IsMemberAsync(us.Id, Comm.CommunityID)

            };

            return CommV;

        }

        public async Task<ViewDetailViewModel> ParticipantViewDetailAsync(int id)
        {
            var name = HttpContext.Current.User.Identity.Name;
            var us = await _db.Users.Where(u => u.UserName == name).SingleOrDefaultAsync();

            var v = await _db.Communities.Where(u => u.CommunityID == id).FirstOrDefaultAsync();
            var b = await _db.FormsCommunity.Where(u => u.CommunityID.Equals(id)).ToListAsync();
            var cu = await _db.CommunityUsers.Where(u => u.CommunityID.Equals(id)).ToListAsync();

            ViewDetailViewModel vd = new ViewDetailViewModel();
            vd.fcom = b;
            vd.comm = v;
            vd.c_usr = cu;
            vd.IsMember = await IsMemberAsync(us.Id, id);

            return vd;
        }

        public async Task<FormUserViewModel> CheckUserParticipationAsync()
        {
            var fu = await _db.FormUsers.ToListAsync();
            var u = await _db.Users.Where(c => c.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefaultAsync();
            FormUserViewModel vm = new FormUserViewModel();

            vm.Fu = fu;
            vm.au = u;

            return vm;

        }

        public async Task<PFormViewModel> FormParticipateAsync(int id)
        {
            var v = await (_db.Questions.Where(u => u.QFormID.Equals(id)).Distinct()).ToListAsync();

            PFormViewModel pf = new PFormViewModel();

            pf.Ques = v;

            return pf;

        }

        public async Task FormParticipateAsyncP(int id, PFormViewModel obj)
        {
            var v = await _db.Users.Where(u => u.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefaultAsync();

            FormUser fu = new FormUser();

            fu.QFormID = id;
            fu.UserID = v.Id;
            _db.FormUsers.Add(fu);
            await _db.SaveChangesAsync();


            var p = await (_db.Questions.Where(u => u.QFormID.Equals(id)).Distinct()).ToListAsync();

            Question q = new Question();


                for (var i = 0; i < obj.Ques.Count(); i++)
                {
                    int a = obj.Ques[i].SelectedAnswerId;
                    p[i].SelectedAnswerId = a;

                
                    var h = await _db.Answers.Where(u => u.AnswerID.Equals(a)).FirstOrDefaultAsync();
                    h.AnsCount = h.AnsCount + 1;

                    await _db.SaveChangesAsync();

                }
                
            }

        
        }
    }