using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace lgTest
{
    [TestFixture]
    public class Recursive
    {
        public class Task
        {
            public string Wbs { get; set; }
            public int Sequence { get; set; }
            public bool IsSummary { get; set; }
            public Task ParentTask { get; set; }
            public string Description { get; set; }
        }
        
        [Test]
        public void InsertAfterRecursiveTest()
        {
            var wbs = SetupData();
            var newTask = new Task {Description = "gogogogogoo new task!!!!"};

            //InsertAfter(1, newTask, wbs);
            InsertBefore(1, newTask, wbs);

            var result = wbs.OrderBy(x => x.Sequence);

            Assert.AreEqual(8, wbs.Count);
        }

        public void InsertBefore(int beforeSeqNum, Task newTask, List<Task> wbs)
        {
            var task = wbs.Single(x => x.Sequence == beforeSeqNum);
            var index = wbs.IndexOf(task);

            wbs.Insert(index , newTask);

            if (index != 0)
            {
                var taskb4 = wbs.Single(x => x.Sequence == beforeSeqNum - 1);
                if (!taskb4.IsSummary)
                    newTask.ParentTask = task.ParentTask;

                if (taskb4.IsSummary)
                    newTask.ParentTask = task;
            }

            //Recalculate the sequence
            for (int i = 0; i < wbs.Count; i++)
            {
                wbs[i].Sequence = i + 1;
            }

            RefreshWbs(wbs);
            
        }

        public void InsertAfter(int afterSeqNum, Task newTask, List<Task> wbs)
        {
            var task = wbs.Single(x => x.Sequence == afterSeqNum);

            var index = wbs.IndexOf(task);
            wbs.Insert(index + 1, newTask);


            if (!task.IsSummary)
                newTask.ParentTask = task.ParentTask;
            

            if (task.IsSummary)
                newTask.ParentTask = task;
            

            //Recalculate the sequence
            for (int i = 0; i < wbs.Count; i++)
            {
                wbs[i].Sequence = i + 1;
            }

            RefreshWbs(wbs);
        }
        

        public void RefreshWbs(List<Task> tasks)
        {
            var rootSummaries =
                tasks.Where(
                    x => (x.IsSummary && x.ParentTask == null) || !x.IsSummary && x.ParentTask == null)
                     .ToList().OrderBy(x => x.Sequence);

            var wbsCount = 1;
            foreach (var rootSummary in rootSummaries)
            {
                rootSummary.Wbs = "" + wbsCount;
                wbsCount++;
                FillInWbs(rootSummary, tasks, rootSummary.Wbs + ".");
            }

        }

        private void FillInWbs(Task summary, List<Task> tasks, string wbs)
        {
            var wbsCount = 1;
            var fills = tasks.Where(x => x.ParentTask == summary).OrderBy(x => x.Sequence).ToList();

            foreach (var fill in fills)
            {
                fill.Wbs = wbs + wbsCount;
                
                
                if (fill.IsSummary)
                    FillInWbs(fill, tasks, fill.Wbs + ".");

                wbsCount++;
            }
        }


        public List<Task> SetupData()
        {
            var result = new List<Task>();
            var task1 = new Task
            {
                IsSummary = true,
                Wbs = "1",
                Sequence = 1,
                Description = "Top"
            };
            var task2 = new Task
            {
                IsSummary = false,
                Wbs = "1.1",
                Sequence = 2,
                ParentTask = task1
            };
            var task3 = new Task
            {
                IsSummary = false,
                Wbs = "1.2",
                Sequence = 3,
                ParentTask = task1
            };
            var task4 = new Task
            {
                IsSummary = true,
                Wbs = "1.3",
                Sequence = 4,
                ParentTask = task1
            };
            var task5 = new Task
            {
                IsSummary = false,
                Wbs = "1.3.1",
                Sequence = 5,
                ParentTask = task4
            };
            var task6 = new Task
            {
                IsSummary = false,
                Wbs = "1.3.2",
                Sequence = 6,
                ParentTask = task4
            };
            var task7 = new Task
            {
                IsSummary = true,
                Wbs = "2",
                Sequence = 7,

            };

            result.AddRange(new List<Task>{task1,task2,task3,task4,task5,task6,task7});
            return result;
        }

    }
}
