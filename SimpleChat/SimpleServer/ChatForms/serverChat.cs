using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SimpleServer.ChatForms
{
    public partial class ServerChat : Form
    {
        private readonly Listener _listener;

        public List<Socket> Clients = new List<Socket>();

        private PrivateChat _pChat;

        public void BroadcastData(string data)
        {
            foreach (var socket in Clients)
            {
                try
                {
                    socket.Send(Encoding.ASCII.GetBytes(data));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Message one : " + e.Message);
                }
            }
        } 

        public ServerChat()
        {
            InitializeComponent();
            _pChat = new PrivateChat(this);
            _listener = new Listener(2016);
            _listener.SocketAccepted += ListenerSocketAccept;
        }

        private void ListenerSocketAccept(Socket socket)
        {
            var client = new Client(socket);
            client.Received += ClientReceived;
            client.Disconnected += ClientDisconnected;
            this.Invoke(() =>
            {
                var ip = client.IpEndPoint.ToString().Split(':')[0];
                var item = new ListViewItem(ip); // ip
                item.SubItems.Add(" "); // nickname
                item.SubItems.Add(" "); // status
                item.Tag = client;
                clientList.Items.Add(item);
                Clients.Add(socket);
            });
        }

        private void ClientDisconnected(Client senderClient)
        {
            this.Invoke(() =>
            {
                for (int i = 0; i < clientList.Items.Count; i++)
                {
                    var client = clientList.Items[i].Tag as Client;
                    if (client != null && Equals(client.IpEndPoint, senderClient.IpEndPoint))
                    {
                        textBoxReceive.Text += "<< " + clientList.Items[i].SubItems[1].Text + " has left the room >>\r\n";
                        BroadcastData("RefreshChat|" + textBoxReceive.Text);
                        clientList.Items.RemoveAt(i);
                    }
                }
            });
        }

        private void ClientReceived(Client senderClient, byte[] dataBytes)
        {
            this.Invoke(() =>
            {
                for (var i = 0; i < clientList.Items.Count; i++)
                {
                    var client = clientList.Items[i].Tag as Client;
                    if (client == null || !Equals(client.IpEndPoint, senderClient.IpEndPoint)) continue;
                    var command = Encoding.ASCII.GetString(dataBytes).Split('|');
                    switch (command[0])
                    {
                        case "Connect":
                            textBoxReceive.Text += "<< " + command[1] + " joined the room >>\r\n";
                            clientList.Items[i].SubItems[1].Text = command[1]; // nickname
                            clientList.Items[i].SubItems[2].Text = command[2]; // status
                            var users = string.Empty;
                            for (var j = 0; j < clientList.Items.Count; j++)
                            {
                                users += clientList.Items[j].SubItems[1].Text + "|";
                            }
                            BroadcastData("Users|" + users.TrimEnd('|'));
                            BroadcastData("RefreshChat|" + textBoxReceive.Text);
                            break;
                        case "Message":
                            textBoxReceive.Text += command[1] + " says: " + command[2] + "\r\n";
                            BroadcastData("RefreshChat|" + textBoxReceive.Text);
                            break;
                        case "pMessage":
                            this.Invoke(() =>
                            {
                                _pChat.textBoxReceive.Text += command[1] + " says: " + command[2] + "\r\n";
                            });
                            break;
                        case "pChat":

                            break;
                    }
                }
            });
        }

        private void richTextBox1_TextChanged(object sender, System.EventArgs e)
        {
            textBoxReceive.SelectionStart = textBoxReceive.TextLength;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (textInputBox.Text != string.Empty)
            {
                BroadcastData("Message : " + textInputBox.Text);
                textBoxReceive.Text += textInputBox.Text + "\r\n";
                textInputBox.Text = "Administrator says : ";
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void ServerChat_Load(object sender, EventArgs e)
        {
            _listener.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _listener.Stop();
        }
    }
}
