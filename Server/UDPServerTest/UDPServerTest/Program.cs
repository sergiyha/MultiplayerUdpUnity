using System;
using System.Net;
using UDPServerTest.UdpClient;

class Program
{
	static void Main()
	{

		#region Useless
		//////create a new server
		////var server = new UdpListener();

		//////start listening for messages and copy the messages back to the client
		////Task.Factory.StartNew(async () =>
		////{
		////	while (true)
		////	{
		////		try
		////		{
		////			Console.Write("StartToListen" + "\n");
		////			var received = await server.Receive();
		////		 	var recievedDatagram = BinarySerializer.Deserialize<Datagrams.AbstractTypes.AbstractDatagram>(received.Datagram);
		////			Console.Write("Someone try to connect" + "\n");
		////			if (recievedDatagram.GetDatagramId() == RequestIdentifiers.Connect)
		////			{

		////				Console.Write("Someone is connected " + received.EndPoint.Address + " " + received.EndPoint.Port + "\n");

		////				server.Reply(BinarySerializer.Serialize(new ConnectedRequestBody(){Description = "connected"}), received.EndPoint);
		////			}

		////			//Console.Write(" "+ recievedDatagram.GetDatagramId()+"\n");
		////			//Console.Write("copy " + " msg: " + transformRequest.Message + "\n" +
		////			//			  "position: " + transformRequest.Position.x + " " + transformRequest.Position.y +" " + transformRequest.Position.z + "\n" +
		////			//			  "rotation: " + transformRequest.Rotation.x + " " + transformRequest.Rotation.y + " "+transformRequest.Rotation.z + " " +transformRequest.Rotation.w + "\n");
		////			//if (transformRequest.Message == "quit")
		////				//break;


		////		}
		////		catch (Exception e)
		////		{
		////			Console.WriteLine(e);
		////			throw;
		////		}

		////	}
		////});

		//////	create a new client
		//////var client = UdpUser.ConnectTo("127.0.0.1", 32123);

		////////wait for reply messages from server and send them to console
		//////Task.Factory.StartNew(async () =>
		//////{
		//////	while (true)
		//////	{
		//////		try
		//////		{
		//////			var received = await client.Receive();
		//////			Console.WriteLine("user: " + " received.Message" + "\n");
		//////			//if (received.Message.Contains("quit"))
		//////			//break;
		//////		}
		//////		catch (Exception ex)
		//////		{
		//////			Console.WriteLine(ex);
		//////		}
		//////	}
		//////});

		//////type ahead :-)
		////string read;
		////do
		////{
		////	read = Console.ReadLine();
		////		//client.Send(read);
		////} while (read != "quit");

		#endregion

		var userManager = new UserManager();
		var connectionMediator = new ConnectionMediator();
		connectionMediator.InjectDependencies(userManager);
		connectionMediator.StartStreaming();

		Console.Write("Start Server");
		string read;
		do
		{
			 read = Console.ReadLine();
		} while (read != "quit");

	}
}