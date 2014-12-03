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

        private const string Path = @"c:\CODE\lgTest\louieGor.txt";

        private static void Watch()
        {
            //if the file doesn't exist, create one
            if (!File.Exists(Path))
            {
                using (var f = File.CreateText(Path))
                {

                }
            }

            watcher = new FileSystemWatcher
                          {
                              Path = @"C:\code",
                              NotifyFilter = NotifyFilters.LastWrite
                                             | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                              Filter = "*.*"
                          };

            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private static DateTime lastRead = DateTime.MinValue;

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Workaround for firing twice: http://stackoverflow.com/a/3042963
            var lastWriteTime = File.GetLastWriteTime(Path);
            if (lastWriteTime != lastRead)
                Console.WriteLine("Something created");

            lastRead = lastWriteTime;
        }
    }
}