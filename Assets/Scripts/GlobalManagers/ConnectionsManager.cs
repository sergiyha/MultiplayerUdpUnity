using System;
using System.Net.Sockets;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using Udp.UdpConnector;
using UnityEngine;
using Zenject;

public class ConnectionsManager : MonoBehaviour, IConnectionManager
{
	[Inject] private IDataUpdaterManager _updateManager;
	private UdpDataListener _udpListener;

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
					var id = (datagram as RequestBodyBase).UserId;
					RegisterConnection();
					break;
				}
			case RequestIdentifiers.PlayerDisconnected:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void RegisterConnection()
	{
		//createUserEtc
		//InitPrefab
	}

	private void UnregisterConnection()
	{
		//createUserEtc
		//InitPrefab
	}



}
