using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GettingStartedClient.ServiceReference1;

namespace GettingStartedClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Channel Factory Test
            var httpFactory =
                new ChannelFactory<ICalculator>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8000/GettingStarted/CalculatorService/"));

            ICalculator httpProxy = httpFactory.CreateChannel();

            Console.WriteLine("***testing***");
            //var x = httpProxy.Add(1, 2);
            //Console.WriteLine("http:" + httpProxy.Add(1, 1));
            //Console.ReadLine();


            //Step 1: Create an instance of the WCF proxy.
            var client = new CalculatorClient();

            //Step 1.5: louiegor modification
            Console.WriteLine("press <hi> to start calculation <close> to close program");
            var temp = Console.ReadLine();

            while (temp != "close")
            {
                if (temp == "hi")
                {
                    Calculate(client);
                    Console.WriteLine("press <hi> to start calculation <close> to close program");
                    temp = Console.ReadLine();
                }

                else
                {
                    Console.WriteLine("press <hi> to start calculation");
                    temp = Console.ReadLine();
                }
            }

            client.Close();
        }

        public static void Calculate(CalculatorClient client)
        {
            // Step 2: Call the service operations.
            // Call the Add service operation.
            double value1 = 100.00D;
            double value2 = 15.99D;
            double result = client.Add(value1, value2);
            Console.WriteLine("Add({0},{1}) = {2}", value1, value2, result);

            // Call the Subtract service operation.
            value1 = 145.00D;
            value2 = 76.54D;
            result = client.Subtract(value1, value2);
            Console.WriteLine("Subtract({0},{1}) = {2}", value1, value2, result);

            // Call the Multiply service operation.
            value1 = 9.00D;
            value2 = 81.25D;
            result = client.Multiply(value1, value2);
            Console.WriteLine("Multiply({0},{1}) = {2}", value1, value2, result);

            // Call the Divide service operation.
            value1 = 22.00D;
            value2 = 7.00D;
            result = client.Divide(value1, value2);
            Console.WriteLine("Divide({0},{1}) = {2}", value1, value2, result);
        }

    }
}
