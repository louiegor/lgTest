using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Observable
    {
        public event EventHandler SomethingHappened;
        public event EventHandler SaidSomething;

        public void DoSomething()
        {
            EventHandler handler = SomethingHappened;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void SaySomething()
        {
            EventHandler handler = SaidSomething;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }

    class Observer
    {
        public void HandleEvent(object sender, EventArgs args)
        {
            Console.WriteLine("Something happened to " + sender);
        }
    }

    
    class Program
    {
        static void Main()
        {
            var observable = new Observable();
            var observer = new Observer();
            observable.SomethingHappened += observer.HandleEvent;
            observable.SaidSomething += observer.HandleEvent;

            observable.DoSomething();
            observable.SaySomething();
            Console.ReadLine();
        }
    }
}

