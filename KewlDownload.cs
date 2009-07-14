using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace KewlDownloader
{
    public partial class KewlDownload : UserControl
    {
        private readonly string filename;
        private readonly string fullPath;
        private readonly Uri url;

        private WebClient client;

        public KewlDownload(Uri url, string destDir)
        {
            InitializeComponent();

            this.url = url;

            client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;

            filename = Path.GetFileName(url.LocalPath);
            fullPath = destDir + Path.DirectorySeparatorChar + filename;
            filenameLabel.Text = filename;
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            BackColor = Color.LightGreen;
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        public void Start()
        {
            client.DownloadFileAsync(url, fullPath);
        }
    }
}