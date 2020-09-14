using System.Collections.Generic;

namespace ReAgent.Models.Views.Order
{
    public class ChatVM
    {
        public string ChatId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<OrderMessageVM> MessageHistory { get; set; }
    }
}