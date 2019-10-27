using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using tools;

public class UdpConnector
{
	private string _hostname = "127.0.0.1";
	private int _port = 32123;
	private string _connectToken = "connect";

	private UdpClient _client;
	private Action<UdpClient> _onConnectionComplete;

	public void Connect( Action<UdpClient> onConnectionComplete)
	{
		_client = new UdpClient();
		_onConnectionComplete = onConnectionComplete;
		_client.Connect(_hostname, _port);

		var connectionObjBinary = BinarySerializer.Serialize(new ConnectRequestbody() { Description = _connectToken });
		_client.Send(connectionObjBinary, connectionObjBinary.Length);

		Task.Factory.StartNew(WaitingForConnect);
	}

	private async Task WaitingForConnect()
	{
		while (true)
		{
			try
			{
				var received = await _client.ReceiveAsync();
				if (received.Buffer != null)
				{
					var absReq = BinarySerializer.Deserialize<AbstractDatagram>(received.Buffer);
					if (absReq.GetDatagramId() == RequestIdentifiers.Connected)
					{
						_onConnectionComplete?.Invoke(_client);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError(ex);
			}
		}
	}
}
