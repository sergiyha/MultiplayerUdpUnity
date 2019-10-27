using System.Text;

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

	public void Send(byte[] datagram)
	{
		Client.Send(datagram, datagram.Length);
	}

}