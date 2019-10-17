using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
			EndPoint= result.RemoteEndPoint
		};
	}
}