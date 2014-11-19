using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace lgTest
{
    public interface IAnimal
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class Cat:IAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MeowSound { get; set; }
        public string GetNameAndSound { get { return Name + " makes " + MeowSound + " sound"; } }
        public Food Food { get; set; }
    }

    public class Dog:IAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarkSound { get; set; }
        public Food Food { get; set; }
    }

    public class Food
    {
        public string Name { get; set; }
    }

    public class Human : IAnimal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Status { get { return Age > 30 ? "cool" : "young"; } }
        private string _message;
        public string Message {
        get
        {
            if (Age < 10) return "I love AKB";
            if (Age >= 10 && Age < 30) return "I love AKB48 so much";
            return _message;
        }
        set { _message = value; }
        }

        
    }

    public class Zoo
    {
        public string Name { get; set; }
        public List<IAnimal> Animals { get; set; }
        public int NumOfAnimals { get; set; }
    }

    public class Main
    {
        public List<IAnimal> Get10Animals(bool isHuman)
        {
            if (!isHuman)
            {
                var list = new List<IAnimal>();
                for (var i = 0; i < 10; i++)
                {
                    if (i%2 == 0)
                        list.Add(new Dog {Name = "Dog" + i});
                    else
                        list.Add(new Cat {Name = "Cat" + i});
                }

                return list;
            }

            var humanList = new List<IAnimal>();
            for (var j = 0; j < 10; j++)
            {
                humanList.Add(new Human {Name = "Human" + j});
            }
            return humanList;
            
        }
    }

    [TestFixture]
    public class GenericTypes
    {
        [Test]
        public void GenericTests()
        {
            var dog = (Dog) GetAnimal("dog");
            Assert.AreEqual(dog.BarkSound, "wruffff.");
        }

        public IAnimal GetAnimal(string animal)
        {
            if (animal == "dog") return new Dog {BarkSound = "wruffff." };
            if (animal == "cat") return new Cat();
            return new Human();
        }

        [Test]
        public void ListGenericTest()
        {
            var main = new Main();
            var listOfHumans = main.Get10Animals(true);

            if(listOfHumans.All(x=> x is Human))
            {
                foreach (var h in listOfHumans.Cast<Human>().ToList())
                {
                    var i = h.Age;
                }
            }

            foreach (var animal in listOfHumans)
            {
                var human = animal as Human;
                if (human != null)
                {
                    var i = human.Age;
                }
            }

            Assert.AreEqual(listOfHumans.Count(), 10);
        }

        [Test]
        public void SelectTest()
        {
            var catList = new List<Cat>();
            var catFood = new Food
                {
                    Name = "Cat Food"
                };

            var cat = new Cat
                {
                    Name = "Cat1",
                    Food = catFood,
                    MeowSound = "Meow!!!"
                };
            var cat2 = new Cat
                {
                    Name = "Cat2",
                    Food = catFood,
                    MeowSound = "moooooo"
                };


            catList.Add(cat);
            catList.Add(cat2);

            var newList = catList.Select(x => new
                {
                    Cat =x,
                    FoodName =x.Food.Name,
                }).ToList();

            Assert.NotNull(newList);


        }

        [Test]
        public void SelectManyTest()
        {
            var zoos = new List<Zoo>
                {
                    new Zoo
                        {
                            Name = "Zoo1",
                            Animals = new List<IAnimal>
                                {
                                    new Dog {Name = "Doggy1"},
                                    new Cat {Name = "KittyCat1"},
                                    new Dog {Name = "Doggy2"}
                                }
                        },
                    new Zoo
                        {
                            Name = "Zoo2",
                            Animals = new List<IAnimal>
                                {
                                    new Human{Name = "louiegor"},
                                    new Dog{Name = "Crazy Dog"},
                                    new Cat{Name = "Super Kitty Cat"}
                                }
                        }
                };

            var animalName = zoos.SelectMany(x => x.Animals, (x, y) => y.Name).ToList();

            var complexAnimal = zoos
                .SelectMany(x => x.Animals, (x, y) => new {Zoo = x, Animal = y})
                .Select(x => new CustomList
                    {
                        ZooName = x.Zoo.Name,
                        AnimalName = x.Animal.Name
                    }).ToList();
               
            Assert.AreEqual(complexAnimal.Count(),6);
            Assert.AreEqual(animalName.Count(),6);
        }

        public class CustomList
        {
            public string AnimalName { get; set; }
            public string ZooName { get; set; }
        }


        [Test]
        public void TestGetter()
        {
            var kittycat = new Cat
                {
                    Name = "Kitty",
                    MeowSound = "Meow!!!!"
                };

            var x = kittycat.GetNameAndSound;
            Assert.AreEqual(x, "Kitty makes Meow!!!! sound");
        }

        [Test]
        public void TestGetter2()
        {
            var human1 = new Human
                {
                    Name = "louiegor",
                    Age = 32,
                    Message = "above 30"
                };

            var human2 = new Human
                {
                    Name = "Chi",
                    Age = 19,
                    Message = "below 19"
                };
            Assert.AreEqual(human1.Status, "cool");
            Assert.AreEqual(human1.Message, "above 30");
            Assert.AreEqual(human2.Message, "I love AKB48 so much");
        }

        [Test]
        public void TestOut()
        {
            Cat cat;
            var cat2 = new Cat
                {
                    Name= "louiegor's cat"
                };
            
            OutMethod(out cat);
            RefMethod(ref cat2);

            Assert.AreEqual(cat.Name, "louiegor");
            Assert.AreEqual(cat2.Name, "louiegor's cat Is Modified");
        }

        public static void OutMethod(out Cat inputCat)
        {
            var newCat = new Cat {Name = "louiegor"};
            inputCat = newCat;
        }

        public static void RefMethod(ref Cat inputCat)
        {
            inputCat.Name = inputCat.Name + " Is Modified";
        }

        [Test]
        public void AbcTest()
        {
            const string jAin = "sashihara rino";
            Console.WriteLine(jAin);
        }
    }
}
