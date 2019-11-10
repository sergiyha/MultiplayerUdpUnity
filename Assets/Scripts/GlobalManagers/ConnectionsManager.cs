using System;
using System.Net.Sockets;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using Datagrams.Datagram;
using Udp.UdpConnector;
using Zenject;

public class ConnectionsManager : IConnectionManager
{
	[Inject] private IDataUpdaterManager _updateManager;

	private UdpDataListener _udpListener;

	public event Action<UserInfo> PlayerConnected;
	public event Action<int> PlayerDisconnected;



	public void StartListening(UdpClient client)
	{
		_udpListener = new UdpDataListener(client);
		_udpListener.OnDataRetrieved += OnDataRetrieved;
	}

	public void StopListening()
	{
		_udpListener.Stop();
	}

	private void OnDataRetrieved(AbstractDatagram datagram)
	{
		switch (datagram.GetDatagramId())
		{
			case RequestIdentifiers.PlayerConnected:
				{
					var userInfo = (datagram as PlayerConnectedRequestBody).UserInformation;
					PlayerConnected?.Invoke(userInfo);
					break;
				}
			case RequestIdentifiers.PlayerDisconnected:
				{
					var id = (datagram as PlayerDisconnectedRequestBody).UserIdentifier;
					PlayerDisconnected?.Invoke(id);
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}




}
