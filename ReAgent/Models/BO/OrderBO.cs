using ReAgent.Models.Enums;
using System;

namespace ReAgent.Models.BO
{
    public class OrderBO
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}