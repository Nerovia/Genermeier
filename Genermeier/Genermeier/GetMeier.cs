using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace Genermeier
{
    class GetMeier
    {

        private class Networking
        {
            private WebClient web = new WebClient();

            public List<string> dataObjects = new List<string>();

            string SurnameLink = @"https://www.deutsche-nachnamen.de/index.php/namen-von-bach-bis-wein/namen-von-knecht-bis-meister/nachnamen-mit-meier?start=1";

            string FirstNameLinkBase = @"https://www.behindthename.com/names/usage/german";

            public string HtmlStringSurnames;

            public List<string> HtmlStringFirstNames = new List<string>();

            public Networking()
            {
                HtmlStringSurnames = web.DownloadString(SurnameLink);

                HtmlStringFirstNames.Add(web.DownloadString(FirstNameLinkBase));

                for (int i = 1; i < 5; i++)
                {
                    HtmlStringFirstNames.Add(web.DownloadString(FirstNameLinkBase + @"/" + i.ToString()));
                }
            }
        }

        private class FindMeier
        {

            private static string PatternMeier = @"(\w+meier)";
            private static string PatternMeierAlt = @"(Meier\w+)";

            public List<string> Meier(string html)
            {

                List<string> Meiers = new List<string>();

                foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(html, PatternMeier))
                {
                    Meiers.Add(m.Groups[1].Value);
                }

                foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(html, PatternMeierAlt))
                {
                    Meiers.Add(m.Groups[1].Value);
                }

                return Meiers;
            }
        }

        private class Crawler {

            private static HtmlNode getRootNode(string script)
            {
                HtmlDocument doc = new HtmlDocument();

                HtmlNode html;

                doc.LoadHtml(script);

                return html = doc.DocumentNode.ChildNodes.First(x => x.Name == "html");
            }

            public static List<string> getFirstNames(string html)
            {

                HtmlNode htmlNode = getRootNode(html);

                List<string> list = new List<string>();

                foreach (HtmlNode node in htmlNode.SelectNodes(@"//span[@class='listname']"))
                {
                    list.Add(node.InnerText.ToLower().Replace(" (1)", "").Replace(" (2)", "").FirstCharToUpper());
                }
                return list;
            }
        }



        private Networking Net;

        private FindMeier Find;

        private Random rand = new Random();

        private List<int> usedFirstNames = new List<int>();

        private List<int> usedLastNames = new List<int>();

        private List<string> LastNames;

        private List<string> FirstNames;

        public GetMeier()
        {
            Net = new Networking();
            Find = new FindMeier();

            LastNames = Find.Meier(Net.HtmlStringSurnames);

            FirstNames = new List<string>();

            foreach (string html in Net.HtmlStringFirstNames)
            {
                Crawler.getFirstNames(html).ForEach( x => FirstNames.Add(x));
            }
        }



        public string getFirstName()
        {
            while (true)
            {
                int n = rand.Next(FirstNames.Count - 1);

                if (!usedFirstNames.Contains(n))
                {
                    usedFirstNames.Add(n);
                    return FirstNames[n];
                }
            }
        }

        public string getLastName()
        {
            while (true)
            {
                int n = rand.Next(LastNames.Count - 1);

                if (!usedLastNames.Contains(n))
                {
                    usedLastNames.Add(n);
                    return LastNames[n];
                }
            }
        }
    }
}

public static class StringExtensions
{
    public static string FirstCharToUpper(this string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}


