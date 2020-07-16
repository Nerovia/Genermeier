using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace Genermeier
{
    class GetMeier
    {

        class Networking
        {

            private WebClient web = new WebClient();

            public List<string> dataObjects = new List<string>();

            string SurnameLink = @"https://www.deutsche-nachnamen.de/index.php/namen-von-bach-bis-wein/namen-von-knecht-bis-meister/nachnamen-mit-meier?start=1";

            string FirstNameLinkBase = @"https://www.behindthename.com/names/usage/german";

            string htmlStringSurnames;

            List<string> htmlStringFirstNames = new List<string>();

            public Networking()
            {

                htmlStringSurnames = web.DownloadString(SurnameLink);

                htmlStringFirstNames.Add(web.DownloadString(FirstNameLinkBase));

                for (int i = 1; i < 5; i++)
                {
                    htmlStringFirstNames.Add(web.DownloadString(FirstNameLinkBase + i.ToString()));
                }
            }
        }

        private class Crawler
        {
            private static HtmlNode getRootNode(string script)
            {
                HtmlDocument doc = new HtmlDocument();

                HtmlNode html;

                doc.LoadHtml(script);

                return html = doc.DocumentNode.ChildNodes.First(x => x.Name == "html");
            }

            public static List<string> getLinksFromClass(string script, string className)
            {

                HtmlNode html = getRootNode(script);

                List<string> list = new List<string>();

                // for elements in list = @"//div[@uael-post__complete-box-overlay]"

                foreach (HtmlNode node in html.SelectNodes(className))
                {
                    foreach (HtmlNode l in html.SelectNodes("//a[@href]"))
                    {
                        string rawLink = l.Attributes["href"].Value;

                        if (rawLink.Contains(prefix))
                        {
                            list.Add(rawLink);
                        }
                    }
                }
                return list;
            }

            // for content of class "elementor-widget-container"

            public static string getStringFromClass(string script, string className)
            {

                HtmlNode html = getRootNode(script);

                // for content of class "elementor-widget-container"

                HtmlNode node = html.SelectSingleNode(className);

                return node.InnerText;
            }

            private static string prefix = @"https://www.bernertierschutz.ch/tiere/";

        }


        public GetMeier()
        {
                
        }



    }
}



