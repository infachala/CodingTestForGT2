using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace CodingTestForGT2Junior
{
    public class FtpDownloader : IDownloader
    {
        bool DownloadFile(string ftpPath, string id, string pwd, string localPath)
        {            
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(localPath + Path.GetFileName(ftpPath), FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpPath);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.Credentials = new NetworkCredential(id, pwd);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                Console.WriteLine(Path.GetFileName(ftpPath)+" is downloaded at " + Path.GetDirectoryName(localPath));
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        string[] GetFileList(string ftpPath, string id, string pwd)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(ftpPath);
                reqFTP.Credentials = new NetworkCredential(id, pwd);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public List<string> GetFilesDetailList(string ftpPath, string id, string pwd)
        {
            List<string> files = new List<string>();
            string line = null;

            try
            {
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(ftpPath);
                ftp.Credentials = new NetworkCredential(id, pwd);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);

                while ((line = reader.ReadLine()) != null)
                {
                    files.Add(line);
                }

                reader.Close();
                response.Close();
                return files;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                files = null;
                return files;
            }
        }

        bool CheckAndDownload(string ftpPath, string id, string pwd, string localPath)
        {
            List<string> filesDetail = GetFilesDetailList(ftpPath, id, pwd);

            string[] files = GetFileList(ftpPath, id, pwd);

            for (int i = 0; i < filesDetail.Count; i++)
            {             
                if (filesDetail[i][0] == '-') //if its file
                {
                    DownloadFile(ftpPath + files[i], id, pwd, localPath);
                }
                else if (filesDetail[i][0] == 'd') //if its directory
                {
                    if(Directory.Exists(localPath+files[i]) == false)
                    {
                        Console.WriteLine("\n" + files[i] + " Directory is made at " + localPath + "\n");
                        Directory.CreateDirectory(localPath + files[i]);
                    }
                    else
                    {
                        Console.WriteLine("\n" + files[i] + " Directory is exists at " + localPath + " already.\n");
                    }
                    CheckAndDownload(ftpPath + files[i] + "/", id, pwd, localPath + files[i] + "\\");
                }
            }
            return true;
        }

        bool IDownloader.DoDownload(out string resultMsg)
        {
            string ftpPath, id, pwd, localPath;

            Console.WriteLine();
            Console.WriteLine("<FTP Data Downloader>");
            Console.Write("Input FTP ADDRESS to download data: ");
            ftpPath = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input ID: "); //anonymous
            id = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input PASSWORD: "); //""
            pwd = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input target file path: ");
            localPath = Console.ReadLine();
            Console.WriteLine();            

            if(Directory.Exists(localPath)==false)
            {
                Directory.CreateDirectory(localPath);
            }

            if (CheckAndDownload(ftpPath, id, pwd, localPath))
            {
                resultMsg = "\nAll files are downloaded.\n";
                return true;
            }
            else
            {
                resultMsg = "\nFailed to download data form URL.\n";
                return false;
            }            
        }
    }
}
