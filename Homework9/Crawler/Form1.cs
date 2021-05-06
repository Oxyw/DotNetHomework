using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Crawler
{
    public partial class Form1 : Form
    {
        BindingSource bdsResult = new BindingSource();
        Crawler crawler = new Crawler();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = bdsResult;
            crawler.CrawlerDownloaded += PageDownloaded;
            crawler.CrawlerStopped += Stop;
            crawler.CrawlerInfo += ShowInfo;
        }

        private void ShowInfo(Crawler crawler, string url)
        {
            Action action = () => CrawlerInfo.Text = "正在爬取" + url;
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void Stop(Crawler obj)
        {
            Action action = () => CrawlerInfo.Text = "爬虫已停止";
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void PageDownloaded(Crawler crawler, string url, string info)
        {
            var pageInfo = new { Index = bdsResult.Count + 1, URL = url, Status = info };
            Action action = () => { bdsResult.Add(pageInfo); };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bdsResult.Clear();
            crawler.StartUrl = txtUrl.Text;
            try
            {
                crawler.MaxNum = int.Parse(txtNum.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("请输入正确的整数！");
            }

            new Thread(crawler.Crawl).Start();   //开始爬行
            CrawlerInfo.Text = "爬虫已启动....";
        }
    }
}
