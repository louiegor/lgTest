using System;
using System.IO;
using System.Security.Permissions;

namespace FileMonitor
{
    internal class Program
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Main(string[] args)
        {
            Watch();
            Console.ReadLine();
        }

        private static FileSystemWatcher watcher;
        private const string DirPath = @"c:\CODE\lgTest\";

        private static void Watch()
        {
            watcher = new FileSystemWatcher
                          {
                              Path = @"c:\CODE\lgTest\",
                              NotifyFilter = NotifyFilters.LastWrite
                                             | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                              Filter = "*.*"
                          };

            watcher.Changed += OnChanged;
            watcher.Created += OnChangedDirectory;
            watcher.EnableRaisingEvents = true;
        }

        private static DateTime lastRead = DateTime.MinValue;

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            var name = e.Name;
            var fullPath = e.FullPath;
            var line = FileReader(fullPath);
            //Workaround for firing twice: http://stackoverflow.com/a/3042963
            var lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime != lastRead)
            {
                Console.WriteLine("{0} has been changed", name);
                Console.WriteLine("{0}", line);
                Console.WriteLine();
            }
            //Console.WriteLine("The files has been changed");

            lastRead = lastWriteTime;
        }

        private static void OnChangedDirectory(object source, FileSystemEventArgs e)
        {
            var name = e.Name;
            var fullPath = e.FullPath;
            var line = FileReader(fullPath);
            //Workaround for firing twice: http://stackoverflow.com/a/3042963
            var lastWriteTime = Directory.GetLastWriteTime(DirPath);
            if (lastWriteTime != lastRead)
            {
                Console.WriteLine("{0} has been created",name);
                Console.WriteLine("{0}",line);
                Console.WriteLine();
            }
            lastRead = lastWriteTime;
        }

        public static string FileReader (string fileName)
        {
            // Read the file and display it line by line.
            string line;

            using (var sr = new StreamReader(fileName))
            {
                line = sr.ReadToEnd();
            }
            
            return line;
        }
    }
}