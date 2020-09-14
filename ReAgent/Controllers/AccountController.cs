using System;
using System.Web.Mvc;
using System.Web.Security;
using ReAgent.Services.API;
using ReAgent.App_Start;
using ReAgent.Models.BO;
using ReAgent.Models.Enums;
using ReAgent.Models.Views.Account;

namespace ReAgent.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountServ accountServ;
        private readonly IOrderServ orderServ;
        private readonly IMailer mailer;
        public AccountController(IAccountServ accountServ, IOrderServ orderServ, IMailer mailer) : base(accountServ)
        {
            this.accountServ = accountServ;
            this.orderServ = orderServ;
            this.mailer = mailer;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            UserBO user = accountServ.Login(model.Email, model.Password, out string errorMsg);

            if (user == null)
            {
                ModelState.AddModelError("", errorMsg ?? "Имя пользователя или пароль указаны неверно.");
                return View(model);
            }

            if (user.Role == UserRole.Admin)
            {
                SetNotify(orderServ.GetCountNewOrders());
                NotifyManager.Instance.SyncDate = DateTime.Now;
            }

            System.Web.Helpers.AntiForgery.Validate();
            FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
            return RedirectToLocal();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToLocal();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                if (accountServ.GetUserByEmail(model.Email) != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return View(model);
                }

                Guid verificationToken = accountServ.Register(MappingProfilesConfig.Mapper.Map<UserBO>(model));
                mailer.SendEmailVerification(model.Email, verificationToken).SendAsync();

                TempData["Message"] = "Регистрация успешно завершена";
                return RedirectToLocal();
            }
            else
                return View();
        }

        [AllowAnonymous]
        public ActionResult VerifyUser(Guid? token)
        {
            if (token.HasValue)
            {
                Guid? userId = accountServ.VerifyUser(token.Value);
                if (userId.HasValue)
                {
                    FormsAuthentication.SetAuthCookie(userId.Value.ToString(), true);
                    TempData["Message"] = "E-mail был верифицирован.";
                    return RedirectToLocal();
                }
                else
                {
                    ViewBag.StatusMessage = "Токен уже был использован или не существует.";
                }
            }
            else
            {
                ViewBag.StatusMessage = "Что-то пошло не так...";
            }

            return View();
        }

        public ActionResult ConfirmRegistration(Guid id)
        {
            if (!accountServ.ConfirmRegistration(id))
            {
                ModelState.AddModelError("", "Пользователь с таким идентификатором не существует");
                return RedirectToLocal();
            }

            return RedirectToLocal();
        }

        public ActionResult UserEdit()
        {
            ViewBag.Message = TempData["Message"];

            UserBO user = accountServ.GetUserById(Id);
            UserVM vm = MappingProfilesConfig.Mapper.Map<UserVM>(user);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                bool result = accountServ.UserEdit(MappingProfilesConfig.Mapper.Map<UserBO>(vm), out string errorMsg);
                if (!result)
                {
                    TempData["Message"] = errorMsg ?? "Что-то пошло не так...";
                }
                else
                {
                    TempData["Message"] = "Данные были изменены.";
                    return RedirectToAction("UserEdit");
                }
            }

            return View(vm);
        }

        public ActionResult RemoveUser(Guid id)
        {
            if (!accountServ.RemoveUser(id))
            {
                ModelState.AddModelError("", "Пользователь с таким идентификатором не существует");
                return RedirectToLocal();
            }

            return RedirectToLocal();
        }

        #region Вспомогательные методы

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Полный список кодов состояния см. по адресу http://go.microsoft.com/fwlink/?LinkID=177550
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Имя пользователя уже существует. Введите другое имя пользователя.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Имя пользователя для данного адреса электронной почты уже существует. Введите другой адрес электронной почты.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Указан недопустимый пароль. Введите допустимое значение пароля.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Указан недопустимый адрес электронной почты. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Указан недопустимый ответ на вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Указан недопустимый вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Указано недопустимое имя пользователя. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.ProviderError:
                    return "Поставщик проверки подлинности вернул ошибку. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                case MembershipCreateStatus.UserRejected:
                    return "Запрос создания пользователя был отменен. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                default:
                    return "Произошла неизвестная ошибка. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";
            }
        }

        #endregion
    }
}
