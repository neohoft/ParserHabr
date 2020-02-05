using ParserHabr.work;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;


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
            //CheckForIllegalCrossThreadCalls = false;
            new Thread(Wrap).Start();
        }

        private void Wrap()
        {
            var parser = new Parser(textBox1.Text, numericUpDown1.Text, numericUpDown2.Text);
            parsing = parser.ParsTover();


            foreach (var post in parsing)
            {
                Invoke((MethodInvoker) (() => dataGridView1.Rows.Add(post.Key.Substring(5, post.Key.Length - 6),
                        post.Value.Replace(" ", ""))));
                
            }
        }
    }
}