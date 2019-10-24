using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Datagrams.AbstractTypes;
using tools;
using UnityEngine;

public class UdpDataListener
{
	private Dictionary<int, UdpClient> _clientsStorage = new Dictionary<int, UdpClient>(); 
	private void StopListenOn(UdpClient client)
	{
		var hash = client.GetHashCode();
		_clientsStorage.Add(hash, client);
		ListenOn(hash);
	}

	private void ListenOn(int hash)
	{
		Task.Run(async () =>{ await Listening(hash); });
	}

	private async Task Listening(int hash)
	{
		var client = _clientsStorage[hash];
		while (_clientsStorage.ContainsKey(hash))
		{
			try
			{
				var result = await client.ReceiveAsync();
				var abstractDatagram =  BinarySerializer.Deserialize<AbstractDatagram>(result.Buffer);
				PackDatagram(abstractDatagram);
			}
			catch (Exception e)
			{
				Debug.Log("Exception Occurs: "+e + "Listening Continued");
				ListenOn(hash);
				break;
			}
		}
	}

	private void PackDatagram(AbstractDatagram datagram)
	{
		switch (datagram.GetDatagramId())
		{
			case Datagrams.CustomTypes.RequestIdentifiers.Connect:
				break;
			case Datagrams.CustomTypes.RequestIdentifiers.Connected:
				break;
			case Datagrams.CustomTypes.RequestIdentifiers.Disconnect:
				break;
			case Datagrams.CustomTypes.RequestIdentifiers.Disconnected:
				break;
		}
	}
}
