using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using NUnit.Framework;

namespace LinqToXml
{
    [TestFixture]
    public class Program
    {
      //<root>
      //  <level1 name=""A"">
      //      <level2 name=""A1"" />
      //      <level2 name=""A2"" />
      //  </level1>
      //  <level1 name=""B"">
      //      <level2 name=""B1"" />
      //      <level2 name=""B2"" />
      //  </level1>
      //  <level1 name=""C"" />
      //</root>"

        [Test]
        public void XmlTest()
        {
            //Load xml
            const string xmlFilePath = @"c:\code\lgTest\data.xml";

            XDocument xdoc = XDocument.Load(xmlFilePath);

            //Run query
            var lv1S = from lv1 in xdoc.Descendants("level1")
                       select new
                           {
                               Header = lv1.Attribute("name").Value,
                               Children = lv1.Descendants("level2")
                           };

            var lvlStemp = lv1S.ToList();
            var result = "";

            //Loop through results
            foreach (var lv1 in lv1S)
            {
                result += ("     " + lv1.Header);
                foreach (var lv2 in lv1.Children)
                    result += ("     " + lv2.Attribute("name").Value);
            }

            Assert.IsNotNull(result);

        }

        [Test]
        public void XmlTest2()
        {
            //Load data xml 2

            const string xmlFilePath = @"c:\code\lgtest\data2.xml";
            XDocument xdoc = XDocument.Load(xmlFilePath);

            var lv1S = xdoc.Descendants("level1")
                           .Select(x=> new{
                                       Symbol = x.Attribute("symbol").Value,
                                       Open = x.Attribute("open").Value,
                                       High = x.Attribute("high").Value,
                                       Low = x.Attribute("low").Value,
                                       Close = x.Attribute("close").Value,
                                  }).ToList();
            
            Assert.IsNotNull(lv1S);
        }

        [Test]
        public void StockXmlTest2()
        {
            //Load data xml 2

            const string xmlFilePath = @"c:\code\lgtest\7203JP.xml";
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

        [Test]
        public void TrimString()
        {
            string dirtyXml =
                @"""<?xml version=\""1.0\"" encoding=\""UTF-8\""?>\n\n<ISBNdb server_time=\""2010-01-28T11:31:08Z\"">\n<BookList total_results=\""1\"" page_size=\""10\"" page_number=\""1\"" shown_results=\""1\"">\n<BookData book_id=\""quantitative_techniques\"" isbn=\""0826458548\"" isbn13=\""9780826458544\"">\n<Title>Quantitative techniques</Title>\n<TitleLong></TitleLong>\n<AuthorsText>Terry Lucey</AuthorsText>\n<PublisherText publisher_id=\""continuum\"">London : Continuum, 2002.</PublisherText>\n</BookData>\n</BookList>\n</ISBNdb>\n""";
            var temp = Regex.Replace(dirtyXml, @"^""|""$|\\n?", "");
            var temp1 = Regex.Replace(temp, @"^""|""$|\n?", "");


            string re = @"[^\x09\x0A\x0D\x20-\uD7FF\uE000-\uFFFD\u10000-\u10FFFF]";
            var temp2 = Regex.Replace(temp1, re, "");
            var temp3 = temp2.Replace("\"",@"");

            XDocument xdoc = XDocument.Load(temp3);

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

            Assert.IsNotNull(temp);
        }

        [Test]
        public void TestYahooXml()
        {
            var url =
                "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22nome%2C%20ak%22)&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

           
            var temp = GetXmlDataFromUrl(url);
            Assert.NotNull(temp);
        }
       
        //go it from http://nishantwork.wordpress.com/2012/10/10/how-to-get-xml-data-from-url-in-c/
        public static XmlDocument GetXmlDataFromUrl(string url)
        {
            //requesting the particular web page
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            //geting the response from the request url
            var response = (HttpWebResponse)httpRequest.GetResponse();

            //create a stream to hold the contents of the response (in this case it is the contents of the XML file
            var receiveStream = response.GetResponseStream();

            //creating XML document
            var mySourceDoc = new XmlDocument();

            //load the file from the stream
            mySourceDoc.Load(receiveStream);

            //close the stream
            receiveStream.Close();

            return mySourceDoc;
        }

        
        public string GetStream(string url)
        {
            string data = "";
            const string urlAddress = "http://google.com";
            var request = (HttpWebRequest)WebRequest.Create(urlAddress);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                //if (response.CharacterSet == null)
                //{
                //    readStream = new StreamReader(receiveStream);
                //}
                //else
                //{
                //    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                //}

               

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return data;
        }
    }
}
