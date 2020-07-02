using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Internals;
using CefSharp.WinForms;
using HtmlAgilityPack;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public ChromiumWebBrowser chromeBrowser;

        public async void InitializeChromium()
        {

            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(txtUrl.Text);
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            await LoadPageAsync(chromeBrowser);
            if (!cbFBLogin.Checked)
            {
                await LoginThroughNormalWay();
            }
            else
            {
                await LoginThroughFacebook();
            }
            await LoadPageAsync(chromeBrowser);
            await LoopThroughEachSolution();
        }

        private async Task LoginThroughNormalWay()
        {
            chromeBrowser.ExecuteScriptAsync($"document.getElementById('emailForSignIn').value = '{txtUsername.Text}';");
            await LoadPageAsync(chromeBrowser);
            chromeBrowser.ExecuteScriptAsync($"document.getElementById('passwordForSignIn').value = '{txtPassword.Text}';");
            await LoadPageAsync(chromeBrowser);
            chromeBrowser.ExecuteScriptAsync("document.getElementsByName('login')[0].click()");
            await LoadPageAsync(chromeBrowser);
        }

        private async Task LoginThroughFacebook()
        {
            var wait = true;
            var startTime = DateTime.Now;
            while(wait)
            {
                wait = !(await gotIn());

                if (DateTime.Now.Subtract(startTime).TotalMinutes >= 5)
                {
                    MessageBox.Show("Couldn't login!");
                    break;
                }
            }
        }

        public Task LoadPageAsync(IWebBrowser browser)
        {
            var tcs = new TaskCompletionSource<bool>();

            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                if (!args.IsLoading)
                {
                    browser.LoadingStateChanged -= handler;
                    tcs.TrySetResultAsync(true);
                }
            };

            browser.LoadingStateChanged += handler;
            return tcs.Task;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public async Task LoopThroughEachSolution()
        {
            bool result = true;
            do
            {
                await Crawl();
                result = await NextPage();
            }
            while (result);
        }

        public async Task<bool> NextPage()
        {
            var source = await chromeBrowser.GetSourceAsync();
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(source);
            var buttonNext = GetButtonNext(htmlDoc);
            if (buttonNext != null)
            {
                chromeBrowser.ExecuteScriptAsync("document.querySelectorAll('[data-hover=Next]')[0].click();");
                await LoadPageAsync(chromeBrowser);
                return true;
            }
            return false;
        }

        private static HtmlNode GetButtonNext(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.Descendants().FirstOrDefault(_ => _.Name.Equals("button") && _.GetAttributeValue("data-hover", "").Equals("Next"));
        }

        public async Task Crawl()
        {
            int wait = 0;
            do
            {
                var source = await chromeBrowser.GetSourceAsync();
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(source);

                var canCrawl = await CanCrawl();
                if (!canCrawl)
                {
                    break;
                }

                var title = GetTitle(htmlDoc);

                var node = GetStepsList(htmlDoc);
                string chapterText = GetChapterText(htmlDoc);
                System.IO.Directory.CreateDirectory($"{txtResultFolderPath.Text}\\{chapterText}");
                if (title != null && node != null)
                {
                    WriteToFile($"{txtResultFolderPath.Text}\\{chapterText}\\{title}.html", node.OuterHtml);
                    break;
                }
                else
                {
                    var noSolutionBtn = htmlDoc.DocumentNode.Descendants().Where(_ => _.Name.Equals("button")).FirstOrDefault(_ => _.HasClass("simple-button", "cta"));

                    if (noSolutionBtn != null)
                    {
                        WriteToFile($"{txtResultFolderPath.Text}\\{chapterText}\\{title}-nosolution.html", "no solution");
                        break;
                    }

                    wait++;
                    await Task.Delay(2000);
                }
            }
            while (wait <= 10);
        }

        private static string GetChapterText(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.Descendants()
                .FirstOrDefault(_ => _.Name.Equals("li") && _.HasClass("chapter", "current"))
                .Descendants()
                .Where(_ => _.Name.Equals("h2")).FirstOrDefault(_ => _.GetAttributeValue("aria-pressed", "").Equals("true")).InnerText;
        }

        private HtmlNode GetStepsList(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.Descendants().Where(_ => _.Name.Equals("ol")).FirstOrDefault(_ => _.HasClass("steps"));
        }

        private string GetTitle(HtmlAgilityPack.HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.Descendants().Where(_ => _.Name.Equals("h2")).FirstOrDefault(_ => _.HasClass("title"))?.InnerText;
        }

        private async Task<bool> gotIn()
        {
            var source = await chromeBrowser.GetSourceAsync();
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(source);

            if (htmlDoc.DocumentNode.Descendants().FirstOrDefault(_ => _.HasClass("capp-promo-close-button")) != null)
            {
                chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('capp-promo-close-button')[0].click()");
            }

            var title = GetTitle(htmlDoc);
            if(String.IsNullOrWhiteSpace(title))
            {
                return false;
            }
            return true;
        }

        private async Task<bool> CanCrawl()
        {
            var source = await chromeBrowser.GetSourceAsync();
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(source);

            if (htmlDoc.DocumentNode.Descendants().FirstOrDefault(_ => _.HasClass("capp-promo-close-button")) != null)
            {
                chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('capp-promo-close-button')[0].click()");
            }

            if (htmlDoc.DocumentNode.Descendants().FirstOrDefault(_ => _.HasClass("error-msg-lg") && _.GetAttributeValue("style", "").Equals("")) != null)
            {
                MessageBox.Show("Invalid username or password. Try again");
                return false;
            }

            var noAccess = htmlDoc.DocumentNode.Descendants().FirstOrDefault(_ => _.Id.Equals("csresubscribemodal"));
            if (noAccess != null)
            {
                MessageBox.Show("You need access to your solutions in order to crawl");
                return false;
            }

            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void WriteToFile(string filePath, string content)
        {
            using (FileStream fs = File.Create(filePath))
            {
                // writing data in string
                string dataasstring = content;
                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
                fs.Write(info, 0, info.Length);

                // writing data in bytes already
                byte[] data = new byte[] { 0x0 };
                fs.Write(data, 0, data.Length);
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            InitializeChromium();
        }

        private void txtResultFolderPath_TextChanged(object sender, EventArgs e)
        {

        }
    }

    static class Extensions
    {
        public static bool HasClass(this HtmlNode node, params string[] classValueArray)
        {
            var classValue = node.GetAttributeValue("class", "");
            var classValues = classValue.Split(' ');
            return classValueArray.All(c => classValues.Contains(c));
        }
    }
}
