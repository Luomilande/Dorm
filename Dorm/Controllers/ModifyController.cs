using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dorm.Controllers
{
    public class ModifyController : Controller
    {
        private Models.DormDBEntities db = new Models.DormDBEntities();
        // GET: Modify
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admininfo()
        {
            var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            var obj = jss.Deserialize<Models.AdminInfo>(sr.ReadToEnd());///前台传来的数据

            if (obj != null)
            {
                Models.AdminInfo adminInfo = new Models.AdminInfo();
                adminInfo.Name = obj.Name;
                adminInfo.JobNumber = obj.JobNumber;
                db.AdminInfo.Add(adminInfo);
                db.SaveChanges();
            }
            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);
            return rst;
        }
        public ActionResult StudentInfo()
        {
            var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据

            if (obj != null)
            {
                Models.StudentInfo student = new Models.StudentInfo();
                student.Name = obj.Name;
                student.StudentId = obj.StudentId;
                student.Class = obj.Class;
                student.DormId = obj.DormId;

                db.StudentInfo.Add(student);
                db.SaveChanges();
            }
            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);

            return rst;
        }
    }
}