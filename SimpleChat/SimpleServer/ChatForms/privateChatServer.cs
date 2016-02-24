using System.Linq;
using System.Windows.Forms;

namespace SimpleServer.ChatForms
{
    public partial class PrivateChat : Form
    {
        private readonly ServerChat serverChat;

        public PrivateChat(ServerChat server)
        {
            InitializeComponent();
            serverChat = server;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (textBoxInput.Text != string.Empty)
            {
                foreach (var client in from ListViewItem item in serverChat.clientList.SelectedItems select (Client) item.Tag)
                {
                   client.Send("Personal Message : " + textBoxInput.Text);
                }
                textBoxReceive.Text += "Server says : " + textBoxInput.Text + "\r\n";
                textBoxInput.Text = string.Empty;
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
            textBoxReceive.SelectionStart = textBoxReceive.TextLength;
        }
    }
}
