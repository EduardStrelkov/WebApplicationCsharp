using ReAgent.Models.Enums;
using System;

namespace ReAgent.Models.Views.Order
{
    public class OrderVM
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public OrderStatus Status { get; set; }
    }
}
