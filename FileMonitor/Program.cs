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
            string mydocPath = DirPath;
            Console.WriteLine(mydocPath);
            Console.ReadLine();
        }

        private static FileSystemWatcher watcher;

        private static readonly string DirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//+ @"\Interactive Data\FormulaOutput\";

        private static void Watch()
        {
            watcher = new FileSystemWatcher
                          {
                              Path = DirPath,
                              NotifyFilter = NotifyFilters.LastWrite
                                             | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                              Filter = "*.*"
                          };

            watcher.Changed += OnChanged;
            //watcher.Created += OnChangedDirectory;
            watcher.EnableRaisingEvents = true;
        }

        private static DateTime lastRead = DateTime.MinValue;

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            var name = e.Name.Replace(".txt", "");
            var line = FileReader(fullPath);
            string position="";
            if (line == "1")
            {
                //Check Orders
                position = "Buy";
                //Check Positions for specify symbol

            }

            if (line == "0")
            {
                position = "Flat";
            }

            if (line == "-1")
            {
                position = "Sell";
            }


            //Workaround for firing twice: http://stackoverflow.com/a/3042963
            var lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime != lastRead)
            {
                //Console.WriteLine("{0} has been changed", name);
                Console.WriteLine("Execute {0} for {1}", position, name);
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

        //private static void OnChangedDirectory(object source, FileSystemEventArgs e)
        //{
        //    var name = e.Name;
        //    var fullPath = e.FullPath;
        //    var line = FileReader(fullPath);
        //    //Workaround for firing twice: http://stackoverflow.com/a/3042963
        //    var lastWriteTime = Directory.GetLastWriteTime(DirPath);
        //    if (lastWriteTime != lastRead)
        //    {
        //        Console.WriteLine("{0} has been created",name);
        //        Console.WriteLine("{0}",line);
        //        Console.WriteLine();
        //    }
        //    lastRead = lastWriteTime;
        //}

    }
}