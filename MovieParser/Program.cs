using HtmlAgilityPack;
using System;
using System.Web;
using System.Linq;
using System.Net;
using System.IO;
namespace MovieParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            var url = args[0];
            var content = client.DownloadString(url);
            client.Dispose();
            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            Console.WriteLine("Hello World!");
        }
    }
}
