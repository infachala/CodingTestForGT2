using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTestForGT2Junior
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            int type = -1;
            bool isExit = false;
            IDownloader downloader = null;
            string resultMsg;
            
            PrintHelp();

            while (!isExit)
            {
                input = Console.ReadLine();

                if (!int.TryParse(input, out type))
                {
                    PrintHelp();
                    continue;
                }

                switch (type)
                {
                    case 1:
                        downloader = new FtpDownloader();
                        break;
                    case 2:
                        downloader = new HttpDownloader();
                        break;
                    case 9:
                        Console.WriteLine("Bye Bye");
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        PrintHelp();
                        break;
                }

                if (type >= 1 && type <= 3 && downloader != null)
                {
                    downloader.DoDownload(out resultMsg);
                    Console.WriteLine(resultMsg);
                    Console.WriteLine();
                    PrintHelp();
                }
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("<How to use this program>");
            Console.WriteLine("-> Select Downloader Type. Only one number is valid.");
            Console.WriteLine("   1. FTP Downloader");
            Console.WriteLine("   2. HTTP Downloader");
            Console.WriteLine("   9. Exit");
            Console.WriteLine();
            Console.Write("Enter the number what you want to do. > ");
        }
    }
}
