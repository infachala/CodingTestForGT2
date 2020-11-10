using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodingTestForGT2Junior
{
    public class HttpDownloader : IDownloader
    {
        public bool DoDownload(out string resultMsg)
        {
            string url, downloadLocalFolderPath;

            Console.WriteLine();
            Console.WriteLine("<HTTP Data Downloader>");
            Console.Write("Input URL to download data: ");
            url = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input target file path: ");
            downloadLocalFolderPath = Console.ReadLine();
            Console.WriteLine();

            if (File.Exists(downloadLocalFolderPath)) File.Delete(downloadLocalFolderPath);

            if (!Directory.CreateDirectory(Path.GetDirectoryName(downloadLocalFolderPath)).Exists)
            {
                resultMsg = "Please, check the target file path.";
                return false;
            }

            //http://www.celestrak.com/NORAD/elements/tle-new.txt
            using (var client = new WebClient())
            {
                client.DownloadFile(url, downloadLocalFolderPath);
            }

            if (File.Exists(downloadLocalFolderPath))
            {
                resultMsg = Path.GetFileName(downloadLocalFolderPath) + " is downloaded at " + Path.GetDirectoryName(downloadLocalFolderPath) + "\nSucceeded to download data from URL.\n";
                return true;
            }
            else
            {
                resultMsg = "Failed to download data form URL.";
                return false;
            }
        }
    }
}
