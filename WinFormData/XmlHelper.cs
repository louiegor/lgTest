using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WinFormData
{
    public static class Extension
    {
        public static int RoundUpToNearest100(this int x)
        {
            var result = (int)Math.Round(x / 100.0);
            if (x > 0 && result == 0)
            {
                result = 1;
            }
            return result * 100;
        }
    }
    public class XmlHelper
    {
        public string Symbol { get; set; }
        public string OrderNumber { get; set; }
        public string Price { get; set; }

        
        private string GetL1Path { get { return String.Format("http://localhost:8080/GetLv1?symbol={0}", Symbol); } }

        private string GetOpenOrderPath { get { return String.Format("http://localhost:8080/GetOpenOrders?user={0}", Global.TraderId); } }

        public void RegisterAll()
        {
            string l1RegisterPath = String.Format("http://localhost:8080/Register?symbol={0}&feedtype=L1&output=bykey",Symbol);
            string ostatRegisterPath = String.Format("http://localhost:8080/Register?region=3&feedtype=OSTAT&output=bytype");
            string papiRegisterPath = String.Format("http://localhost:8080/Register?region=3&feedtype=PAPIORDER&output=bytype");

            var regList = new List<string> {l1RegisterPath, ostatRegisterPath, papiRegisterPath};
            
            foreach (var item in regList)
            {
                GetXmlDataFromUrl(item);
            }
        }

        public void CancelSellOrdersForSymbol(string symbol)
        {
            var xmlPath = String.Format("http://localhost:8080/CancelOrder?type=all&symbol={0}&side=ask", symbol);
            var xdoc = GetXmlDataFromUrl(xmlPath);
            var x = xdoc.XPathSelectElements("//Content").Single().Value;
        }

        public void CancelBuyOrdersForSymbol(string symbol)
        {
            var xmlPath = String.Format("http://localhost:8080/CancelOrder?type=all&symbol={0}&side=bid", symbol);
            var xdoc = GetXmlDataFromUrl(xmlPath);
            var x = xdoc.XPathSelectElements("//Content").Single().Value;
        }

        public void FlattenSymbol (string symbol)
        {
            var xmlPath  = String.Format("http://localhost:8080/Flatten?symbol={0}", symbol);
            var xdoc = GetXmlDataFromUrl(xmlPath);
            var x = xdoc.XPathSelectElements("//Content").Single().Value;

        }

        public OpenPosition GetOpenPositionForSymbol(string symbol)
        {
            //var xmlPath = String.Format("http://localhost:8080/GetOpenPositions?user={0}&symbol={1}", Global.TraderId, symbol);
            //var xdoc = GetXmlDataFromUrl(xmlPath);

            const string xmlexecId = @"C:\CODE\lgTest\PproXml\8GetOpenPositions.xml";//debug
            XDocument xdoc = XDocument.Load(xmlexecId);//debug

            var oPositions = xdoc.Descendants("Position")
                .Select(x => new OpenPosition
                                 {
                                     Symbol = x.Attribute("Symbol").Value,
                                     Market = x.Attribute("Market").Value,
                                     Volume = Int32.Parse(x.Attribute("Volume").Value),
                                     Side = x.Attribute("Side").Value,

                                 })
                .ToList();
            
            var o = oPositions.FirstOrDefault(x => x.Symbol == symbol.Split('.').ToArray()[0]);
            
            return o;
        }

        public string ExecuteOrder(string side, string symbol, int price, int shares = 100)
        {
            var execPath = side == "Buy" ? Buy(symbol, price, shares) : Sell(symbol, price, shares);
            
            //var xdoc = GetXmlDataFromUrl(execPath);
            const string xmlexecId = @"C:\CODE\lgTest\PproXml\5ExecuteOrder.xml";//debug
            XDocument xdoc = XDocument.Load(xmlexecId);//debug
            var execId = xdoc.XPathSelectElements("//Content").FirstOrDefault();


            //var xdoc2 = GetXmlDataFromUrl(String.Format("http://localhost:8080/GetOrderState?ordernumber={0}", execId));
            const string xmlOrder = @"C:\CODE\lgTest\PproXml\6GetOrderNumber.xml";//debug
            XDocument xdoc2 = XDocument.Load(xmlOrder);//debug
            var orderNum = xdoc2.XPathSelectElements("//Content").Single().Value;

            return orderNum;
        }

        public string Buy(string symbol, int price, int shares = 100)
        {
            var country = symbol.Split('.').ToArray()[1];

            if (country == "JP")
            {
                return String.Format(
                    "http://localhost:8080/ExecuteOrder?symbol={0}&limitprice={1}&ordername=JAPN Buy TSE Limit DAY&shares={2}",
                    symbol, price, shares);
            }

            throw new NotSupportedException("The symbol is not supported");
        }

        public string Sell(string symbol, int price, int shares = 100)
        {
            var country = symbol.Split(',')[1];

            if (country == "JP")
            {
                return String.Format(
                    "http://localhost:8080/ExecuteOrder?symbol={0}&limitprice={1}&ordername=JAPN Sell->Short TSE Limit DAY&shares={2}",
                    symbol, price, shares);
            }

            throw new NotSupportedException("The symbol is not supported");
        }

        public Lv1Quote GetL1(string symbol)
        {
            string getL1Path=String.Format("http://localhost:8080/GetLv1?symbol={0}", Symbol); 
            var xdoc = GetXmlDataFromUrl(getL1Path);
            
            var lv1S = xdoc.Descendants("Level1Data")
               .Select(x => new Lv1Quote
               { 
                   Symbol = x.Attribute("Symbol").Value,
                   BidPrice = Int32.Parse(x.Attribute("BidPrice").Value),
                   AskPrice = Int32.Parse(x.Attribute("AskPrice").Value),
                   BidSize = Int32.Parse(x.Attribute("BidSize").Value),
                   AskSize = Int32.Parse(x.Attribute("AskSize").Value),
                   MarketTime = Convert.ToDateTime(x.Attribute("MarketTime").Value)
               }).ToList();

            var current = lv1S.FirstOrDefault();
            return current;
        }

        public XDocument GetXmlDataFromUrl(string url)
        {
            try
            {
                var httpRequest = (HttpWebRequest) WebRequest.Create(url);

                var response = (HttpWebResponse) httpRequest.GetResponse();

                var receiveStream = response.GetResponseStream();

                using (XmlReader reader = XmlReader.Create(receiveStream))
                {
                    var x = XDocument.Load(reader);
                    return x;
                }
            }catch(Exception e)
            {
                Form1.Form1Edit.SetText(e.Message);
                return new XDocument();
            }
        }
        
  
        
    }
}
