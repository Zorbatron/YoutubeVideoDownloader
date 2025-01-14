﻿using System;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace YTVideoDownloader
{
    public partial class MainWindow : Window
    {
        private Process processID;

        static string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\Youtube Video Downloader\%(title)s.%(ext)s";
        static string directory = Directory.GetCurrentDirectory();
        static string fileName = Path.Combine(directory, @"yt-dlp\yt-dlp.exe");

        public MainWindow()
        {
            InitializeComponent();

            webLink.Text = Clipboard.GetText();

            switch (File.Exists(fileName))
            {
                case false:
                    MessageBox.Show("yt-dlp wasn't found, read readme.md");
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            DownloadVideo();
        }

        private void webLink_KeyDown(object sender, KeyEventArgs key)
        {
            if (key.Key == Key.Enter)
            {
                DownloadVideo();
            }
        }

        private void DownloadVideo()
        {

            try
            {
                if (mp4.IsChecked == true)
                {
                    Process.Start("cmd.exe", $"/c \"\"{fileName}\" --output \"{downloadsFolder}\" -f bestvideo+bestaudio --merge-output-format mp4 \"{webLink.Text}\"\"");
                }

                if (mp3.IsChecked == true)
                {
                    Process.Start("cmd.exe", $"/c \"\"{fileName}\" --output \"{downloadsFolder}\" -x --audio-format mp3 \"{webLink.Text}\"\"");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void OpenQualWin_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = $"/k \"\"{fileName}\" -F \"{webLink.Text}\"\"";
            p.Start();

            processID = p; 
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                if (processID.HasExited == false)
                {
                    processID.Kill();
                }
            }
            catch 
            {
                //hi
            }
        }

        private void DownloadQualBtn_Click(object sender, RoutedEventArgs e)
        {
            DownloadQuality();
        }

        private void qualityTextBox_KeyDown(object sender, KeyEventArgs key)
        {
            if (key.Key == Key.Enter)
            {
                DownloadQuality();
            }
        }

        private void DownloadQuality()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = $"/c \"\"{fileName}\" -o \"{downloadsFolder}\" -f \"{qualityTextBox.Text}\" \"{webLink.Text}\"\"";
            p.Start();
        }
    }
}