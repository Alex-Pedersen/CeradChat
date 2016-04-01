using System.Net.Sockets;

namespace Server.Functionality
{
    public class ListenFunction
    {
        public void Listen(Socket socket, int amount)
        {
            socket.Listen(amount);
        }
    }
}
