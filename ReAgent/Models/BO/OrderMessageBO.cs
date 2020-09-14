using System;

namespace ReAgent.Models.BO
{
    public class OrderMessageBO
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}