using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Utils
{
    public class ConfigControl
    {
        static DeborahEntities db = new DeborahEntities();
        public static bool isBonusActive()
        {
            var config = db.Config_Bonus.SingleOrDefault(a => a.Code == "AGENTBONUS");
            if (config != null && config.Status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string DOMAIN = System.Configuration.ConfigurationManager.AppSettings["DOMAIN"];
    }
}