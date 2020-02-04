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
        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var parser = new Parser("https://habr.com/ru/hub/csharp/", "1", "95");
            // Thread myThread = new Thread(new ThreadStart(parser.ParsTover));
            // myThread.Start();
            parser.ParsTover();

        }

        private void Wrap()
        {
            var parser = new Parser("https://habr.com/ru/hub/csharp/", "1", "95");
            parser.ParsTover();
        }
    }
}
