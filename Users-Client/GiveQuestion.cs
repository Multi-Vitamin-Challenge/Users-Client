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
    public partial class GiveQuestion : Form
    {
        new BindingList<Type> question_types = new BindingList<Type>();
        public GiveQuestion()
        {
            InitializeComponent();
            dataGridView1.DataSource = question_types;
            string url = "http://192.168.1.2:5000/structure";
            var wb = new WebClient();
            var resp = wb.DownloadString(url);
            var resp2 = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(resp);
            foreach (string x in resp2.Keys){
                question_types.Add(new Type(x));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var t = new Thread(() => Application.Run(new MainMenu()));
            t.Start();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                DialogResult dialogResult = MessageBox.Show("are you sure?", "make suer", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                string url = "http://192.168.1.2:3000/questions/give";
                var wb = new WebClient();
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("team_code", textBox1.Text);
                keyValuePairs.Add("username", DataSaver.username);
                keyValuePairs.Add("password", DataSaver.password);
                keyValuePairs.Add("question_type", question_types[dataGridView1.SelectedRows[i].Index].full);
                string inp = JsonConvert.SerializeObject(keyValuePairs);
                var resp = wb.UploadString(url, "POST", inp);
                Dictionary<string, string> resp2 = JsonConvert.DeserializeObject<Dictionary<string, string>>(resp);
                label2.Text = resp2["message"];
                break;
            }
        }
    }
    public class Type
    {
        public Type(string full)
        {
            this.full = full;
            string[] temp = full.Split("#");
            Name = temp[0];
            Level = temp[1];
            Cost = temp[2];
        }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Cost { get; set; }

        public string full;
    }
}
