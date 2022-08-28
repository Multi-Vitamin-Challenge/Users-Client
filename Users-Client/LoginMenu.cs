using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Users_Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox2.Text = "";
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string url = "http://"+ DataSaver.address + "/user/input";
            var wb = new WebClient();
            var user = new Input(textBox1.Text, textBox2.Text);
            string inp = JsonConvert.SerializeObject(user);
            var resp = wb.UploadString(url, "POST", inp);
            Output resp2 = JsonConvert.DeserializeObject<Output>(resp);
            if (resp2.status == "200")
            {
                DataSaver.username = textBox1.Text;
                DataSaver.password = textBox2.Text;
                var t = new Thread(() => Application.Run(new MainMenu()));
                t.Start();
                this.Close();
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                label1.Text = resp2.message;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSaver.address = textBox3.Text;
            DataSaver.printer_address = textBox4.Text;
        }
    }

    public class Input
    {
        public Input(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Output
    {
        public Output(string message, string status)
        {
           this.message = message;
           this.status = status;
        }
        public string message { get; set; }
        public string status { get; set; }
    }
}