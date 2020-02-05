using System;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Leaf.xNet;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Int32;

namespace ParserHabr.work
{
    public class Parser
    {
        private readonly string start;
        private readonly string end;
        private readonly string link;

        public Parser(string link, string start, string end)

        {
            this.link = link ;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// Генерирует страницы 
        /// </summary>
        /// <returns>Возврощает список урлов</returns>
        private List<string> GenerateHtmlPage()
        {
            var urls = new List<string>();

            for (int i = Parse(start); i <= Parse(end); i++)
            {
                urls.Add(item: $"{link}page{i.ToString()}/");
            }

            return urls;
        }
        /// <summary>
        /// Получает список Html страниц
        /// </summary>
        /// <returns></returns>
        private List<string> GetPages()
        {
            var pages = new List<string>();

            using (var request = new HttpRequest())
            {
                request.AddHeader("Accept-Encoding", "gzip, deflate");
                request.AddHeader(HttpHeader.Accept,
                        "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader(HttpHeader.AcceptLanguage, "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                request.AddHeader("Cache-Control", "max-age=0");
                request.AddHeader("Upgrade-Insecure-Requests", "1");
                request.KeepAlive = true;
                request.UserAgentRandomize();

                foreach (var html in GenerateHtmlPage())
                {
                    pages.Add(request.Get(html).ToString());
                }
            }


            return pages;
        }

        /// <summary>
        /// Парсит Html страницы
        /// </summary>
        /// <returns>Возрощает словарь, заголовок, ссылка</returns>
        public Dictionary<string, string> ParsTover()
        {
            List<string> pages = GetPages();
            Dictionary<string, string> headerCollection = new Dictionary<string, string>();
            HtmlParser htmlParser = new HtmlParser();
            var blockList = new List<IHtmlCollection<IElement>>();

            foreach (var page in pages)
            {
                var doc = htmlParser.ParseDocument(page);
                blockList.Add(
                        doc.QuerySelectorAll(
                                "li.content-list__item.content-list__item_post.shortcuts_item>article"));
            }

            foreach (var block in blockList)
            {
                foreach (var heading in block)
                {
                    try
                    {
                        headerCollection.Add(heading.QuerySelector("h2").TextContent,
                                heading.QuerySelector("h2>a").GetAttribute("href"));
                    }
                    catch
                    {
                         // MessageBox.Show("Error");
                    }
                }
            }

            return headerCollection;
        }
    }

}