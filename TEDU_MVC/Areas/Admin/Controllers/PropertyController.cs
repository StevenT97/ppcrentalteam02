using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.FrameWork;
using System.IO;

namespace TEDU_MVC.Areas.Admin.Controllers
{
    public class PropertyController : BaseController
    {

        // GET: Admin/Property
        List<SelectListItem> propertytype;
        DemoPPCRentalEntities model = new DemoPPCRentalEntities();
        public ActionResult Index(int page=1, int pageSize = 5)
        {
            var propertymodel = new AccountModel();
            var model = propertymodel.ListAllPaging(page,pageSize);
            return View(model);

           
        }

        // GET: Admin/Property/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Property/Create
        public ActionResult Create()
        {
           
            ListAll();
            return View();
        }

        // POST: Admin/Property/Create
        [HttpPost]
        public ActionResult Create(PROPERTy property)
        {
            ListAll();
            try
            {
                // Images
                string filename = Path.GetFileNameWithoutExtension(property.ImageFile.FileName);
                string extension = Path.GetExtension(property.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                property.Images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images"), filename);
                // Avatar
                //string filename2 = Path.GetFileNameWithoutExtension(property.ImageFile2.FileName);
                //string extension2 = Path.GetExtension(property.ImageFile2.FileName);
                //filename2 = filename2 + "Avatar" + DateTime.Now.ToString("yymmssfff") + extension2;
                //property.Avatar = "~/Images/" + filename2;
                //filename2 = Path.Combine(Server.MapPath("~/Images"), filename2);
                // Save
                //property.ImageFile2.SaveAs(filename2);
                //property.ImageFile.SaveAs(filename);

                property.Created_at = DateTime.Parse(DateTime.Now.ToShortDateString());
                if (Path.GetFileNameWithoutExtension(property.ImageFile.FileName) == null)
                {
                    string s2 = "~/Images/ImagesNull.png";
                    property.ImageFile.SaveAs(s2);
                    //property.ImageFile2.SaveAs(filename2);
                }
                else
                {
                    //property.ImageFile2.SaveAs(filename2);
                    property.ImageFile.SaveAs(filename);
                }



                if (ModelState.IsValid)
                {
                    var model = new AccountModel();
                    long id = model.InsertProperty(property);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Property");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create Property khong thanh cong");

                    }

                }

            }
            catch (NullReferenceException)
            {

                property.Created_at = DateTime.Parse(DateTime.Now.ToShortDateString());

                ListAll();

                if (ModelState.IsValid)
                {
                    var model = new AccountModel();
                    long id = model.InsertProperty(property);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Property");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Create Property khong thanh cong");

                    }

                }
            }
           

            return View();
        }

        // GET: Admin/Property/Edit/5
        public ActionResult Edit(int id)
        {
            var property = new AccountModel().ViewDetail(id);
            ListAll();

            return View(property);
        }

        // POST: Admin/Property/Edit/5
        [HttpPost]
        public ActionResult Edit(PROPERTy property)
        {
            ListAll();
            // Images
            try
            {

                string filename = Path.GetFileNameWithoutExtension(property.ImageFile.FileName);
                string extension = Path.GetExtension(property.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                property.Images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images"), filename);
                // Avatar
                //string filename2 = Path.GetFileNameWithoutExtension(property.ImageFile2.FileName);
                //string extension2 = Path.GetExtension(property.ImageFile2.FileName);
                //filename2 = filename2 + "Avatar" + DateTime.Now.ToString("yymmssfff") + extension2;
                //property.Avatar = "~/Images/" + filename2;
                //filename2 = Path.Combine(Server.MapPath("~/Images"), filename2);
                // Save
              
                //if (Path.GetFileNameWithoutExtension(property.ImageFile2.FileName)==null)
                //{
                //    string s1 = "~/ Images / AvatarNull.png";
                //    property.ImageFile2.SaveAs(s1);
                //    property.ImageFile.SaveAs(filename);
                //}
                 if (Path.GetFileNameWithoutExtension(property.ImageFile.FileName)==null)
                {
                    string s2="~/Images/ImagesNull.png";
                    property.ImageFile.SaveAs(s2);
                    //property.ImageFile2.SaveAs(filename2);
                }
                else
                {
                    //property.ImageFile2.SaveAs(filename2);
                    property.ImageFile.SaveAs(filename);
                }
               
              

                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var model = new AccountModel();
                    var res = model.Update(property);
                    if (res)
                    {
                        return RedirectToAction("Index", "Property");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update không thành công");
                    }
                }

            }
            catch
            {
                if (ModelState.IsValid)
                {
                    
                    var model = new AccountModel();
                    var res = model.Update(property);
                    if (res)
                    {
                        return RedirectToAction("Index", "Property");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update không thành công");
                    }
                }
            }
            
                return View("Index");
            
           // return View("Index");
        }

        // GET: Admin/Property/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Admin/Property/Delete/5
        //[HttpPost]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new AccountModel().Delete(id);

           // return View("Index");
            return RedirectToAction("Index", "Property");
            
            //return View();

        }
       
        public void ListAll()
        {
            ViewBag.property_type = model.PROPERTY_TYPE.ToList();
            ViewBag.street = model.STREETs.OrderBy(x=>x.StreetName).ToList();
            ViewBag.ward = model.WARDs.OrderBy(x => x.WardName).ToList();
            ViewBag.district = model.DISTRICT_Table.OrderBy(x => x.DistrictName).ToList();
            ViewBag.user = model.USERs.OrderBy(x => x.FullName).ToList();
            ViewBag.status = model.PROJECT_STATUS.OrderBy(x => x.Status_Name).ToList();
            //ViewBag.sale = model.Sla.ToList();

        }
    }
}
