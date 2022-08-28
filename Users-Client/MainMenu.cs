using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Users_Client
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSaver.username = "";
            DataSaver.password = "";
            var t = new Thread(() => Application.Run(new Login()));
            t.Start();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = new Thread(() => Application.Run(new GiveQuestion()));
            t.Start();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var t = new Thread(() => Application.Run(new GetQuestion()));
            t.Start();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var t = new Thread(() => Application.Run(new PutScore()));
            t.Start();
            this.Close();
        }
    }
}
