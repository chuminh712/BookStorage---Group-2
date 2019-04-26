using BookStorage.Common;
using BookStorage.Models;
using BookStorage.ViewModels;
using System.Web.Mvc;
using WebThuVien.Common;

namespace BookStorage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new User();
                var result = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                if (result == 3)
                {
                    var user = dao.GetByID(model.Username);
                    var userSession = new UserLogin();
                    userSession.Username = user.Username;
                    userSession.UserID = user.ID;
                    Session.Add(Constants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == 1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa.");
                }
                else if (result == 2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return View("Index");
        }
    }
}