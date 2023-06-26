using System.Security.Policy;
using YoutubePlayer;

namespace YoutubeViewer
{
	public partial class YoutubeURL : Form
	{
		public List<YoutubePlayer.YoutubePlayer> Players { get; set; }

		public YoutubeURL()
		{
			InitializeComponent();
			this.Text = "Youtube URL";
			Players = new List<YoutubePlayer.YoutubePlayer>();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;
			var urlList = new List<string>();

			// Add all URL's to list
			for (int i = 1; i <= 12; i++)
			{
				var name = string.Format("textBox{0}", i);
				var textBox = this.Controls[name] as TextBox;

				if (!string.IsNullOrEmpty(textBox.Text))
				{
					urlList.Add(textBox.Text);
				}
			}

			if (!string.IsNullOrEmpty(textBox13.Text))
			{
				urlList = textBox13.Text.Split('\n').ToList<string>();
			}

			// Verify all URL's in the list
			int c = 0;
			foreach (var url in urlList)
			{
				c++;
				if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
				{
					InvalidURL(url, c);
					button1.Enabled = true;
					return;
				}
			}

			// Start players
			foreach (var url in urlList)
			{
				if (!string.IsNullOrEmpty(url))
				{
					await PlayVideo(url);
					await Task.Delay((int)numericUpDown1.Value);
				}
			}

			// if no URL is entered
			if (Players.Count == 0)
			{
				string message = "Please enter atleast 1 URL.";
				string title = "Information";
				MessageBox.Show(message, title);
				button1.Enabled = true;
			}
		}

		private Task PlayVideo(string url)
		{
			var player = new YoutubePlayer.YoutubePlayer(url.Trim());
			if (showVideoPlayback.Checked)
			{
				player.Show();
			}
			Players.Add(player);
			return Task.CompletedTask;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int closedPlayersCount = 0;
			foreach (var player in Players)
			{
				player.Close();
				closedPlayersCount++;
			}

			if (Players.Count == closedPlayersCount)
			{
				this.Close();
			}
			else
			{
				string message = "Failed to close all opened players!\r\nPlease close them manually.";
				string title = "Error";
				MessageBox.Show(message, title);
				this.Close();
			}
		}

		private void YoutubeURL_FormClosing(object sender, FormClosingEventArgs e)
		{
			int closedPlayersCount = 0;
			foreach (var player in Players)
			{
				player.Close();
				closedPlayersCount++;
			}

			if (Players.Count != closedPlayersCount)
			{
				string message = "Failed to close all opened players!\r\nPlease close them manually.";
				string title = "Error";
				MessageBox.Show(message, title);
			}
		}

		private void InvalidURL(string url, int urlNumber)
		{
			string message = string.Format("Invalid URL no. {0} : " + url, urlNumber);
			string title = "Error";
			MessageBox.Show(message, title);
		}

		#region Paste Buttons click methods

		private void paste_button1_Click(object sender, EventArgs e)
		{
			textBox1.Text = Clipboard.GetText();
		}

		private void paste_button2_Click(object sender, EventArgs e)
		{
			textBox2.Text = Clipboard.GetText();
		}

		private void paste_button3_Click(object sender, EventArgs e)
		{
			textBox3.Text = Clipboard.GetText();
		}

		private void paste_button4_Click(object sender, EventArgs e)
		{
			textBox4.Text = Clipboard.GetText();
		}

		private void paste_button5_Click(object sender, EventArgs e)
		{
			textBox5.Text = Clipboard.GetText();
		}

		private void paste_button6_Click(object sender, EventArgs e)
		{
			textBox6.Text = Clipboard.GetText();
		}

		private void paste_button7_Click(object sender, EventArgs e)
		{
			textBox7.Text = Clipboard.GetText();
		}

		private void paste_button8_Click(object sender, EventArgs e)
		{
			textBox8.Text = Clipboard.GetText();
		}

		private void paste_button9_Click(object sender, EventArgs e)
		{
			textBox9.Text = Clipboard.GetText();
		}

		private void paste_button10_Click(object sender, EventArgs e)
		{
			textBox10.Text = Clipboard.GetText();
		}

		private void paste_button11_Click(object sender, EventArgs e)
		{
			textBox11.Text = Clipboard.GetText();
		}

		private void paste_button12_Click(object sender, EventArgs e)
		{
			textBox12.Text = Clipboard.GetText();
		}

		private void paste_button13_Click(object sender, EventArgs e)
		{
			textBox13.Text = Clipboard.GetText();
		}

		#endregion Paste Buttons click methods

		#region Clear Buttons click methods

		private void clear_button1_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
		}

		private void clear_button2_Click(object sender, EventArgs e)
		{
			textBox2.Text = "";
		}

		private void clear_button3_Click(object sender, EventArgs e)
		{
			textBox3.Text = "";
		}

		private void clear_button4_Click(object sender, EventArgs e)
		{
			textBox4.Text = "";
		}

		private void clear_button5_Click(object sender, EventArgs e)
		{
			textBox5.Text = "";
		}

		private void clear_button6_Click(object sender, EventArgs e)
		{
			textBox6.Text = "";
		}

		private void clear_button7_Click(object sender, EventArgs e)
		{
			textBox7.Text = "";
		}

		private void clear_button8_Click(object sender, EventArgs e)
		{
			textBox8.Text = "";
		}

		private void clear_button9_Click(object sender, EventArgs e)
		{
			textBox9.Text = "";
		}

		private void clear_button10_Click(object sender, EventArgs e)
		{
			textBox10.Text = "";
		}

		private void clear_button11_Click(object sender, EventArgs e)
		{
			textBox11.Text = "";
		}

		private void clear_button12_Click(object sender, EventArgs e)
		{
			textBox12.Text = "";
		}

		private void clear_button13_Click(object sender, EventArgs e)
		{
			textBox13.Text = "";
		}

		#endregion Clear Buttons click methods
	}
}