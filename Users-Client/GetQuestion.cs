using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Users_Client
{
    public partial class GetQuestion : Form
    {
        public GetQuestion()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = new Thread(() => Application.Run(new MainMenu()));
            t.Start();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("are you sure?", "make suer", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            string url = "http://"+ DataSaver.address + "/questions/get";
            var wb = new WebClient();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("team_code", textBox1.Text);
            keyValuePairs.Add("username", DataSaver.username);
            keyValuePairs.Add("password", DataSaver.password);
            keyValuePairs.Add("question_code",textBox2.Text);
            string inp = JsonConvert.SerializeObject(keyValuePairs);
            var resp = wb.UploadString(url, "POST", inp);
            Dictionary<string, string> resp2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(resp);
            label3.Text = resp2["message"];
        }
    }
}
