using ReAgent.App_Start;
using ReAgent.Services.API;
using ReAgent.Models.Views;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ReAgent.Models.Enums;
using ReAgent.Models.BO;
using ReAgent.Models.Views.Order;

namespace ReAgent.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderServ orderServ;
        private readonly IAccountServ accountServ;
        private readonly IMailer mailer;

        public OrderController(IOrderServ orderServ, IAccountServ accountServ, IMailer mailer) : base(accountServ)
        {
            this.orderServ = orderServ;
            this.accountServ = accountServ;
            this.mailer = mailer;
        }

        public ActionResult Index()
        {
            bool isAdmin = IsAdmin();
            OrdersVM vm = new OrdersVM
            {
                IsAdmin = isAdmin,
                Orders = isAdmin ? MappingProfilesConfig.Mapper.Map<IEnumerable<OrderVM>>(orderServ.GetAllOrders()) :
                                   MappingProfilesConfig.Mapper.Map<IEnumerable<OrderVM>>(orderServ.GetAllUserOrders(Id))
            };

            return View(vm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateOrderVM model)
        {
            if (ModelState.IsValid)
            {
                int orderId = orderServ.CreateOrder(new OrderBO()
                {
                    Date = DateTime.Now,
                    Status = OrderStatus.New,
                    Details = model.Text,
                    UserId = Id
                });

                mailer.SendEmailOrderChange(Email, orderId, orderServ.GetLocalizedOrderStatusName(OrderStatus.New)).SendAsync();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ChangeStatus(int id, string status)
        {
            if (Enum.TryParse<OrderStatus>(status, out OrderStatus newStatus))
            {
                OrderBO order = orderServ.ChangeStatus(id, newStatus);
                mailer.SendEmailOrderChange(Email, order.Id, orderServ.GetLocalizedOrderStatusName(order.Status)).SendAsync();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Chat(int chatId)
        {
            UserBO user = accountServ.GetUserById(Id);

            ChatVM vm = new ChatVM
            {
                ChatId = chatId.ToString(),
                UserName = user != null ? $"{user.FirstName} {user.LastName}" : "Пользователь",
                MessageHistory = MappingProfilesConfig.Mapper.Map<IEnumerable<OrderMessageVM>>(orderServ.GetMessagesByOrderId(chatId))
            };

            return View(vm);
        }
    }
}
