﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SimpleServer
{
    class Listener
    {
        private Socket _socket;

        public bool ListeningBoolean { get; private set; }

        public int Port { get; private set; }

        public Listener(int port)
        {
            Port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (ListeningBoolean) return;
            _socket.Bind(new IPEndPoint(0, Port));
            _socket.Listen(0);
            _socket.BeginAccept(Callback, null);
            ListeningBoolean = true;
        }

        public void Stop()
        {
            if (!ListeningBoolean) return;
            if (_socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        public delegate void SocketAcceptedHandler(Socket eSocket);

        public event SocketAcceptedHandler SocketAccepted;

        void Callback(IAsyncResult asyncResult)
        {
            try
            {
                var socketVariable = _socket.EndAccept(asyncResult);
                if (SocketAccepted != null)
                {
                    SocketAccepted(socketVariable);
                }
                _socket.BeginAccept(Callback, null);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Message 3 : " + exception.Message);
            }
        }
    }
}
