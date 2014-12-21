using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WinFormData
{
    public class Lv1Quote
    {
        public string Symbol { get; set; }
        public int Open { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
        public int Close { get; set; }
        public DateTime MarketTime { get; set; }
    }

    public class OpenPosition
    {
        public string Symbol { get; set; }
        public string Market { get; set; }
        public int Volume { get; set; }
        public string Side { get; set; }
    }

    public static class Global
    {
        public static string TraderId { get; set; }
        public static string PproPath { get; set; }
        public static string EsignalPath { get; set; }
    }

    public class Chrome: MainModel.Site
    {
        public string Louiegor { get; set; }
    }


}
