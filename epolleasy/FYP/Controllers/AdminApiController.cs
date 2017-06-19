using FYP.Models;
using FYP.Services;
using FYP.ViewModel;
using FYP.ViewModel.AdminApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace FYP.Controllers
{
    public class AdminApiController : ApiController
    {
 
        private readonly AdminService _as;

        public AdminApiController() : this(new AdminService()) { }

        public AdminApiController(AdminService AdminS)
        {

            _as = AdminS;

        }

        [HttpGet]
        [ActionName("GetAdminIndex")]
        public async Task<CommunityUserViewModel> GetAdminIndex()
        {
            var adin = await _as.GetCommunitiesAsync();
            return adin;
        }

        [HttpGet]
        [ActionName("GetEditCommunity")]
        public async Task<Community> GetEditCommunity(int id)
        {
            var com = await _as.EditCommunity(id);
            return com;
        }
       
        //put
        //api/adminapi/editcommunity
        [HttpPut]
        //[ActionName("EditCommunity")]
        [Route("api/AdminApi/EditCommunity")]
        public async Task<string> EditCommunity(Community community)
        {
            await _as.PEditCommunity(community);
            return "edited";
        }

        [HttpDelete]
        //[ActionName("DeleteCommunity")]
        [Route("api/AdminApi/DeleteCommunity")]
        public async Task<string> DeleteCommunity(Community com)
        {
            var a = com.CommunityID;
            if (com == null)
            {
                return "empty";
            }
            await _as.DeleteCommunity(a);
            return "deleted";
        }

        //[ActionName("AddCommunity")]
        //public async Task<IHttpActionResult> AddCommunity(CreatCommunityViewModel ccvm)
        //{
        //    //dynamic jsondata = json_obj;

        //    // dynamic jb = JsonConvert.DeserializeObject<dynamic>(jsondata);

        //    //Community com = jsondata.ToObject<Community>();

        //    var com = ccvm.com;
        //    //HttpPostAttribute file = ccvm.file;

        //    await _as.CreateCommunityPAsync(com, ccvm.file);

        //    return Ok();

        //}

        [HttpPost]
        //[ActionName("AddCommunity")]
        [Route("api/AdminApi/AddCommunity")]
        public async Task<IHttpActionResult> AddCommunity(Community com)
        {

           
           await _as.CreateCommunity_temp_Async(com);

            return Ok();

        }


        [HttpGet]
       // [ActionName("GetViewDetail")]
        [Route("api/AdminApi/GetViewDetail/{cid}")]
        public async Task<ViewDetailViewModel> GetViewDetail(int cid)
        {
            return await _as.ViewDetailAsync(cid);
            
        }

        //post//
        [HttpPost]
        [Route("api/AdminApi/PViewDetail/{vdvm}")]
        public async Task<string> PViewDetail(ApiViewDetailViewModel vdvm)
        {
            var cid = vdvm.cid;
            int a = vdvm.lid.Count();
            if(a==0)
            {
                return "no member added";
            }
            string[] AppUser = new string[a];
            AppUser = vdvm.lid;
            await _as.ViewDetailsPAsync(cid,AppUser);
            return "Added";//////////////////////member added succesfull//
        }
        

        //[Route("AddForm")]

        //public async Task<string> PostAddForm(JObject j_obj, JObject abc)
        //{
        //    dynamic jsondata = j_obj;
        //    QForm qf = jsondata.ToObject<QForm>();

        //    string cid = jsondata.ToString(jsondata.cid);
        //    await _as.CreateCommunityAsync();


        //    return "ali";

        //}
        //post
        [HttpPost]
        [Route("api/AdminApi/AddFormViewModel/{afvm}")]

        public async Task<int> AddForm(AddFormViewModel afvm)
        {
            //dynamic jsondata = j_obj;
            //QForm qf = jsondata.ToObject<QForm>();

            //string cid = jsondata.ToString(jsondata.cid);
            var cid = afvm.cid;
            QForm qf = afvm.qfom;
            var ft = qf.FormType;

            if (ft == 0)
            {

                return 0;
            }

            return await _as.AddFormAsync(cid,qf,ft);//return qfid
            
        }

        [HttpPost]
        //[ActionName("AddQuestion")]
        [Route("api/AdminApi/AAddQuestionViewModel/{aqvm}")]

        public async Task<bool> AddQuestion(AAddQuestionViewModel aqvm)
        {
            int cid = aqvm.cid;
            int ft = aqvm.ft;
            var ans = aqvm.Mul_ans;
            Question q_obj = aqvm.ques;
            int qfid = aqvm.qfid;
            int a = aqvm.Mul_ans.Count();
            string[] Multiple_Answer = new string[a];
            Multiple_Answer = aqvm.Mul_ans;
            if (ft == 1)
            {

                if (a == 0 || q_obj == null)
                {

                    ModelState.AddModelError("", "Fields are missing ");
                    return false;

                }

                await _as.AddQuestionAsync(cid, qfid, q_obj, Multiple_Answer);
                return true;
            }
            else
            {
                if (q_obj == null)
                {

                    ModelState.AddModelError("", "Fields are missing ");
                    return false;

                }
                await _as.FrequencyQuestionAsync(cid, qfid, q_obj);
                return true;
            }
            
        }

        [HttpGet]
        //[ActionName("FormResult")]
        [Route("api/AdminApi/FormResult/{frvm}")]

        public async Task<FormResultViewModel> FormResult(ApFormResultViewModel frvm)
        {
            var cid = frvm.cid;
            var fid = frvm.fomid;
            return await _as.FormResultAsync(cid, fid);
        }

        [HttpGet]
        //[ActionName("ResultChart")]

        [Route("api/AdminApi/ResultChart/{qid}")]

        public void ResultChart(int qid)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var group = db.Answers.Where(a => a.QuestionID.Equals(qid)).Select(x => new { x.AnswerStatement, x.AnsCount }).AsEnumerable();

            //var group = await _as.ResultChartAsync(qid);
            new Chart(width: 400, height: 200).AddSeries(

                chartType: "column",
                xValue: group, xField: "AnswerStatement",
                yValues: group, yFields: "AnsCount").Write();
            
        }
        
    }
}
