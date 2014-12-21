using NUnit.Framework;

namespace WinFormData
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestStuff()
        {
            const int x = 1;
            Assert.AreEqual(x,1);
        }
    }
}
