using ReAgent.App_Start;
using ReAgent.Models.BO;
using ReAgent.Models.DAO;
using ReAgent.Models.Enums;
using ReAgent.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReAgent.Services.Impl
{
    public class OrderServ : IOrderServ
    {
        public IEnumerable<OrderBO> GetAllOrders()
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return MappingProfilesConfig.Mapper.Map<IEnumerable<OrderBO>>(db.tblOrders.OrderByDescending(x => x.date));
            }
        }

        public OrderBO GetOrderById(int orderId)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return MappingProfilesConfig.Mapper.Map<OrderBO>(db.tblOrders.FirstOrDefault(x => x.pk_id == orderId));
            }
        }
        
        public IEnumerable<OrderBO> GetAllUserOrders(Guid userId)
        {
            using (ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return MappingProfilesConfig.Mapper.Map<IEnumerable<OrderBO>>(db.tblOrders.Where(x => x.fk_user == userId));
            }
        }

        public int CreateOrder(OrderBO order)
        {
            try
            {
                using (ReAgentDBEntities db = new ReAgentDBEntities())
                {
                    tblOrder tblOrder = db.tblOrders.Add(new tblOrder 
                    {
                        details = order.Details,
                        date = DateTime.Now,
                        fk_status = (int)order.Status,
                        fk_user = order.UserId
                    });

                    db.SaveChanges();

                    return tblOrder.pk_id;
                }
            }
            catch(Exception ex)
            {
                // TODO: log
                return 0;
            }
        }

        public OrderBO ChangeStatus(int orderId, OrderStatus status)
        {
            using(ReAgentDBEntities db = new ReAgentDBEntities())
            {
                tblOrder order = db.tblOrders.FirstOrDefault(x => x.pk_id == orderId);
                if(order != null)
                {
                    order.fk_status = (int)status;
                    db.SaveChanges();

                    return MappingProfilesConfig.Mapper.Map<OrderBO>(order);
                }
            }

            return null;
        }
           
        public int GetCountNewOrders()
        {
            using(ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return db.tblOrders.Count(x => x.date > NotifyManager.Instance.SyncDate);
            }
        }

        public void SaveChatMsg(string chatId, string name, string message)
        {
            try
            {
                using(ReAgentDBEntities db = new ReAgentDBEntities())
                {
                    db.tblOrderMessages.Add(new tblOrderMessage
                    {
                        fk_order = Int32.Parse(chatId),
                        user_name = name,
                        message = message,
                        send_date = DateTime.Now
                    });

                    db.SaveChanges();
                }
            }
            catch(Exception ex) 
            {
                //TODO logovani - log4net
            }
        }

        public IEnumerable<OrderMessageBO> GetMessagesByOrderId(int orderId)
        {
            using(ReAgentDBEntities db = new ReAgentDBEntities())
            {
                return MappingProfilesConfig.Mapper.Map<IEnumerable<OrderMessageBO>>(db.tblOrderMessages.Where(x => x.fk_order == orderId).OrderBy(x => x.send_date));
            }
        }

        public string GetLocalizedOrderStatusName(OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatus.New:
                    return "Новый";
                case OrderStatus.Accepted:
                    return "Принят";
                case OrderStatus.Canceled:
                    return "Отменен";
                case OrderStatus.Completed:
                    return "Выполнен";
                case OrderStatus.InProgress:
                    return "В работе";

                default:
                    return "Undefined";
            }
        }
    }
}