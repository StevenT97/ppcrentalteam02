using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using TEDU_MVC.Areas.Admin.Code;
using System.Web.Security;
using TEDU_MVC.Code;

namespace TEDU_MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModels model)
        {
            //var result = new AccountModel().Login(model.UserName, model.PassWord);
           
            if (ModelState.IsValid)
            {
                var account = new AccountModel();
                var result = account.Login(model.UserName, MaHoa.MD5Hash(model.PassWord));

                if (result == 1)
                {
                    var user = account.GetID(model.UserName);
                    var userSession = new UserSession();
                    userSession.UserName = user.Email;
                    userSession.UserID = user.id;
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản Đăng Nhập Bị Khóa");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài Khoản Đăng Nhập Không Tồn Tại");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật Khẩu Đăng Nhập Không Đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                }
            }
            
            return View("Index");
        }

        
    }
}