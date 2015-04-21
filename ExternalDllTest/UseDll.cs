using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DllTest;
using NUnit.Framework;

namespace ExternalDllTest
{
    [TestFixture]
    public class UseDll
    {
        public void UseDllTest()
        {
            const string temp = "this cake taste good!";
            var final = temp.ByLouiegor();

            Assert.AreNotEqual(final.Length, 0);
        }

    }
}
