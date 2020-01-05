using HtmlAgilityPack;
using System;
using System.Web;
using System.Linq;
using System.Net;
using System.IO;

namespace HTMLParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var urls = Enumerable.Range(14, 3).Select(i=>$"{args[0]}{i}").ToList();
            var destinationDir = args[1];
            foreach (string url in urls)
            {
                var client = new WebClient();
                var content = client.DownloadString(url);
                client.Dispose();
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var allNodes = doc.DocumentNode.Descendants().Where(node => node.Name == "a" && node.Attributes.Select(a => a.Value).Contains("Download Higher quality (128kbps) ")).ToList();
                var filesToDownload = allNodes.Select(node => new
                {
                    Url = $"http:{node.Attributes["href"].Value}",
                    FileName = $"{new string(WebUtility.HtmlDecode(node.Attributes["download"].Value).Skip(22).SkipLast(15).ToArray())}.mp3".Replace("?", "").Replace("/", ""),
                    OriginalName = node.Attributes["download"].Value
                });
                filesToDownload.ToList().ForEach(file =>
                {
                    var filePath = Path.Combine(destinationDir, file.FileName);
                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine($"Downloading {filePath}");
                        client.DownloadFile(file.Url, filePath);
                    };

                });
            }
            
            Console.WriteLine("Hello World!");
        }
    }
}
