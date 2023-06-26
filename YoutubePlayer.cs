using Microsoft.Web.WebView2.Core;

namespace YoutubePlayer
{
	public partial class YoutubePlayer : Form
	{
		public int count { get; set; } = 0;
		public Uri uri { get; set; }

		public YoutubePlayer()
		{
			InitializeComponent();
		}

		public YoutubePlayer(string uri)
		{
			InitializeComponent();

			// Start Video
			webView21.NavigationCompleted += WebView_NavigationCompleted;

			string embedURL = "https://www.youtube.com/embed/{0}";
			string URL = uri;

			// Use embed feature.
			//if (uri.Contains("/shorts"))
			//{
			//	URL = string.Format(embedURL, textBox1.Text.Split("/shorts/")[1]);
			//}
			//else
			//{
			//	URL = string.Format(embedURL, textBox1.Text.Split("v=")[1]);
			//}

			this.webView21.Source = new Uri(URL);
		}

		private void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
		{
			count++;
			if (e.IsSuccess && count >= 2)
			{
				webView21.ExecuteScriptAsync("document.getElementsByClassName('ytp-large-play-button')[0].click();");
				this.Text = webView21.CoreWebView2.DocumentTitle;
				//Task.Delay(1000);
				webView21.ExecuteScriptAsync("document.getElementsByClassName('ytp-mute-button')[0].click();");
			}
		}

		private async void YoutubePlayer_FormClosing(object sender, FormClosingEventArgs e)
		{
			CoreWebView2Profile profile;
			webView21.CoreWebView2.CookieManager.DeleteAllCookies();
			if (webView21.CoreWebView2 != null)
			{
				profile = webView21.CoreWebView2.Profile;
				// Get the current time, the time in which the browsing data will be cleared
				// until.
				await profile.ClearBrowsingDataAsync();
			}
		}
	}
}