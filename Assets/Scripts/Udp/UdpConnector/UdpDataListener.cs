using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Datagrams.AbstractTypes;
using tools;
using UnityEngine;

namespace Udp.UdpConnector
{
	public class UdpDataListener
	{
		public event Action<AbstractDatagram> OnDataRetrieved;
		private UdpClient _client;


		public UdpDataListener(UdpClient client)
		{
			_client = client;
		}

		private void ExeListen()
		{
			Task.Run(async () => { await Listening(); });
		}

		private async Task Listening()
		{
			while (_client != null)
			{
				try
				{
					var result = await _client.ReceiveAsync();
					var abstractDatagram = BinarySerializer.Deserialize<AbstractDatagram>(result.Buffer);
					OnDataRetrieved?.Invoke(abstractDatagram);
				}
				catch (Exception e)
				{
					Debug.Log("Exception Occurs: " + e + "Listening Continued");
					ExeListen();
					break;
				}
			}
		}
	}
}
