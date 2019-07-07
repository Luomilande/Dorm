using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dorm.Controllers
{
    public class HomeController : Controller
    {
        private Models.DormDBEntities db = new Models.DormDBEntities();

        public ActionResult Index()
        {
            return View();
        }
        public string TestInfo()
        {
            List<Models.DateNumber> dn = new List<Models.DateNumber>();
            
            for (int a = 0; a >= -7; a--)
            {
                Models.DateNumber dateNumber = new Models.DateNumber();
                string datetime = DateTime.Now.AddDays(a).ToString("yyyy-MM-dd");
                dateNumber.DateTime = datetime;
                dateNumber.InNumber  = (int)db.VisitInfo.Where(p => p.InOut == 1 && p.DateTime.StartsWith(datetime)).Count();//获取外出全部访问量
                dateNumber.OutNumber = (int)db.VisitInfo.Where(p => p.InOut == 0 && p.DateTime.StartsWith(datetime)).Count();//获取进入全部访问量
                dn.Add(dateNumber);
                dateNumber = null;
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(dn).ToString();
            return json;
        }
        public string Totaldata()
        {
            Models.TotalData td = new Models.TotalData();
            td.VisitNumber = db.VisitInfo.Count();
            var di = db.DormInfo.FirstOrDefault();
            td.ToolNumber = (int)(di.Mop + di.Water + di.Trash + di.Broom);
            td.LiveNumber = db.StudentInfo.Count();
            td.NoticeNumber = db.NoticeInfo.Count();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(td).ToString();
            return json;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult OutInInput()
        {
            return View();
        }
        public ActionResult Admininfo()
        {
            return View();
        }
        public ActionResult StudentInfo()
        {
            return View();
        }
        public ActionResult DormInfo()
        {
            return View();
        }
        public ActionResult DormMan()
        {
            return View();
        }
    }
}