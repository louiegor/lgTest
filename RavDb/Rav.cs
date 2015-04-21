using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Document;

namespace RavDb
{
    [TestFixture]
    public class Rav
    {
        public class Menu
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public List<Course> Courses { get; set; }
        }

        public class Course
        {
            public string Name { get; set; }
            public double Cost { get; set; }
            public List<string> Allergenics { get; set; }
        }

        [Test]
        public void RavTest()
        {

            using (var store = new DocumentStore {Url = "http://localhost:8080", DefaultDatabase = "lgDb"})
            {
                store.Initialize();

                using (var session = store.OpenSession())
                {
                    var menu =
                        new Menu
                            {
                                Name = "Breakfast Menu",
                                Courses = new List<Course>
                                    {
                                        new Course {Name = "Milk", Cost = 2.50}
                                    }
                            };

                    session.Store(menu);
                    session.SaveChanges();

                    var temp = //session.Load<Menu>(menu.Id);

                    session.Query<Menu>()
                           .Where(x => x.Name == "Breakfast Menu")
                           .ToList();


                        //.Where(xd => xd.Name == "Breakfast Menu").ToList();

                    Assert.NotNull(temp);
                }
            }
        }
    }
}