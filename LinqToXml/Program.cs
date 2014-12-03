using System.Linq;
using System.Xml.Linq;
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
    }
}
