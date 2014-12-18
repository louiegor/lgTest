using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;

namespace FileMonitor
{
    public class XmlHelper
    {
        private string Symbol { get; set; }
        private string OrderNumber { get; set; }
        private string UserName { get; set; }
        private string Price { get; set; }
        private string L1RegisterPath { get { return String.Format("http://localhost:8080/Register?symbol={0}&feedtype=L1&output=bykey", Symbol); } }
        private string OstatRegisterPath { get { return String.Format("http://localhost:8080/Register?region=3&feedtype=OSTAT&output=bytype"); } }
        private string PapiRegisterPath { get { return String.Format("http://localhost:8080/Register?region=3&feedtype=PAPIORDER&output=bytype"); } }
        private string GetL1Path { get { return String.Format("http://localhost:8080/GetLv1?symbol={0}", Symbol); } }
        private string ExecuteBuyJapnOrderPath { get { return String.Format("http://localhost:8080/ExecuteOrder?symbol={0}&limitprice={1}&ordername=JAPN%20Buy%20TSE%20Limit%20DAY&shares=100", Symbol, Price); } }
        private string GetOrderStatePath { get { return String.Format("http://localhost:8080/GetOrderState?ordernumber={0}", OrderNumber); } }
        private string GetOpenPostionsPath { get { return String.Format("http://localhost:8080/GetOpenPositions?user={0}&symbol={1}", UserName, Symbol); } }
        private string FlattenPath { get { return String.Format("http://localhost:8080/Flatten?symbol={0}", Symbol); } }
        private string CancelBuyOrderPath { get { return String.Format("http://localhost:8080/CancelOrder?type=all&symbol={0}&side=bid", Symbol); } }

        public void RegisterAll()
        {
            var regList = new List<string>();
            regList.Add(L1RegisterPath);
            regList.Add(OstatRegisterPath);
            regList.Add(PapiRegisterPath);

            foreach (var item in regList)
            {
                GetXmlDataFromUrl(item);
            }
        }

        public void GetL1(string url)
        {
            var xdoc = GetXmlDataFromUrl(GetL1Path);
            
            var lv1S = xdoc.Descendants("Level1Data")
               .Select(x => new
               {
                   Symbol = x.Attribute("Symbol").Value,
                   Open = x.Attribute("BidSize").Value,
                   High = x.Attribute("AskSize").Value,
                   Low = x.Attribute("BidSize").Value,
                   Close = x.Attribute("AskSize").Value,
                   MarketTime = Convert.ToDateTime(x.Attribute("MarketTime").Value)
               }).ToList();

            var current = lv1S.FirstOrDefault();
        }

        public XDocument GetXmlDataFromUrl(string url)
        {
            //requesting the particular web page
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            //geting the response from the request url
            var response = (HttpWebResponse)httpRequest.GetResponse();

            //create a stream to hold the contents of the response (in this case it is the contents of the XML file
            var receiveStream = response.GetResponseStream();

            using (XmlReader reader = XmlReader.Create(receiveStream))
            {
                return XDocument.Load(reader);
            }

        }


        [Test]
        public void TestYahooXml()
        {
            var url =
                "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22nome%2C%20ak%22)&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";


            var temp = GetXmlDataFromUrl(url);
            Assert.NotNull(temp);
        }

        public void StockXmlTest2(string xmlFilePath = @"c:\code\lgtest\7203JP.xml")
        {
            XDocument xdoc = XDocument.Load(xmlFilePath);

            var lv1S = xdoc.Descendants("Level1Data")
                           .Select(x => new
                           {
                               Symbol = x.Attribute("Symbol").Value,
                               Open = x.Attribute("BidSize").Value,
                               High = x.Attribute("AskSize").Value,
                               Low = x.Attribute("BidSize").Value,
                               Close = x.Attribute("AskSize").Value,
                               MarketTime = Convert.ToDateTime(x.Attribute("MarketTime").Value)
                           }).ToList();

            var current = lv1S.FirstOrDefault();

            Assert.IsNotNull(current);
        }

    }
}
