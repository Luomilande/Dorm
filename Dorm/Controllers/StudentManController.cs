using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dorm.Controllers
{
    public class StudentManController : Controller
    {
        private Models.DormDBEntities db = new Models.DormDBEntities();
        // GET: StudentMan
        public ActionResult Index()
        {
            return View();
        }
        public string GetList()
        {
            List<Models.StudentInfo> studentInfos = new List<Models.StudentInfo>(db.StudentInfo.OrderByDescending(x => x.id).Take(10));
            JavaScriptSerializer jss = new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(studentInfos).ToString();
            return json;
        }
        public string Search(Models.StudentInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据

            //var studentInfo = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();
            Models.StudentInfo studentInfos = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(studentInfos).ToString();
            return json;
        }
        public ActionResult DeleteInfo(Models.StudentInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            //var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据
            var studentInfos = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();
            db.StudentInfo.Remove(studentInfos);
            db.SaveChanges();
            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);
            return rst;
        }
        public string GetInfo()
        {
            var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据

            List<Models.StudentInfo> studentInfos = new List<Models.StudentInfo>(db.StudentInfo.Where(p => p.StudentId == obj.StudentId));
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(studentInfos).ToString();
            return json;

        }
        public ActionResult StudentAdd(Models.StudentInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            //var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据

            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);

            try
            {
                var student = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();
                Models.StudentInfo studentInfo = new Models.StudentInfo();
                if (obj != null && student != null && obj.StudentId == student.StudentId)
                {
                    student.Name = obj.Name;
                    student.StudentId = obj.StudentId;
                    student.Class = obj.Class;
                    student.DormId = obj.DormId;
                }
                else
                {
                    studentInfo.Name = obj.Name;
                    studentInfo.StudentId = obj.StudentId;
                    studentInfo.Class = obj.Class;
                    studentInfo.DormId = obj.DormId;
                    db.StudentInfo.Add(studentInfo);
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                rst = Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);
            }

            return rst;
        }
    }
}