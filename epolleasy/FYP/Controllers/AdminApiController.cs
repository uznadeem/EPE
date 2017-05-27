using FYP.Models;
using FYP.Services;
using FYP.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FYP.Controllers
{
    public class AdminApiController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        private readonly AdminService _as;

        public AdminApiController() : this(new AdminService()) { }

        public AdminApiController(AdminService AdminS)
        {

            _as = AdminS;

        }



        public async Task<CommunityUserViewModel> GetAdminIndex()
        {
            var adin = await _as.GetCommunitiesAsync();
            return adin;
        }


        public async Task<Community> GetEditCommunity(int id)
        {
            var com = await _as.EditCommunity(id);
            return com;
        }


        public async Task<string> PutEditCommunity(Community community)
        {
            await _as.PEditCommunity(community);
            return "edited";
        }

        public async Task<string> DeleteCommunity(Community com)
        {
            if (com == null)
            {
                return "empty";
            }
            await _as.DeleteCommunity(com);
            return "deleted";
        }

        //[Route("CommunityAsync")]
        //public async Task<IHttpActionResult> PostCommunityAsync(JObject json_obj)
        //{
        //    dynamic jsondata = json_obj;

        //    // dynamic jb = JsonConvert.DeserializeObject<dynamic>(jsondata);

        //    Community com = jsondata.ToObject<Community>();

        //    HttpPostedFileBase file = null;

        //    await _as.CreateCommunityPAsync(com,file);

        //    return Ok();

        //}

        //[System.Web.Http.Route("GetViewDetailAsync")]
        //public async Task<ViewDetailViewModel>GetViewDetailsAsync(int id)
        //{
        //    var abc = await _as.ViewDetailAsync(id);

        //    var ab = new MultiSelectList(abc.Au, "ID", "UserName");

        //    return abc;
        //}
        //[Route("AddForm")]
        //public async Task<string> PostAddForm(JObject afv)
        //{
        //    dynamic jsondata = afv;
        //    QForm qf = jsondata.ToObject<QForm>();
        //    string cid = jsondata.ToString(jsondata.cid);
        //    await _as.CreateCommunityAsync();


        //    return "ali";

        //}



    }
}
