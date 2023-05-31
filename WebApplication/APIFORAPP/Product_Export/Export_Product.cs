using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.APIFORAPP.Product_Export
{
    public class Export_Product : P_Export
    {
        public Export_Product()
        {
            this.Items = new List<P_Export_Item>();
        }
        public List<P_Export_Item> Items { get; set; }
    }
}