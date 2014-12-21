using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WinFormData
{
    public class FileWatcher
    {
        XmlHelper xmlHelper = new XmlHelper();
        private static FileSystemWatcher watcher;
        
        //private const string DirectoryPath = @"C:\Users\louiegor\Documents\Interactive Data\FormulaOutput";
        private const string DirectoryPath = @"c:\CODE\lgTest\testDirectory";
        private static readonly string DirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ @"\Interactive Data\FormulaOutput\";
        
        public string GetEsignalPath()
        {
            return DirPath;
        }

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
            string position = "";
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

        public bool SiteGoogleConnection()
        {
            string text;
            var browser = new Chrome();
            
            var httpRequest = (HttpWebRequest)WebRequest.Create(browser.Uri);

            var response = (HttpWebResponse)httpRequest.GetResponse();

            var receiveStream = response.GetResponseStream();

            using (var reader = new StreamReader(receiveStream, Encoding.UTF8))
            {
                text = reader.ReadToEnd();
            }

            int first = text.IndexOf("@A@", StringComparison.Ordinal) + "@B@".Length;
            int last = text.LastIndexOf("@B@", StringComparison.Ordinal);
            string title = text.Substring(first, last - first);
            if (title != "louiegor") return false;
            return true;

        }

        public static string FileReader(string fileName)
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
