using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Datagrams.AbstractTypes;
using tools;

public class RecievedData
{
	public byte[] Datagram;
	public IPEndPoint EndPoint;
}

abstract class UdpBase
{
	protected UdpClient Client;

	protected UdpBase()
	{

		Client = new UdpClient();

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
		Client = new UdpClient(_listenOn);
		//Client.m
	}

	public void Reply(string message, IPEndPoint endpoint)
	{
		var datagram = Encoding.ASCII.GetBytes(message);
		Client.Send(datagram, datagram.Length, endpoint);
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

class Program
{
	static void Main()
	{
		//create a new server
		var server = new UdpListener();
		
		//start listening for messages and copy the messages back to the client
		Task.Factory.StartNew(async () =>
		{
			while (true)
			{
				//while (true)
				//{
				//	Console.Write("startLister");
				//	var received = await server.Receive();
				//	Console.Write("recieve: " + received.Datagram.Length);
				//	server.Reply("copy " + received.Datagram.Length, received.EndPoint);
				//	if (received.Datagram.ToString() == "quit")
				//		break;
				//}
				try
				{
					Console.Write("StartToListen");
					var received = await server.Receive();
				 	var transformRequest = BinarySerializer.Deserialize<Datagrams.AbstractTypes.AbstractNumberedDatagram>(received.Datagram);
					Console.Write(" "+ transformRequest.GetDatagramId()+"\n");
					//server.Reply("copy " + transformRequest.Message + "", received.EndPoint);
					//Console.Write("copy " + " msg: " + transformRequest.Message + "\n" +
					//			  "position: " + transformRequest.Position.x + " " + transformRequest.Position.y +" " + transformRequest.Position.z + "\n" +
					//			  "rotation: " + transformRequest.Rotation.x + " " + transformRequest.Rotation.y + " "+transformRequest.Rotation.z + " " +transformRequest.Rotation.w + "\n");
					//if (transformRequest.Message == "quit")
						//break;


				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}

			}
		});

		//	create a new client
		//var client = UdpUser.ConnectTo("127.0.0.1", 32123);

		////wait for reply messages from server and send them to console
		//Task.Factory.StartNew(async () =>
		//{
		//	while (true)
		//	{
		//		try
		//		{
		//			var received = await client.Receive();
		//			Console.WriteLine("user: " + " received.Message" + "\n");
		//			//if (received.Message.Contains("quit"))
		//			//break;
		//		}
		//		catch (Exception ex)
		//		{
		//			Console.WriteLine(ex);
		//		}
		//	}
		//});

		//type ahead :-)
		string read;
		do
		{
			read = Console.ReadLine();
				//client.Send(read);
		} while (read != "quit");
	}
}