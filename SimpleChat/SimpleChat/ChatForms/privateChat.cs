using System.Windows.Forms;

namespace SimpleClient.ChatForms
{
    public partial class PrivateChat : Form
    {
        public PublicChat PChat;

        public PrivateChat(PublicChat publicChat)
        {
            InitializeComponent();
            PChat = publicChat;
        }

        private void buttonSend_Click(object sender, System.EventArgs e)
        {
            if (textBoxMessage.Text != string.Empty)
            {
                string user = Text.Split('-')[1];
                PChat.LoginChat.ClientSettings.Send("User : " + user + "Message : " + textBoxMessage.Text);
                textBoxChat.Text += user + "Says : " + textBoxMessage.Text + "\r\n";
                textBoxMessage.Text = string.Empty;
            }
        }

        private void PrivateChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSend.PerformClick();
            }
        }

        private void textBoxChat_TextChanged(object sender, System.EventArgs e)
        {
            textBoxChat.SelectionStart = textBoxChat.TextLength;
        }
    }
}
