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
            chromeBrowser.ExecuteScriptAsync($"document.getElementById('emailForSignIn').value = '{txtUsername.Text}';");
            await LoadPageAsync(chromeBrowser);
            chromeBrowser.ExecuteScriptAsync($"document.getElementById('passwordForSignIn').value = '{txtPassword.Text}';");
            await LoadPageAsync(chromeBrowser);
            chromeBrowser.ExecuteScriptAsync("document.getElementsByName('login')[0].click()");
            await LoadPageAsync(chromeBrowser);
            await LoopThroughEachSolution();
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
            var buttonNext = htmlDoc.DocumentNode.Descendants().FirstOrDefault(_ => _.Name.Equals("button") && _.GetAttributeValue("data-hover", "").Equals("Next"));
            if (buttonNext != null)
            {
                chromeBrowser.ExecuteScriptAsync("document.querySelectorAll('[data-hover=Next]')[0].click();");
                await LoadPageAsync(chromeBrowser);
                return true;
            }
            return false;
        }

        public async Task Crawl()
        {
            int wait = 0;
            do
            {
                var source = await chromeBrowser.GetSourceAsync();
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(source);

                var title = htmlDoc.DocumentNode.Descendants().Where(_ => _.Name.Equals("h3")).FirstOrDefault(_ => _.HasClass("title"))?.InnerText;

                var node = htmlDoc.DocumentNode.Descendants().Where(_ => _.Name.Equals("ol")).FirstOrDefault(_ => _.HasClass("steps"));
                if (title != null && node != null)
                {
                    WriteToFile($"{txtResultFolderPath.Text}\\{title}.html", node.OuterHtml);
                    break;
                }
                else
                {
                    var noSolutionBtn = htmlDoc.DocumentNode.Descendants().Where(_ => _.Name.Equals("button")).FirstOrDefault(_ => _.HasClass("simple-button", "cta"));

                    if (noSolutionBtn != null)
                    {
                        WriteToFile($"{txtResultFolderPath.Text}\\{title}-nosolution.html", "no solution");
                        break;
                    }

                    wait++;
                    await Task.Delay(2000);
                }
            }
            while (wait <= 10);
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
