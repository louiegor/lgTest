using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NUnit.Framework;

namespace lgTest
{
    
    public interface IMobileService
    {
        void Execute();
    }

    public class SmsService :IMobileService
    {
        public string Name { get; set; }

        public void Execute()
        {
            Name = "Sms Service";
        }
    }

    public interface IMailService
    {
        void Execute();
    }

    public class EmailService:IMailService
    {
        public string Name { get; set; }

        public void Execute()
        {
            Name = "Email Service";
        }
    }

    [TestFixture]
    public class AutoFac
    {

        [Test]
        public void AutoFacTest()
        {
            var builder = new ContainerBuilder();
            
        }
    }
}
