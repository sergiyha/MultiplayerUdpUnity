using System;
using System.Net.Sockets;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using Udp.UdpConnector;
using UnityEngine;
using Zenject;

public class ConnectionsManager : MonoBehaviour
{

	[Inject] private IDataUpdaterManager _updateManager;

	public void ListenTheConnection(UdpClient client)
	{
		new UdpDataListener(client).OnDataRetrieved += OnDataRetrieved;
	}


	private void OnDataRetrieved(AbstractDatagram datagram)
	{
		switch (datagram.GetDatagramId())
		{
			case RequestIdentifiers.Connected:
			{
				var id = (datagram as RequestBodyBase).UserId;
				RegisterConnection();

				break;
			}
			case RequestIdentifiers.Disconnect:
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
