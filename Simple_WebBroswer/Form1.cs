using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_WebBroswer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In this programme, I have try to create very simple and basic web broswer using C#..!"); 
        }

        public void NavigateToPage()
        {
            webBrowser1.Navigate(textBox1.Text);
            button1.Enabled = false;
            textBox1.Enabled = false;
            toolStripStatusLabel1.Text = "Navigation started...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigateToPage();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                NavigateToPage();
                //button1_Click(null, null);
            }
        }

        private void webBrowser1_FileDownload(object sender, EventArgs e)
        {
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            button1.Enabled = true;
            textBox1.Enabled = true;
            toolStripStatusLabel1.Text = "Navigation Completed";

            foreach(HtmlElement image in webBrowser1.Document.Images)
            {
                image.SetAttribute("src", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fupload.wikimedia.org%2Fwikipedia%2Fcommons%2Fthumb%2F7%2F73%2FLion_waiting_in_Namibia.jpg%2F1200px-Lion_waiting_in_Namibia.jpg&tbnid=18OQpH7P44zbJM&vet=12ahUKEwjEyOiFqfGDAxV2mycCHYFTDhgQMygAegQIARBz..i&imgrefurl=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FLion&docid=0P9ZPIi_HU4dMM&w=1200&h=900&itg=1&q=lion&ved=2ahUKEwjEyOiFqfGDAxV2mycCHYFTDhgQMygAegQIARBz");
            }
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0)
            {
                toolStripProgressBar1.ProgressBar.Value = (int)(e.CurrentProgress / e.MaximumProgress) * 100; 
            }
        }
    }
}