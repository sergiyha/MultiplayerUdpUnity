using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using tools;

public class UdpUserConnector
{
	private string _hostname = "127.0.0.1";
	private int _port = 32123;
	private string _connectToken = "connect";

	private UdpClient _client;
	private Action<UdpClient, UserConnectedRequestBody> _onConnectionComplete;
	private int _userId;

	public void Connect(int userId, Action<UdpClient, UserConnectedRequestBody> onConnectionComplete)
	{
		_userId = userId;
		_client = new UdpClient();
		_onConnectionComplete = onConnectionComplete;
		_client.Connect(_hostname, _port);

		var connectionObjBinary = BinarySerializer.Serialize(new UserConnectRequestbody() { Description = _connectToken, UserId = _userId.ToString(),UserIdentifier = _userId });
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

					if (absReq.GetDatagramId() == RequestIdentifiers.UserConnected &&
					    absReq is UserConnectedRequestBody userConnectedPayload &&
					    userConnectedPayload.UserInformation.UserIdentifier == _userId)
					{
						_onConnectionComplete?.Invoke(_client, userConnectedPayload);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError(ex);
				await WaitingForConnect();
			}
		}
	}
}
