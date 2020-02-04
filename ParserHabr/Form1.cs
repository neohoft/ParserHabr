using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParserHabr.work;

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
            var parser = new Parser("https://habr.com/ru/hub/csharp/", "33", "95");
            parsing = parser.ParsTover();


            foreach(var post in parsing)
            {
                dataGridView1.Rows.Add(post.Key.Substring(5, post.Key.Length - 6),
                        post.Value.Replace(" ", ""));
            }


        }

        private void Wrap()
        {
            
        }
    }
}
