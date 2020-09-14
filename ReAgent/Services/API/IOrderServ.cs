using ReAgent.Models.BO;
using ReAgent.Models.Enums;
using System;
using System.Collections.Generic;

namespace ReAgent.Services.API
{
    public interface IOrderServ
    {
        IEnumerable<OrderBO> GetAllOrders();

        OrderBO GetOrderById(int orderId);

        IEnumerable<OrderBO> GetAllUserOrders(Guid userId);

        int CreateOrder(OrderBO order);

        OrderBO ChangeStatus(int orderId, OrderStatus status);

        int GetCountNewOrders();

        void SaveChatMsg(string chatId, string name, string message);

        IEnumerable<OrderMessageBO> GetMessagesByOrderId(int orderId);

        string GetLocalizedOrderStatusName(OrderStatus orderStatus);
    }
}
