using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    //public delegate void EventHandler();

    //class Program
    //{
    //    public static event EventHandler _show;

    //    static void Main()
    //    {
    //        // Add event handlers to Show event.
    //        _show += new EventHandler(Dog);
    //        _show += new EventHandler(Cat);
    //        _show += new EventHandler(Mouse);
    //        _show += new EventHandler(Mouse);

    //        // Invoke the event.
    //        _show.Invoke();
    //    }

    //    static void Cat()
    //    {
    //        Console.WriteLine("Cat");
    //    }

    //    static void Dog()
    //    {
    //        Console.WriteLine("Dog");
    //    }

    //    static void Mouse()
    //    {
    //        Console.WriteLine("Mouse");
    //    }
    //}

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
            observable.DoSomething();
            observable.SaySomething();
            Console.ReadLine();
        }
    }
}

