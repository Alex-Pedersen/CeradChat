using System.Linq;
using System.Windows.Forms;

namespace SimpleServer.ChatForms
{
    public partial class PrivateChat : Form
    {
        private readonly ServerChat serverChat;

        public PrivateChat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                foreach (var client in from ListViewItem item in serverChat.listView1.SelectedItems select (Client) item.Tag)
                {
                   client.Send("Personal Message : " + textBox2.Text);
                }
                textBox1.Text += "Server says : " + textBox2.Text + "\r\n";
                textBox2.Text = string.Empty;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick(); 
            }
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            textBox1.SelectionStart = textBox1.TextLength;
        }
    }
}
