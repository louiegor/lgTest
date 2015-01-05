using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;

namespace FileMonitor
{
    [TestFixture]
    public class TestFileWatcher
    {
        XmlHelper xmlHelper = new XmlHelper();
        Program fileWatcher = new Program();
        //private const string DirectoryPath = @"C:\Users\louiegor\Documents\Interactive Data\FormulaOutput";
        private const string DirectoryPath = @"c:\CODE\lgTest\testDirectory";
        private string filePath = DirectoryPath + "\\test.txt";

        [Test]
        public void Test()
        {
            //const string lines = "First line.\r\nSecond line.\r\nThird line.";
            //if the directory path does not exist, create it
            if (!Directory.Exists(DirectoryPath))
            {
                DirectoryInfo  di = Directory.CreateDirectory(DirectoryPath);
                Console.ReadLine();
            }
            
            if (!File.Exists(filePath))
            {
                using (var f = File.CreateText(filePath))
                {

                }
            }

            //var file = new StreamWriter(filePath);

            Watch();

            for (var i=0; i<10; i++)
            {
                string lines = String.Format("line {0}\r\n",i);
                var file = new StreamWriter(filePath);
                Thread.Sleep(1500);
                file.WriteLine(lines);
                file.Flush();
                file.Close();
                //WriteStuffTofile(file);
            }

            Assert.IsNotNull(DirectoryPath);
        }

        private static FileSystemWatcher watcher;

        public static void Watch()
        {
            watcher = new FileSystemWatcher
            {
                Path = DirectoryPath,
                NotifyFilter = NotifyFilters.LastWrite
                               | NotifyFilters.FileName |
                               NotifyFilters.DirectoryName,
                Filter = "*.*"
            };

            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private static DateTime lastRead = DateTime.MinValue;

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Workaround for firing twice: http://stackoverflow.com/a/3042963
            var lastWriteTime = File.GetLastWriteTime(DirectoryPath);
            if (lastWriteTime != lastRead)
                Console.WriteLine("Something created");

            lastRead = lastWriteTime;
        }


        [Test]
        public void TestYahooXml()
        {
            var url =
                "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22nome%2C%20ak%22)&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";


            var temp = xmlHelper.GetXmlDataFromUrl(url);
            Assert.NotNull(temp);
        }

        [Test]
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
