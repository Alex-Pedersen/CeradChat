using System.Net;

namespace Server.Functionality
{
    public class EndPoint
    {
        public IPEndPoint CreateIpEndPoint()
        {
            return new IPEndPoint(IPAddress.Any, 8000);
        }
    }
}