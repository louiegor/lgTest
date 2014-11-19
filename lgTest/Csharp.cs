using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace lgTest
{   
    // ReSharper disable InconsistentNaming
    public enum Group
    {
        AKB,HKT,NMB,SKE,SNH,JKT
    }

    public class AkbGmember
    {
        //Default Constructor
        public AkbGmember()
        {
            Rank = 0;
        }

        public string Name { get; set; }
        public int Rank { get; set; }
        public Group Group { get; set; }
        public string Team { get; set; }
        public int Age { get; set; }

        public string GetRank
        {
            get
            {
                if (Rank == 0)
                {
                    return @"Out of Rank";
                }

                return String.Format("Rank{0}", Rank);
            }
        
        }
    }

    // ReSharper restore InconsistentNaming
    public class Csharp
    {
        [Test]
        public void KeyValuePairTest()
        {
            var kvpList = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Key1", "Value1"),
                    new KeyValuePair<string, string>("Key2", "Value2"),
                    new KeyValuePair<string, string>("Key3", "Value3"),
                };

            kvpList.Insert(0, new KeyValuePair<string, string>("New Key 1", "New Value 1"));


            foreach (KeyValuePair<string, string> kvp in kvpList)
            {
                Console.WriteLine("Key: {0} Value: {1}", kvp.Key, kvp.Value);
            }

            var result = kvpList.Where(s => s.Key == "Key1").ToList();
            Assert.NotNull(result);

            var result1 = kvpList.Where(kvp => kvp.Value == "Value2").ToList();
            Assert.NotNull(result1);
        }

        [Test]
        public void KeyValuePairTest2()
        {
            var member1 = new AkbGmember
                {
                    Name = "Sashihara Rino",
                    Group = Group.HKT,
                    Team = "H",
                    Rank = 2
                };

            var member2 = new AkbGmember
            {
                Name = "Shizuka Ooya",
                Group = Group.AKB,
                Team = "A",
            };

            var aKbList = new List<KeyValuePair<string, AkbGmember>>
                {
                    new KeyValuePair<string, AkbGmember>(member1.Name, member1),
                    new KeyValuePair<string, AkbGmember>(member2.Name, member2),
                };

            Console.WriteLine(member2.GetRank);
            Assert.NotNull(aKbList);
            Assert.NotNull(member2.GetRank);
        }

    }
}
