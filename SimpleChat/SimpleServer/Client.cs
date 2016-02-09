﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    class Client
    {
        public delegate void ClientReceivedHandler(Client senderClient, byte[] data);
        public delegate void ClientDisconnectedHandler(Client senderClient);

        public event ClientReceivedHandler Received;
        public event ClientDisconnectedHandler Disconnected;

        public IPEndPoint IpEndPoint { get; private set; }

        private readonly Socket _socket;

        public Client(Socket socket)
        {
            _socket = socket;
            IpEndPoint = (IPEndPoint) _socket.RemoteEndPoint;
            _socket.BeginReceive(new byte[] {0}, 0, 0, 0, Callback, null);
        }

        public void Callback(IAsyncResult ar)
        {
            try
            {
                _socket.EndReceive(ar);
                var buffer = new byte[_socket.ReceiveBufferSize];
                var rec = _socket.Receive(buffer, buffer.Length, 0);
                if (rec < buffer.Length)
                {
                    Array.Resize(ref buffer, rec);
                }
                if (Received != null)
                {
                    Received(this, buffer);
                }
                _socket.BeginReceive(new byte[] {0}, 0, 0, 0, Callback, null);
            }
            catch (Exception)
            {
                Close();
                if (Disconnected != null)
                {
                    Disconnected(this);
                }
            }
        }

        public void Send(string data)
        {
            var buffer = Encoding.ASCII.GetBytes(data);
            _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, ar => _socket.EndSend(ar), buffer);
        }

        public void Close()
        {
            _socket.Dispose();
            _socket.Close();
        }
    }
}