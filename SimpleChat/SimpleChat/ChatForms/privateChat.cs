using System.Windows.Forms;

namespace SimpleClient.ChatForms
{
    public partial class privateChat : Form
    {
        public PublicChat pChat;

        public privateChat(PublicChat publicChat)
        {
            InitializeComponent();
            pChat = publicChat;
        }

        private void buttonSend_Click(object sender, System.EventArgs e)
        {
            if (textBoxMessage.Text != string.Empty)
            {
                string user = Text.Split('-')[1];
                pChat.formlogin
            }
        }
    }
}
