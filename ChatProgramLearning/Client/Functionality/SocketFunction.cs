using System.Net.Sockets;

namespace Cli.Functionality
{
    public class SocketFunction
    {
        public Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
