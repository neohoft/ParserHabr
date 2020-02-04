using System;
using AngleSharp.Html.Parser;
using Leaf.xNet;
using System.Collections.Generic;
using System.Windows.Forms;
using AngleSharp.Dom;

namespace ParserHabr.work
{
    public class Parser
    {
        private readonly string start;
        private readonly string end;
        private readonly string url;

        public Parser(string url, string start, string end)
        {
            this.url = url;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// Генерирует страницы 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GenerateHtmlPage()
        {
            var urls = new List<string>();

            for (int i = int.Parse(start); i <= int.Parse(end); i++)
            {
                urls.Add($"{url}page{i.ToString()}/");
            }

            return urls;
        }

        private List<string> GetPages()
        {
            var request = new HttpRequest();
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader(HttpHeader.Accept,
                    "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader(HttpHeader.AcceptLanguage, "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            request.AddHeader("Cache-Control", "max-age=0");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.KeepAlive = true;
            request.UserAgentRandomize();
            var pages = new List<string>();

            foreach (var html in GenerateHtmlPage())
            {
                pages.Add(request.Get(html).ToString());
            }


            return pages;
        }

        public Dictionary<string, string> ParsTover()
        {
            List<string> pages = GetPages();
            Dictionary<string, string> headerCollection = new Dictionary<string, string>();
            HtmlParser htmlParser = new HtmlParser();
            var blockList = new List<IHtmlCollection<IElement>>();

            for (var index = 0; index < pages.Count; index++)
            {
                var page = pages[index];
                var doc = htmlParser.ParseDocument(page);
                blockList.Add(
                        doc.QuerySelectorAll(
                                "li.content-list__item.content-list__item_post.shortcuts_item>article"));
            }

            pages = null;

            for (var i = 0; i < blockList.Count; i++)
            {
                var block = blockList[i];
                for (var index = 0; index < block.Length; index++)
                {
                    try
                    {
                        var heading = block[index];
                        headerCollection.Add(heading.QuerySelector("h2").TextContent,
                                heading.QuerySelector("h2>a").GetAttribute("href"));
                    }
                    catch (Exception e)
                    {
                        // MessageBox.Show(e.ToString());
                        continue;
                    }
                    
                }
            }


            return headerCollection;
        }
    }
}