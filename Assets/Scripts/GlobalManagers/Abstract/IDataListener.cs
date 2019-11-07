using System.Net.Sockets;

public interface IDataListener
{
	void StartListening(UdpClient client);
	void StopListening();
}
