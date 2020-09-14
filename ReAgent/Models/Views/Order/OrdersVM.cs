using System.Collections.Generic;

namespace ReAgent.Models.Views.Order
{
    public class OrdersVM
    {
        public bool IsAdmin { get; set; }

        public IEnumerable<OrderVM> Orders { get; set; }
    }
}
