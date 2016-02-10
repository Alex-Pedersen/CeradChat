using System;
using System.Windows.Forms;

namespace SimpleClient.ChatForms
{
    public partial class PublicChat : Form
    {
        public PublicChat()
        {
            _pChat = new privateChat(this);
            InitializeComponent();
        }

        public readonly LoginChat LoginChat = new LoginChat();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoginChat.ClientSettings.Received += _client_Received;
            LoginChat.ClientSettings.Disconnected += Client_Disconnected;
            Text = "TCP Chat - " + LoginChat.txtIP.Text + " - (Connected as: " + LoginChat.txtNickname.Text + ")";
            LoginChat.ShowDialog();
        }

        private static void Client_Disconnected(ClientSettings cs)
        {

        }

        private readonly privateChat _pChat;

        public void _client_Received(ClientSettings cs, string received)
        {
            var cmd = received.Split('|');
            switch (cmd[0])
            {
                case "Users":
                    this.Invoke(() =>
                    {
                        userList.Items.Clear();
                        for (int i = 1; i < cmd.Length; i++)
                        {
                            if (cmd[i] != "Connected" | cmd[i] != "RefreshChat")
                            {
                                userList.Items.Add(cmd[i]);
                            }
                        }
                    });
                    break;
                case "Message":
                    this.Invoke(() =>
                    {
                        textBoxChat.Text += cmd[1] + "\r\n";
                    });
                    break;
                case "RefreshChat":
                    this.Invoke(() =>
                    {
                        textBoxChat.Text = cmd[1];
                    });
                    break;
                case "Chat":
                    this.Invoke(() =>
                    {
                        _pChat.Text = _pChat.Text.Replace("user", LoginChat.txtNickname.Text);
                        _pChat.Text = _pChat.Text.Replace("user", LoginChat.)
                        _pChat.Show();
                    });
                    break;
                case "pMessage":
                    this.Invoke(() =>
                    {
                        _pChat.textBoxChat.Text += "Server says: " + cmd[1] + "\r\n";
                    });
                    break;
                case "Disconnect":
                    Application.Exit();
                    break;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (textBoxMessage.Text != string.Empty)
            {
                LoginChat.Client.Send("Message|" + LoginChat.txtNickname.Text + "|" + textBoxMessage.Text);
                textBoxChat.Text += LoginChat.txtNickname.Text + " says: " + textBoxMessage.Text + "\r\n";
                textBoxMessage.Text = string.Empty;
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend.PerformClick();
            }
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            textBoxChat.SelectionStart = textBoxChat.TextLength;
        }

        private void privateChat_Click(object sender, EventArgs e)
        {
            LoginChat.Client.Send("pChat|" + LoginChat.txtNickname.Text);
        }
    }
}

