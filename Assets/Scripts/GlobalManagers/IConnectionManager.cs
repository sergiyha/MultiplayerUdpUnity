using System;
using Datagrams.Datagram;
using UnityEngine;

public interface IConnectionManager : IDataListener
{
	event Action<UserInfo> PlayerConnected;
	event Action<int> PlayerDisconnected;

}
