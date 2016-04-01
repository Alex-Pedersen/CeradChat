using System.Net.Sockets;

namespace Server.Functionality
{
    public class AcceptFunction
    {
        public void AcceptSocket(Socket socket)
        {
            socket.Accept();
        }
    }
}
