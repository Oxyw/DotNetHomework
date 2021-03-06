using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Crawler
{
    class Crawler
    {
		public event Action<Crawler> CrawlerStopped;
		public event Action<Crawler, string> CrawlerInfo;
		public event Action<Crawler, string, string> CrawlerDownloaded;
		public string StartUrl { get; set; } //起始URL
        public int MaxNum { get; set; } //最大数量

		public Hashtable urls = new Hashtable();
		public static readonly string urlDetectRegex = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";   //URL检测表达式，在HTML文本中查找URL
		public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";   //URL解析表达式

		public Crawler()
        {
        }
		private int count = 0;

		public void Crawl()
        {
			urls.Clear();
			urls.Add(StartUrl, false);   //加入初始页面
			while (count < MaxNum)
			{
				string current = null;
				foreach (string url in urls.Keys)   //找到一个还没有下载过的链接
				{
					if ((bool)urls[url]) continue;   //已经下载过的,不再下载
					current = url;
				}
				if (current == null) break;
				CrawlerInfo(this, current);
                try
                {
					string html = DownLoad(current);   //下载
					urls[current] = true;
					CrawlerDownloaded(this, current, "success");
					count++;
					Parse(html, current);   //解析,并加入新的链接
				}
				catch(Exception ex)
                {
					CrawlerDownloaded(this, current, "  Error:" + ex.Message);
				}
			}
			CrawlerStopped(this);
		}

		public string DownLoad(string url)
		{
			WebClient webClient = new WebClient();
			webClient.Encoding = Encoding.UTF8;
			string html = webClient.DownloadString(url);
			string fileName = count.ToString();
			File.WriteAllText(fileName, html, Encoding.UTF8);
			return html;
		}

		public void Parse(string html, string pageUrl)
		{
			//string strRef = @"(href|HREF)[]* =[]* [""'][^""'#>] + [""']";
			MatchCollection matches = new Regex(urlDetectRegex).Matches(html);
			/*
			foreach (Match match in matches)
			{
				string strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"','\"','#',' ','>');
                if (strRef.Length == 0) continue;
				if (urls[strRef] == null)
					urls[strRef] = false;
			}
			*/
			foreach (Match match in matches)
			{
				string linkUrl = match.Groups["url"].Value;
				if (linkUrl == null || linkUrl == "" || linkUrl.StartsWith("javascript:")) continue;
				linkUrl = FixUrl(linkUrl, pageUrl);
				if (urls[linkUrl] == null)
				{
					urls[linkUrl] = false;
				}
			}
		}

		//转换为完整路径
		static private string FixUrl(string url, string pageUrl)
		{
			if (url.Contains("://"))
			{
				return url;
			}
			if (url.StartsWith("//"))
			{
				return "http:" + url;
			}
			if (url.StartsWith("/"))
			{
				Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
				String site = urlMatch.Groups["site"].Value;
				return site.EndsWith("/") ? site + url.Substring(1) : site + url;
			}
			if (url.StartsWith("../"))
			{
				url = url.Substring(3);
				int idx = pageUrl.LastIndexOf('/');
				return FixUrl(url, pageUrl.Substring(0, idx));
			}
			if (url.StartsWith("./"))
			{
				return FixUrl(url.Substring(2), pageUrl);
			}

			int end = pageUrl.LastIndexOf("/");
			return pageUrl.Substring(0, end) + "/" + url;
		}
	}
}