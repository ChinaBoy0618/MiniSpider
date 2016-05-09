using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace MinSpdier
{
    public class Spilder
    {
        static Dictionary<string, Tuple<string, string>> bookName = new Dictionary<string, Tuple<string, string>>();
        public static void start()
        {
            Dictionary<string, int> unload = new Dictionary<string, int>();
            Dictionary<string, int> loaded = new Dictionary<string, int>();


            unload.Add("http://www.baidu.com", 0);
            string baseUrl = "";
            var count = 148;
            while (unload.Count > 0)
            {

                string url = unload.First().Key;
                int depth = unload.First().Value;
                if (!loaded.ContainsKey(url))
                    loaded.Add(url, depth);
                unload.Remove(url);
               

                Console.WriteLine("Now loading " + url);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "GET";
                req.Accept = "text/html";
                req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)";

                try
                {
                    string html = null;
                    HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                    using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                    {
                        html = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(html))
                        {
                            if (depth > 0)
                            {
                                SaveContents(html, url);
                            }

                            Console.WriteLine("Download OK!\n");
                        }
                    }
                    if (depth == 0)
                    {
                        string[] names = null, direct = null;
                        string[] links = GetLinks(html, ref names, ref direct);
                        AddUrls(links, depth + 1, baseUrl, unload, loaded, bookName, names, direct);
                    }
                }
                catch (WebException we)
                {
                    Console.WriteLine(we.Message);
                }
                if (unload.Count == 0)
                {
                    SaveContents(count);
                    unload.Add("你懂得" + (++count) + ".jsp", 0);

                }
            }
        }
        private static string[] GetLinks(string html, ref string[] names, ref string[] direct)
        {
            //const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            const string pattern = @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>.+?</a>";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection m = r.Matches(html);
            string[] links = new string[m.Count];
            names = new string[m.Count];
            direct = new string[m.Count];
            string a;
            int index;
            for (int i = 0; i < m.Count; i++)
            {
                a = m[i].ToString();
                if (a.IndexOf("hg2") > -1)
                {
                    index = a.IndexOf('"');
                    links[i] = a.Substring(index + 1, a.IndexOf("jsp") + 2 - index);
                    index = a.IndexOf('>');
                    names[i] = a.Substring(index + 1, a.LastIndexOf('<') - index - 1);
                    index = a.IndexOf('【');
                    //if (index == -1)
                    //{
                        index = a.IndexOf('>');
                        direct[i] = a.Substring(index + 1, a.LastIndexOf('>') - index - 4);
                    //}
                    //else
                    //    direct[i] = a.Substring(index, a.LastIndexOf('】') - index + 1);
                }

            }
            return links;
        }

        private static bool UrlAvailable(string url, Dictionary<string, int> unload, Dictionary<string, int> loaded)
        {
            if (unload.ContainsKey(url) || loaded.ContainsKey(url))
            {
                return false;
            }
            if (url.Contains(".jpg") || url.Contains(".gif")
                || url.Contains(".png") || url.Contains(".css")
                || url.Contains(".js") && url.IndexOf("jsp") == -1)
            {
                return false;
            }
            return true;
        }

        private static void AddUrls(string[] urls, int depth, string baseUrl, Dictionary<string, int> unload, Dictionary<string, int> loaded, Dictionary<string, Tuple<string, string>> bookName, string[] names, string[] direct)
        {
            if (depth >= 3)
            {
                return;
            }
            string url;
            for (int i = 0, j = urls.Length; i < j; i++)
            {
                url = urls[i];
                if (string.IsNullOrEmpty(url))
                    continue;
                string cleanUrl = url.Trim();
                int end = cleanUrl.IndexOf(' ');
                if (end > 0)
                {
                    cleanUrl = cleanUrl.Substring(0, end);
                }
                if (UrlAvailable(cleanUrl, unload, loaded))
                {
                    if (cleanUrl.IndexOf("hg2") > -1)
                    {
                        cleanUrl = baseUrl + url;
                        unload.Add(cleanUrl, depth);
                        bookName.Add(cleanUrl, new Tuple<string, string>(direct[i], names[i]));
                    }
                    else
                    {
                        // 外链
                    }
                }
            }
        }
        private static void SaveContents(string html, string url)
        {
            if (string.IsNullOrEmpty(html))
            {
                return;
            }
            
            //过滤内容
            string pattern = "<div[^>]*?\\s+?[^>]*?class=\"content\"[^>]*?.+?[^>]*?>[^>]*?.+?[^>]*?<span?";

            var matches = Regex.Matches(Regex.Replace(html, "\r\n", ""), pattern);
            string str = "";
            foreach (var match in matches)
            {
                str = match.ToString();
                str = Regex.Replace(str, "<div class=\"content\">", "");
                str = Regex.Replace(str, "<!--.+?-->?", "");
                str = Regex.Replace(str, "<span.+?</span>?", "");
                str = Regex.Replace(str, "<br.+?/>?", "\r\n").Replace("<span", "");
            }

            string path = "";
            //lock (_locker)
            //{
            //    path = string.Format("{0}\\{1}.txt", _path, _index++);
            //}
            string direc = AppDomain.CurrentDomain.BaseDirectory + "/books/" +CleanInvalidFileName( bookName[url].Item1);
            if (!Directory.Exists(direc))
                Directory.CreateDirectory(direc);
            try
            {
                using (StreamWriter fs = new StreamWriter(direc + "/" +CleanInvalidFileName( bookName[url].Item2) + ".txt"))
                {
                    fs.Write(str);
                    
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine("SaveContents IO" + ioe.Message + " path=" + path);
            }
            //delegate
            //if (ContentsSaved != null)
            //{
            //    ContentsSaved(path, url);
            //}
            bookName.Remove(url);
        }
        private static void SaveContents(int count)
        {

            string direc = AppDomain.CurrentDomain.BaseDirectory + "/count";
            if (!Directory.Exists(direc))
                Directory.CreateDirectory(direc);
            try
            {
                using (StreamWriter fs = new StreamWriter(direc + "/" + "count.txt"))
                {
                    fs.Write(count.ToString());
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine("count IO" + ioe.Message + " path=" + direc);
            }
            //delegate
            //if (ContentsSaved != null)
            //{
            //    ContentsSaved(path, url);
            //}
        }
        private static readonly char[] InvalidFileNameChars = new[]

        {

        '"',

        '<',

        '>',

        '|',

        '\0',

        '\u0001',

        '\u0002',

        '\u0003',

        '\u0004',

        '\u0005',

        '\u0006',

        '\a',

        '\b',

        '\t',

        '\n',

        '\v',

        '\f',

        '\r',

        '\u000e',

        '\u000f',

        '\u0010',

        '\u0011',

        '\u0012',

        '\u0013',

        '\u0014',

        '\u0015',

        '\u0016',

        '\u0017',

        '\u0018',

        '\u0019',

        '\u001a',

        '\u001b',

        '\u001c',

        '\u001d',

        '\u001e',

        '\u001f',

        ':',

        '*',

        '?',

        '\\',

        '/'

        };

        //过滤方法

        public static string CleanInvalidFileName(string fileName)
        {

            fileName = fileName + "";

            fileName = InvalidFileNameChars.Aggregate(fileName, (current, c) => current.Replace(c + "", ""));



            if (fileName.Length > 1)

                if (fileName[0] == '.')

                    fileName = "dot" + fileName.TrimStart('.');



            return fileName;

        }
    }
}
