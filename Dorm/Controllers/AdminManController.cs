using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Dorm.Controllers
{
    public class AdminManController : Controller
    {
        private Models.DormDBEntities db = new Models.DormDBEntities();
        // GET: AdminMan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminAdd(Models.AdminInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            //var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.AdminInfo>(sr.ReadToEnd());///前台传来的数据

            
            var adminInfo = db.AdminInfo.Where(p => p.JobNumber == obj.JobNumber).FirstOrDefault();
            Models.AdminInfo adminInfo1 = new Models.AdminInfo();

            if (obj != null && adminInfo!=null &&obj.JobNumber== adminInfo.JobNumber )
            {
                adminInfo.Name = obj.Name;
                adminInfo.JobNumber = obj.JobNumber;
                adminInfo.PhoneNumber = obj.PhoneNumber;
                adminInfo.Remarks = obj.Remarks;
            }
            else
            {
                string psw = SHA256Encrypt(obj.Password);
                adminInfo1.Name = obj.Name;
                adminInfo1.Password = psw;
                adminInfo1.JobNumber = obj.JobNumber;
                adminInfo1.PhoneNumber = obj.PhoneNumber;
                adminInfo1.Remarks = obj.Remarks;
                db.AdminInfo.Add(adminInfo1);
            }
            db.SaveChanges();
            var rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);
            return rst;
        }
        public string GetList()
        {
            var adminInfos1 = (from r in db.AdminInfo
                               orderby r.id descending
                               select new {
                                   Name =r.Name,
                                   JobNumber =r.JobNumber ,
                                   PhoneNumber =r.PhoneNumber })
                                   .Take(10).ToList();

            JavaScriptSerializer jss = new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(adminInfos1).ToString();
            return json;
        }
        public string GetInfo()
        {
            var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            var obj = jss.Deserialize<Models.AdminInfo>(sr.ReadToEnd());///前台传来的数据

            var adminInfos1 = (from r in db.AdminInfo
                               orderby r.id descending
                               select new
                               {
                                   Name = r.Name,
                                   JobNumber = r.JobNumber,
                                   PhoneNumber = r.PhoneNumber
                               })
                       .Take(10).ToList();

            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(adminInfos1).ToString();
            return json;
        }
        public ActionResult DeleteInfo(Models.AdminInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            //var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.AdminInfo>(sr.ReadToEnd());///前台传来的数据
            var rst = Json(new { success = true, message = 0 }, JsonRequestBehavior.AllowGet);
            string psw = SHA256Encrypt(obj.Password);
            var adminInfo = db.AdminInfo.Where(p => p.JobNumber == obj.JobNumber &&p.Password==psw).FirstOrDefault();
            if(adminInfo!=null)
            {
                db.AdminInfo.Remove(adminInfo);
                db.SaveChanges();
                rst = Json(new { success = true, message = 1 }, JsonRequestBehavior.AllowGet);
            }
            return rst;
        }
        public string Search(Models.AdminInfo obj)
        {
            //var sr = new System.IO.StreamReader(Request.InputStream);
            var jss = new JavaScriptSerializer();
            //var obj = jss.Deserialize<Models.AdminInfo>(sr.ReadToEnd());///前台传来的数据

            Models.AdminInfo adminInfos =db.AdminInfo.Where(p => p.JobNumber == obj.JobNumber).FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            String json = jss.Serialize(adminInfos).ToString();
            return json;
        }
        private string SHA256Encrypt(string plainText)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(plainText);
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] result = sha256.ComputeHash(data);
            return Convert.ToBase64String(result);
        }
    }
}