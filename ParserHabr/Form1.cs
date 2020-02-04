using ParserHabr.work;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ParserHabr
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> parsing = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var parser = new Parser(textBox1.Text, numericUpDown1.Text, numericUpDown2.Text);
            parsing = parser.ParsTover();


            foreach(var post in parsing)
            {
                dataGridView1.Rows.Add(post.Key.Substring(5, post.Key.Length - 6),
                        post.Value.Replace(" ", ""));
            }


        }

    }
}
