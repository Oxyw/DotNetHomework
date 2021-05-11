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

namespace ParallelCrawler
{
    public partial class Form1 : Form
    {
        BindingSource bdsResult = new BindingSource();
        ParallelCrawler crawler = new ParallelCrawler();
        Thread thread = null;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = bdsResult;
            crawler.CrawlerDownloaded += PageDownloaded;
            crawler.CrawlerStopped += Stop;
            crawler.CrawlerInfo += ShowInfo;
        }

        private void ShowInfo(ParallelCrawler crawler, string url)
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

        private void Stop(ParallelCrawler obj)
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

        private void PageDownloaded(ParallelCrawler crawler, int index, string url, string info)
        {
            var pageInfo = new { Index = index, URL = url, Status = info };
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

            if (thread != null)
            {
                thread.Abort();
            }
            thread = new Thread(crawler.Crawl);
            thread.Start();
            CrawlerInfo.Text = "爬虫已启动....";
        }
    }
}
