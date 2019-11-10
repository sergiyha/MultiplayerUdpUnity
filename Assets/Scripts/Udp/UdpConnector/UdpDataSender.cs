using System;
using System.Net.Sockets;

public class UdpDataSender
{
	private Func<byte[]> _getData;
	private UdpClient _udpClient;
	public void InitSender(UdpClient client,Func<byte[]> getData)
	{
		_udpClient = client;
		_getData = getData;
	}

	public void Send()
	{
		var currentData = _getData?.Invoke();
		_udpClient.Send(currentData ?? throw new InvalidOperationException(), currentData.Length);
	}
}
