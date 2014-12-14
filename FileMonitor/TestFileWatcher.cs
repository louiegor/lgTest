using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FileMonitor
{
    [TestFixture]
    public class TestFileWatcher
    {
        Program fileWatcher = new Program();

        private const string DirectoryPath = @"c:\CODE\lgTest\testDirectory";
        private string filePath = DirectoryPath + "\\test.txt";

        [Test]
        public void Test()
        {
            //const string lines = "First line.\r\nSecond line.\r\nThird line.";
            //if the directory path does not exist, create it
            if (!Directory.Exists(DirectoryPath))
            {
                //DirectoryInfo
                  DirectoryInfo  di = Directory.CreateDirectory(DirectoryPath);
                Console.ReadLine();
            }
            
            if (!File.Exists(filePath))
            {
                using (var f = File.CreateText(filePath))
                {

                }
            }

            //var file = new StreamWriter(filePath);

            Watch();

            for (var i=0; i<10; i++)
            {
                string lines = String.Format("line {0}\r\n",i);
                var file = new StreamWriter(filePath);
                Thread.Sleep(1500);
                file.WriteLine(lines);
                file.Flush();
                file.Close();
                //WriteStuffTofile(file);
            }

            Assert.IsNotNull(DirectoryPath);
        }

        private static FileSystemWatcher watcher;

        public static void Watch()
        {
            watcher = new FileSystemWatcher
            {
                //Path = @"C:\Users\louiegor\Documents\Interactive Data\FormulaOutput",
                Path = DirectoryPath,
                NotifyFilter = NotifyFilters.LastWrite
                               | NotifyFilters.FileName |
                               NotifyFilters.DirectoryName,
                Filter = "*.*"
            };

            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private static DateTime lastRead = DateTime.MinValue;

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Workaround for firing twice: http://stackoverflow.com/a/3042963
            var lastWriteTime = File.GetLastWriteTime(DirectoryPath);
            if (lastWriteTime != lastRead)
                Console.WriteLine("Something created");

            lastRead = lastWriteTime;
        }
    }
}
