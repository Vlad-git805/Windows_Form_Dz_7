using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreateMenu1
{
    public partial class Form1 : Form
    {
        Color c;

        TextBox CurrentTextBox
        {
            get
            {
                if (tabControl1.SelectedIndex != -1)
                {
                    return (TextBox)tabControl1.SelectedTab.Controls[0];
                }
                return null;
            }
        }

        string strCopy;
        int countOfLeter = 0;
        public Form1()
        {
            InitializeComponent();

            c = this.BackColor;

            timer1.Start();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem it = (ToolStripMenuItem) sender;
            if (it.Checked == true) 
            {
                this.BackColor = Color.Black;
            }
            else 
            {
                this.BackColor = c;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            open.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt||";
            open.FilterIndex = 2;
            open.CheckFileExists = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(open.OpenFile()))
                {
                    while (!reader.EndOfStream)
                    {
                        CurrentTextBox.Text += reader.ReadLine();
                        CurrentTextBox.Text += Environment.NewLine;
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }

        private void blackBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem it = (ToolStripMenuItem)sender;
            if (it.Checked == true)
            {
                CurrentTextBox.BackColor = Color.Black;
            }
            else 
            {
                CurrentTextBox.BackColor = Color.White;
            }
        }

        private void dublicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.CreatePrompt = true;
            save.DefaultExt = ".txt";
            save.OverwritePrompt = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.FileName);
                writer.Write(CurrentTextBox.Text);

                string str = null;
                for (int i = save.FileName.Length - 1; i >= 0; i--)
                {
                    if (save.FileName[i] != '\\')
                    {
                        str += save.FileName[i];
                    }
                    else
                    {
                        break;
                    }
                }
                char[] s = str.ToCharArray();
                Array.Reverse(s);
                str = new string(s);
                tabControl1.SelectedTab.Text = str;

                writer.Close();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.CreatePrompt = true;
            save.DefaultExt = ".txt";
            save.OverwritePrompt = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.FileName);
                writer.Close();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            strCopy = CurrentTextBox.Text;
        }

        private void pastleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTextBox.Text += strCopy;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTextBox.Text = "";
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTextBox.SelectedText = "";
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CurrentTextBox.ForeColor = Color.Yellow;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            CurrentTextBox.ForeColor = Color.Green;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            CurrentTextBox.ForeColor = Color.Red;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            CurrentTextBox.ForeColor = Color.Magenta;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                CurrentTextBox.ForeColor = color.Color;
            }
        }

        private void textFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                CurrentTextBox.BackColor = color.Color;
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();

            if (font.ShowDialog() == DialogResult.OK)
            {
                var selected = font.Font;
                CurrentTextBox.Font = selected;

            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            strCopy = CurrentTextBox.Text;    
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            CurrentTextBox.SelectedText = "";
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            CurrentTextBox.Text += strCopy;
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            CurrentTextBox.Text = "";
        }

        private void designerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            TabPage newPage = new TabPage($"New Tab {tabControl1.TabPages.Count + 1}");

            TextBox newTextBox = new TextBox();
            newTextBox.ContextMenuStrip = this.contextMenuStrip1;
            newTextBox.Location = new System.Drawing.Point(10, 10);
            newTextBox.Multiline = true;
            newTextBox.Name = "textBox1";
            newTextBox.Size = new System.Drawing.Size(630, 300);
            newTextBox.TabIndex = 1;

            newPage.Controls.Add(newTextBox);

            tabControl1.TabPages.Add(newPage);
        }

        private void closeTabToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                if(CurrentTextBox.Text == "")
                {
                    tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Do you want to save info?", "FLEKS", MessageBoxButtons.YesNo);
                    if(dialog == DialogResult.Yes)
                    {
                        dublicateToolStripMenuItem_Click(sender,e);
                        tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                    }
                    else
                    {
                        tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                toolStripStatusLabel4.Text = "0";
                countOfLeter = 0;
                toolStripStatusLabel2.Text = CurrentTextBox.Text.Length.ToString();

                foreach (var item in CurrentTextBox.Text)
                {
                    if ((item >= 65 && item <= 90) || (item >= 97 && item <= 122))
                    {
                        toolStripStatusLabel4.Text = (++countOfLeter).ToString();
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                countOfLeter = 0;
                toolStripStatusLabel2.Text = CurrentTextBox.Text.Length.ToString();

                foreach (var item in CurrentTextBox.Text)
                {
                    if ((item >= 65 && item <= 90) || (item >= 97 && item <= 122))
                    {
                        toolStripStatusLabel4.Text = (++countOfLeter).ToString();
                    }
                }
            }
        }
    }
}
