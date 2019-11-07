using System.Collections.Generic;
using System.Net.Sockets;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using Datagrams.Datagram;
using Udp.UdpConnector;
using Zenject;

public class DataUpdaterManger : IDataUpdaterManager
{
	private Dictionary<int, IUpdatableUserInfo<UserInfo>> _usersInfoStorage = new Dictionary<int, IUpdatableUserInfo<UserInfo>>();
	private UdpDataListener _udpListener;

	private UsersInformationRequestBody _currentDataToUpdate;


	public void StartListening(UdpClient client)
	{
		_udpListener = new UdpDataListener(client);
		_udpListener.OnDataRetrieved += OnDataRetrieved;
	}

	public void StopListening()
	{
		_udpListener.Stop();
	}

	public void OnDataRetrieved(AbstractDatagram datagram)
	{
		switch (datagram.GetDatagramId())
		{
			case RequestIdentifiers.UsersInfo:
				_currentDataToUpdate = datagram as UsersInformationRequestBody;
				UpdateUsersInfo(_currentDataToUpdate);
				break;
		}
	}

	private void UpdateUsersInfo(UsersInformationRequestBody usersInformation)
	{
		// lock
		foreach (UserInfo info in usersInformation.UsersInformation)
		{
			if (_usersInfoStorage.ContainsKey(info.UserIdentifier))
			{
				_usersInfoStorage[info.UserIdentifier].UpdateData(info);
			}
		}

	}

	//Should be added when object was instantiated in scene
	public void AddUpdatable<T>(int id, IUpdatableUserInfo<T> updatable)
	{
		if (typeof(T) == typeof(UserInfo))
		{
			_usersInfoStorage.Add(id, updatable as IUpdatableUserInfo<UserInfo>);
		}
	}


	//Should be added when object was deleted scene
	private void RemoveUpdatable<T>(int id)
	{
		if (typeof(T) == typeof(UserInfo))
		{
			if (_usersInfoStorage.ContainsKey(id)) _usersInfoStorage.Remove(id);
		}
	}


}
