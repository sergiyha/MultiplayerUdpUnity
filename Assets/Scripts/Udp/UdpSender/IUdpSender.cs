using System;
using System.Runtime.InteropServices;

public interface IUdpSender
{
	void Connect();
	void Disconnect();
	event Action DataRetrieved;
	void Send();
}

public abstract class AbstractUdpSender<T> : IUdpSender
{
	public event Action DataRetrieved;
	public abstract void Connect();
	public abstract void Disconnect();

	public void Send()
	{
		
	}
}
