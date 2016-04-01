using System.Net;

namespace Cli.Functionality
{
    public class EndPoint
    {
        public IPEndPoint CreateIpEndPoint()
        {
            return new IPEndPoint(IPAddress.Any, 8000);
        }
    }
}