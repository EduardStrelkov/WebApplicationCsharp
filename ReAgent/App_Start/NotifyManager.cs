using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReAgent.App_Start
{
    public class NotifyManager
    {
        private static NotifyManager _instance;

        public static NotifyManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotifyManager();

                return _instance;
            }
            private set { }
        }
        
        public DateTime SyncDate { get; set; }

        private NotifyManager()
        {

        }
    }
}