using System.Net.Sockets;

namespace Server.Functionality
{
    public class SocketFunctionality
    {
        public Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
