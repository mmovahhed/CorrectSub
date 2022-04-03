using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CorrectSub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
        }

        private void textBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }
        public string[] files;
        public static string[] stringSeparators = new string[] { "\\" };
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
            files = null;
            files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  

            if (files != null && files.Any())
            {
                foreach (var item in files)
                {
                    string fileName = item.Split(stringSeparators, StringSplitOptions.None).Last();
                    textBox1.Text += "[" + fileName + "] ";
                    richTextBox1.Text += Environment.NewLine + fileName + " -->> imported!";
                }
                //textBox1.Text = files.First(); //select the first one
                //   richTextBox1.Text= files
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBox1.Text = dialog.FileName;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                foreach (var item in files)
                {
                    string fileName = item.Split(stringSeparators, StringSplitOptions.None).Last();
                    if (fileName.EndsWith(".txt") || fileName.EndsWith(".srt"))
                    {
                        string text = File.ReadAllText(item);
                        text = text.Replace("‫", " ");
                        File.WriteAllText(item, text);
                        richTextBox1.Text += Environment.NewLine + fileName + " -->> replacement sueccessed!";
                    }
                }

            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(textBox1.Text))
            //{
            //    string ext = Path.GetExtension(textBox1.Text);
            //    richTextBox1.Text = "file type: " + ext;

            //}
            //else
            //{
            //    richTextBox1.Text = "";
            //}
        }
    }
}
