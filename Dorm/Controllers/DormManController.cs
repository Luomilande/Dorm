using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dorm.Controllers
{
    public class DormManController : Controller
    {
        private Models.DormDBEntities db = new Models.DormDBEntities();
        // GET: DormMan
        public ActionResult Index()
        {
            return View();
        }
        public string GetList()
        {
            List<Models.StudentInfo> studentInfos = new List<Models.StudentInfo>(db.StudentInfo.OrderByDescending(x => x.DormId).Take(10));
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
            List<Models.StudentInfo> studentInfos =new List<Models.StudentInfo>(db.StudentInfo.Where(p => p.DormId == obj.DormId));
                     
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(studentInfos).ToString();
            return json;
        }
        public ActionResult DeleteInfo(Models.StudentInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            //var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据
            var student = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();
            student.DormId = null;
            db.SaveChanges();
            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);
            return rst;
        }
        public string GetInfo(Models.StudentInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据

            Models.StudentInfo studentInfos = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(studentInfos).ToString();
            return json;
        }
        public ActionResult DormAdd(Models.StudentInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            //var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.StudentInfo>(sr.ReadToEnd());///前台传来的数据

            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);

            try
            {
                var student = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();//根据学号，来修改宿舍号
                if (obj != null && student != null)
                {
                    student.DormId = obj.DormId;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                rst = Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);
            }

            return rst;
        }

        public string GetTool(Models.DormInfo obj)
        {
            var jss = new JavaScriptSerializer();
            Models.DormInfo dorm = db.DormInfo.FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(dorm).ToString();
            return json;
        }

        public ActionResult AddTool(Models.DormInfo obj)
        {
            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);
            if (obj.Broom <= obj.Broom_Max && obj.Mop <= obj.Mop_Max && obj.Trash <= obj.Trash_Max && obj.Water <= obj.Water_Max)
            {
                try
                {
                    var dormInfo = db.DormInfo.FirstOrDefault();
                    dormInfo.Broom_Max = obj.Broom_Max;
                    dormInfo.Broom = obj.Broom;
                    dormInfo.Mop_Max = obj.Mop_Max;
                    dormInfo.Mop = obj.Mop;
                    dormInfo.Trash_Max = obj.Trash_Max;
                    dormInfo.Trash = obj.Trash;
                    dormInfo.Water_Max = obj.Water_Max;
                    dormInfo.Water = obj.Water;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    rst = Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);
                }
            }else
            {
                rst = Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);
            }
            return rst;
        }
        public ActionResult AddNotice(Models.NoticeInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            //var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.NoticeInfo>(sr.ReadToEnd());///前台传来的数据

            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);

            
            try
            {
                Models.NoticeInfo noticeInfo = new Models.NoticeInfo();
                noticeInfo.Notice = obj.Notice;
                noticeInfo.Title = obj.Title;
                noticeInfo.Name = obj.Name;
                noticeInfo.DateTime = DateTime.Now.ToString();
                db.NoticeInfo.Add(noticeInfo);
                db.SaveChanges();
            }
            catch (Exception)
            {
                rst = Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);
            }

            return rst;
        }
        public string GetListNotice()
        {
            List<Models.NoticeInfo> noticeInfos = new List<Models.NoticeInfo>(db.NoticeInfo.OrderByDescending(x => x.DateTime).Take(3));
            JavaScriptSerializer jss = new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(noticeInfos).ToString();
            return json;
        }
        public string SearchTool(Models.NoticeInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.NoticeInfo>(sr.ReadToEnd());///前台传来的数据

            //var studentInfo = db.StudentInfo.Where(p => p.StudentId == obj.StudentId).FirstOrDefault();
            //List<Models.NoticeInfo> noticeInfos = new List<Models.NoticeInfo>(db.NoticeInfo.Where(p => p.Title == obj.Title));
            var noticeInfos = (from a in db.NoticeInfo
                       where (a.Title.IndexOf(obj.Title) >= 0)
                       select a).ToList();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(noticeInfos).ToString();
            return json;
        }
    }
}