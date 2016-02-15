using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SimpleClient
{
    public class ClientSettings
    {
        public delegate void DisconnectedEventHandler(ClientSettings clientSettings);

        public delegate void ReceivedEventHandler(ClientSettings clientSettings, string received);

        private readonly Socket _socket;

        public bool connected { get; private set; }

        public ClientSettings()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public event ReceivedEventHandler Received = delegate { };

        public event EventHandler Connected = delegate { };

        public event DisconnectedEventHandler Disconnected = delegate { };

        public void Connect(string ip, int port)
        {
            try
            {
                var ep = new IPEndPoint(IPAddress.Parse(ip), port);
                _socket.BeginConnect(ep, ConnectCallback, _socket);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Close()
        {
            _socket.Dispose();
            _socket.Close();
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            _socket.EndConnect(ar);
            connected = true;
            Connected(this, EventArgs.Empty);
            var buffer = new byte[_socket.ReceiveBufferSize];
            _socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReadCallback, buffer);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            var buffer = (byte[]) ar.AsyncState;
            var rec = _socket.EndReceive(ar);
            if (rec != 0)
            {
                var data = Encoding.ASCII.GetString(buffer, 0, rec);
                Received(this, data);
            }
            else
            {
                Disconnected(this);
                connected = false;
                Close();
                return;
            }
            _socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReadCallback, buffer);
        }

        public void Send(string data)
        {
            try
            {
                var buffer = Encoding.ASCII.GetBytes(data);
                _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, buffer);
            }
            catch
            {
                Disconnected(this);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            _socket.EndSend(ar);
        }
    }
}