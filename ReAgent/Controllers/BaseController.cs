using ReAgent.Services.API;
using System;
using System.Web;
using System.Web.Mvc;
using ReAgent.Models.Enums;
using ReAgent.Models.Views.Order;

namespace ReAgent.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAccountServ accountServ;

        private Guid id;
        public Guid Id
        {
            get
            {
                if(id == default(Guid))
                {
                    id = Guid.Parse(HttpContext.User.Identity.Name);
                }

                return id;
            }
        }

        private string email;
        public string Email
        {
            get
            {
                if (String.IsNullOrEmpty(email))
                {
                    HttpCookie cookie = HttpContext.Request.Cookies.Get("user_email");
                    email = cookie == null ? accountServ.GetUserById(Id).Email : cookie.Value;
                    HttpContext.Response.SetCookie(new HttpCookie("user_email", email));
                }

                return email;
            }
        }

        public BaseController(IAccountServ accountServ)
        {
            this.accountServ = accountServ;
        }

        protected bool IsAdmin()
        {
            return accountServ.GetUserRole(this.Id) == UserRole.Admin;
        }

        protected void SetNotify(int count)
        {
            TempData["Notify"] = new NotifyVM()
            {
                CountOrders = count
            };
        }
    }
}
