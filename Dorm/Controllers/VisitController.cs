using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dorm.Controllers
{
    public class VisitController : Controller
    {
        private Models.DormDBEntities db = new Models.DormDBEntities();
        // GET: Visit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddVisit(Models.VisitInfo obj)
        {
            if(obj!=null)
            {   
                Models.VisitInfo visit = new Models.VisitInfo();

                visit.StudentId = obj.StudentId;
                visit.DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                visit.StudentName = obj.StudentName;
                visit.Remarks = obj.Remarks;
                visit.InOut = obj.InOut;
                db.VisitInfo.Add(visit);
                db.SaveChanges();
            }
            var rst = Json(new { success = true, message = 1}, JsonRequestBehavior.AllowGet);
            return rst;
        }
        public string GetList()
        {
            string datetime = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            List<Models.VisitInfo> visits =new List < Models.VisitInfo >( db.VisitInfo.OrderByDescending(x=>x.id).Take(10));
            JavaScriptSerializer jss = new JavaScriptSerializer();

            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(visits).ToString();
            return json;
        }
        public string Search(Models.VisitInfo obj)
        {
          
            List<Models.VisitInfo> visits = new List<Models.VisitInfo>(db.VisitInfo.Where(s=>s.StudentName==obj.StudentName).OrderByDescending(x => x.DateTime).Take(10));
            JavaScriptSerializer jss = new JavaScriptSerializer();

            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(visits).ToString();
            return json;
        }
    }
}