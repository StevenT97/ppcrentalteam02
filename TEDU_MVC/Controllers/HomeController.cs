using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.FrameWork;
using System.IO;



namespace TEDU_MVC.Controllers
{
    public class HomeController : Controller
    {
        List<SelectListItem> myList, myList2, propertytype, street;
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
        // GET: Home
        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            ListAll();
            var propertymodel = new AccountModel();
            var model = propertymodel.ListAllPaging(page, pageSize);
            return View(model);
        }
        public ActionResult Details(int id)
        {
            db = new DemoPPCRentalEntities();
            var product = db.PROPERTies.FirstOrDefault(x => x.ID == id);
            return View(product);
        }
        public JsonResult GetStreet(int did)
        {
            var db = new DemoPPCRentalEntities();
            var streets = db.STREETs.Where(s => s.District_ID== did);
            return Json(streets.Select(s => new
            {
                id = s.ID,
                text = s.StreetName
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Search(string _area, string text, string _district, string _propertytype)
        {
            ListAll();
            //    var product = model.PROPERTies.ToList().Where(x => (x.PropertyName.Contains(text) && x.Area.Contains(_area) && x.DISTRICT_Table.DistrictName.Contains(_district)));
            var pro = (from a in db.PROPERTies
                       where ((a.PropertyName.Contains(text) && a.Area.Contains(_area) && a.DISTRICT_Table.DistrictName.Contains(_district) && a.PROPERTY_TYPE.CodeType.Contains(_propertytype)))

                       select a);

            return View(pro);
        }

        public void ListAll()
        {
            myList = new List<SelectListItem>();
            myList2 = new List<SelectListItem>();
            propertytype = new List<SelectListItem>();
            street = new List<SelectListItem>();
            var distr = db.DISTRICT_Table.Where(x => x.ID >= 31 && x.ID <= 54);
            var str = db.STREETs.ToList();
            var ward1 = db.WARDs.ToList().OrderBy(x => x.WardName);
            var protype = db.PROPERTY_TYPE.ToList().OrderBy(x => x.CodeType);

            //myList.Add(new SelectListItem { Text = "ALL", Value = "ALL" });
            //myList2.Add(new SelectListItem { Text = "ALL", Value = "ALL" });
            //propertytype.Add(new SelectListItem { Text = "ALL", Value = "ALL" });

            ////////
            foreach (var x in distr)
            {
                myList.Add(new SelectListItem { Text = x.DistrictName, Value = x.DistrictName });
            }
            ViewData["data1"] = myList;
            /////
            foreach (var x in ward1)
            {
                myList2.Add(new SelectListItem { Text = x.WardName, Value = x.WardName });
            }
            ViewData["data2"] = myList2;
            //////
            foreach (var x in protype)
            {
                propertytype.Add(new SelectListItem { Text = x.CodeType, Value = x.CodeType });
            }
            ViewData["data3"] = propertytype;

            foreach (var x in str)
            {
                street.Add(new SelectListItem { Text = x.StreetName, Value = x.StreetName });
            }
            ViewData["data4"] = street;
        }
    }
}