using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UdpClient
{

	public class RecievedData
	{
		public byte[] Datagram;
		public IPEndPoint EndPoint;
	}

	abstract class UdpBase
	{
		protected System.Net.Sockets.UdpClient Client;

		protected UdpBase()
		{

			Client = new System.Net.Sockets.UdpClient();

		}

		public async Task<RecievedData> Receive()
		{
			var result = await Client.ReceiveAsync();
			return new RecievedData()
			{
				Datagram = result.Buffer,
				EndPoint = result.RemoteEndPoint
			};
		}
	}

	//Server
	class UdpListener : UdpBase
	{
		private IPEndPoint _listenOn;

		public UdpListener() : this(new IPEndPoint(IPAddress.Any, 32123))
		{

		}

		public UdpListener(IPEndPoint endpoint)
		{
			_listenOn = endpoint;
			Client = new System.Net.Sockets.UdpClient(_listenOn);
			//Client.m
		}

		public void Reply(string message, IPEndPoint endpoint)
		{
			var datagram = Encoding.ASCII.GetBytes(message);
			Client.Send(datagram, datagram.Length, endpoint);
		}

		public void Send(byte[] bytes, IPEndPoint endpoint)
		{
			Client.Send(bytes, bytes.Length, endpoint);
		}

		public Task<int>  SendAsync(byte[] bytes,IPEndPoint endPoint)
		{
			return Client.SendAsync(bytes, bytes.Length, endPoint);
		}

	}

	//Client
	class UdpUser : UdpBase
	{
		private UdpUser() { }

		public static UdpUser ConnectTo(string hostname, int port)
		{
			var connection = new UdpUser();
			connection.Client.Connect(hostname, port);
			return connection;
		}

		public void Send(string message)
		{
			var datagram = Encoding.ASCII.GetBytes(message);
			Client.Send(datagram, datagram.Length);
		}
	}
}
