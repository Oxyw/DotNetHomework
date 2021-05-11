using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ParallelCrawler
{
    class ParallelCrawler
    {
        public event Action<ParallelCrawler> CrawlerStopped;
        public event Action<ParallelCrawler, string> CrawlerInfo;
        public event Action<ParallelCrawler, int, string, string> CrawlerDownloaded;

        public string StartUrl { get; set; } //起始URL
        public int MaxNum { get; set; } //最大数量

        public static readonly string urlDetectRegex = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";   //URL检测表达式，在HTML文本中查找URL
        public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";   //URL解析表达式

        //public Hashtable urls = new Hashtable();

		private readonly ConcurrentDictionary<string, bool> urls = new ConcurrentDictionary<string, bool>();

		private ConcurrentQueue<string> waitingQueue = new ConcurrentQueue<string>();

		public Encoding HtmlEncoding = Encoding.UTF8;

        private int count = 0;

		public void Crawl()
		{
			urls.Clear();
			//urls.Add(StartUrl, false);
			waitingQueue = new ConcurrentQueue<string>();
			waitingQueue.Enqueue(StartUrl);

			List<Task> tasks = new List<Task>();

			/*
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
				catch (Exception ex)
				{
					CrawlerDownloaded(this, current, "  Error:" + ex.Message);
				}
			 }
			 */

			while (tasks.Count < MaxNum)
			{
				if (waitingQueue.TryDequeue(out string url))
				{
					int index = tasks.Count;
					Task task = Task.Run(() => DownAndParse(url, index));
					tasks.Add(task);
					
				}
			}

			Task.WaitAll(tasks.ToArray());
			CrawlerStopped(this);
		}

		private void DownAndParse(string url, int index)
        {
			CrawlerInfo(this, url);
			try
			{
				string html = DownLoad(url, index);
				urls[url] = true;
				Parse(html, url);
				CrawlerDownloaded(this, index, url, "success");
				count++;
			}
			catch (Exception ex)
			{
				CrawlerDownloaded(this, index, url, "Error:" + ex.Message);
			}
		}

		public string DownLoad(string url, int index)
		{
			WebClient webClient = new WebClient();
			webClient.Encoding = HtmlEncoding;
			string html = webClient.DownloadString(url);
			File.WriteAllText(index + ".html", html, Encoding.UTF8);
			return html;
		}

		public void Parse(string html, string pageUrl)
		{
			MatchCollection matches = new Regex(urlDetectRegex).Matches(html);

			foreach (Match match in matches)
			{
				string linkUrl = match.Groups["url"].Value;
				if (linkUrl == null || linkUrl == "" || linkUrl.StartsWith("javascript:")) continue;
				linkUrl = FixUrl(linkUrl, pageUrl);

				if (!urls.ContainsKey(linkUrl))
				{
					waitingQueue.Enqueue(linkUrl);
					urls.TryAdd(linkUrl, false);
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
