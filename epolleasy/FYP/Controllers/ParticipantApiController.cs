using FYP.Models;
using FYP.Services;
using FYP.ViewModel;
using FYP.ViewModel.ParticipantApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FYP.Controllers
{
    public class ParticipantApiController : ApiController
    {
        private readonly ParticipantService _ps;

        public ParticipantApiController() : this(new ParticipantService()) { }

        public ParticipantApiController(ParticipantService ParticipantSer)
        {

            _ps = ParticipantSer;

        }
        [HttpGet]
        [ActionName("ParticipantIndex")]
        public async Task<ProfileDataViewModel> ParticipantIndex()
        {
            return await _ps.GetParticipantPanelAsync();
        }


        [HttpGet]
        //[ActionName("SearchCommunity")]
        [Route("api/ParticipantApi/SearchCommunity/{searchString}")]
        public async Task<IList<Community>> SearchCommunity(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {

                var communityQuery = await _ps.SearchCommunityAsync(searchString);
                //ViewBag.Membership = IsMember(UseriD, ViewBag.communityQuery).ToList();


                return communityQuery;

            }

            return null;

        }


        [HttpPost]
        //[ActionName("joinCommunity")]
        [Route("api/ParticipantApi/joinCommunity/{CommID}")]
        public async Task joinCommunity(int CommID)
        {
            
            await _ps.joinCommunityAsync(CommID);

        }


        [HttpPost]
        //[ActionName("leaveCommunity")]
        [Route("api/ParticipantApi/leaveCommunity/{CommID}")]
        public async Task leaveCommunity(int CommID)
        {

            await _ps.leaveCommunityAsync(CommID);

        }

        [HttpGet]
        //[ActionName("Community")]
        [Route("api/ParticipantApi/Community/{id}")]
        public async Task<CommunityMemberViewModel> Community(int id)
        {

            var cm = await _ps.CommunityAsync(id);

            return cm;

        }

        [HttpGet]
        //[ActionName("ParticipantViewDetail")]
        [Route("api/ParticipantApi/ParticipantViewDetail/{id}")]
        public async Task<ViewDetailViewModel> ParticipantViewDetail(int id)
        {

            var da = await _ps.ParticipantViewDetailAsync(id);
            return da;

        }

        //to check whether user participate in particular form or not//
        [HttpGet]
        //[ActionName("CheckUserFormParticipation")]
        [Route("api/ParticipantApi/CheckUserFormParticipation/{id}")]
        public async Task<bool> CheckUserFormParticipation(int id)
        {
            int n = -1;
            var p = await _ps.CheckUserParticipationAsync();

            for (var i = 0; i < p.Fu.Count; i++)
            {
                if (p.Fu[i].QFormID.Equals(id) && p.Fu[i].UserID.Equals(p.au.Id))
                {
                    return true;
                }

            }
            return false;
            
        }

        [HttpGet]
        //[ActionName("FormParticipate")]
        [Route("api/ParticipantApi/FormParticipate/{fpvm}")]
        public async Task<PApiFormParticipateViewModel> FormParticipate(PApiFormParticipateViewModel fpvm)
        {
            int fid = fpvm.fid;
            int cid = fpvm.cid;
            if ((await CheckUserFormParticipation(fid)).Equals(true))
            {
                return null;

            }
            else
            {
                var v = await _ps.FormParticipateAsync(fid);

                fpvm.pfm = v;

                return fpvm;
            }
        }

        [HttpPost]
        //[ActionName("PPFormParticipate")]
        [Route("api/ParticipantApi/PPFormParticipate/{pafvm}")]
        public async Task PPFormParticipate(PApiFormParticipateViewModel pafvm)
        {
            PFormViewModel obj = pafvm.pfm;
            int fid = pafvm.fid;

            await _ps.FormParticipateAsyncP(fid, obj);
        }
        

    }
}