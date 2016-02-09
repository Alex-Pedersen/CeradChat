 using System;
using System.Collections.Generic;
using System.Linq;
 using System.Net;
 using System.Net.Sockets;
 using System.Text;
using System.Threading.Tasks;
 using System.Windows.Forms;

namespace SimpleClient
{
    class ClientSettings
    {
        readonly Socket _socket;

        public delegate void ReceivedEventHandler(ClientSettings clientSettings, string received);

        public delegate void DisconnectedEventHandler(ClientSettings clientSettings);

        public event ReceivedEventHandler Received = delegate { };

        public event EventHandler Connected = delegate { };

        public event DisconnectedEventHandler Disconnected = delegate { };

        public bool connected;

        public ClientSettings(Socket socket)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {
            try
            {
                var ep = new IPEndPoint(IPAddress.Parse(ip),port);
                _socket.BeginConnect(ep, ConnectCallback, _socket);
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        public void Close()
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
            _socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReadCallBack, buffer);
        }

        private void ReadCallBack(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }
    }
}
