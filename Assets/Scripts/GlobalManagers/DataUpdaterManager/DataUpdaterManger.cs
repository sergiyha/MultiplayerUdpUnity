using System.Collections.Generic;
using System.Net.Sockets;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using Datagrams.Datagram;
using Udp.UdpConnector;

public class DataUpdaterManger : IDataUpdaterManager
{
	private Dictionary<int, IUpdatableUserInfo<UsersInfo>> _usersInfoStorage = new Dictionary<int, IUpdatableUserInfo<UsersInfo>>();

	public void StartListenUpdatedData(UdpClient client)
	{
		new UdpDataListener(client).OnDataRetrieved += OnDataRetrieved;
	}

	public void OnDataRetrieved(AbstractDatagram datagram)
	{
		switch (datagram.GetDatagramId())
		{
			case RequestIdentifiers.UsersInfo:
				ManageUsersInfo(datagram as UsersInformationRequestBody);
				break;
		}
	}

	private void ManageUsersInfo(UsersInformationRequestBody usersInformation)
	{
		if (usersInformation != null)
		{
			foreach (UsersInfo info in usersInformation.UsersInformation)
			{
				if (_usersInfoStorage.ContainsKey(info.UserIdentifier))
				{
					_usersInfoStorage[info.UserIdentifier].UpdateData(info);
				}
			}
		}
	}

	//Should be added when object was instantiated in scene
	public void AddUpdatable<T>(int id, IUpdatableUserInfo<T> updatable)
	{
		if (typeof(T) == typeof(UsersInfo))
		{
			_usersInfoStorage.Add(id, updatable as IUpdatableUserInfo<UsersInfo>);
		}
	}


	//Should be added when object was deleted scene
	private void RemoveUpdatable<T>(int id)
	{
		if (typeof(T) == typeof(UsersInfo))
		{
			if (_usersInfoStorage.ContainsKey(id)) _usersInfoStorage.Remove(id);
		}
	}
}
