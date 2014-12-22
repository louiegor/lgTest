using System;

namespace WinFormData
{
    public class Lv1Quote
    {
        public string Symbol { get; set; }
        public int BidPrice { get; set; }
        public int BidSize { get; set; }
        public int AskPrice { get; set; }
        public int AskSize { get; set; }
        public DateTime MarketTime { get; set; }
    }

    public class OpenPosition
    {
        public string Symbol { get; set; }
        public string Market { get; set; }
        public int Volume { get; set; }
        public string Side { get; set; }
    }

    static class Global
    {
        public static string TraderId { get; set; }
        public static string PproPath { get; set; }
        public static string EsignalPath { get; set; }
        public static int ShareSize { get; set; }
    }

    public class Symbol
    {
        public string Name { get; set; }
    }


    public class Chrome: MainModel.Site
    {
        public string Louiegor { get; set; }
    }


}
