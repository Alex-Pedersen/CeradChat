using System.Net;
using System.Net.Sockets;

namespace Server.Functionality
{
    public class BindFunction
    {
        public void BindEndPoint(Socket socket,IPEndPoint endPoint)
        {
            socket.Bind(endPoint);
        }
    }
}
