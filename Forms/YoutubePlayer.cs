using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace YoutubePlayer
{
	public partial class YoutubePlayer : Form
	{
		public int count { get; set; } = 0;
		public string uriGlobal { get; set; }
		private int TimerInterval { get; set; }

		private static readonly Random rand = new Random();
		private static readonly object syncLock = new object();

		System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

		public YoutubePlayer()
		{
			InitializeComponent();
		}

		public YoutubePlayer(string uri, int timerInterval)
		{
			InitializeComponent();
			uriGlobal = uri;

			// Initializing Timer
			timer1 = new System.Windows.Forms.Timer();
			timer1.Interval = TimerInterval = timerInterval;
			timer1.Tick += new System.EventHandler(timer1_Tick);
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
			var player = new YoutubePlayer(uriGlobal, TimerInterval);
			player.Show();
			timer1.Stop();
			this.Close();
		}

		private async void YoutubePlayer_Load(object sender, EventArgs e)
		{
			timer1.Start();

			webView21.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
			//await webView21.EnsureCoreWebView2Async();
			await webView21.EnsureCoreWebView2Async(null);
		}

		private void WebView_CoreWebView2InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
		{
			// Start Video
			webView21.NavigationCompleted += WebView_NavigationCompleted;
			webView21.CoreWebView2.Settings.UserAgent = GetRandomUserAgent();

			string embedURL = "https://www.youtube.com/embed/{0}";
			string URL = uriGlobal;

			// Use embed feature.
			//if (uri.Contains("/shorts"))
			//{
			//	URL = string.Format(embedURL, textBox1.Text.Split("/shorts/")[1]);
			//}
			//else
			//{
			//	URL = string.Format(embedURL, textBox1.Text.Split("v=")[1]);
			//}

			this.webView21.CoreWebView2.Navigate(URL);
		}

		private void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
		{
			count++;
			if (e.IsSuccess /*&& (uriGlobal.Contains("/shorts/") || count >= 2)*/)
			{
				webView21.ExecuteScriptAsync("document.getElementsByClassName('ytp-large-play-button')[0].click();");
				this.Text = webView21.CoreWebView2.DocumentTitle;
				//await Task.Delay(1000);
				//webView21.ExecuteScriptAsync("document.getElementsByClassName('ytp-mute-button')[0].click();");
				if (!webView21.CoreWebView2.IsMuted)
				{
					webView21.CoreWebView2.IsMuted = true;
				}
			}
		}

		private async void YoutubePlayer_FormClosing(object sender, FormClosingEventArgs e)
		{
			CoreWebView2Profile profile;
			webView21.CoreWebView2.CookieManager.DeleteAllCookies();
			if (webView21.CoreWebView2 != null)
			{
				profile = webView21.CoreWebView2.Profile;
				// browsing data will be cleared
				await profile.ClearBrowsingDataAsync();
			}
		}

		private static string GetRandomUserAgent()
		{
			string userAgent = "";
			var browserType = new string[] { "chrome", "firefox", };
			var UATemplate = new Dictionary<string, string> { { "chrome", "Mozilla/5.0 ({0}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{1} Safari/537.36" }, { "firefox", "Mozilla/5.0 ({0}; rv:{1}.0) Gecko/20100101 Firefox/{1}.0" }, };
			var OS = new string[] { "Windows NT 10.0; Win64; x64", "X11; Linux x86_64", "Macintosh; Intel Mac OS X 12_4" };
			string OSsystem = "";
			lock (syncLock)
			{ // synchronize
				OSsystem = OS[rand.Next(OS.Length)];
				int version = rand.Next(93, 104);
				int minor = 0;
				int patch = rand.Next(4950, 5162);
				int build = rand.Next(80, 212);
				string randomBroswer = browserType[rand.Next(browserType.Length)];
				string browserTemplate = UATemplate[randomBroswer];
				string finalVersion = version.ToString();
				if (randomBroswer == "chrome")
				{
					finalVersion = String.Format("{0}.{1}.{2}.{3}", version, minor, patch, build);
				}

				userAgent = String.Format(browserTemplate, OSsystem, finalVersion);
			}

			return userAgent;
		}
	}
}