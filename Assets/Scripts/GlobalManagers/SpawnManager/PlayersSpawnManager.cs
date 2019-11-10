using System;
using System.Collections.Generic;
using System.Linq;
using Datagrams.Datagram;

public class PlayersSpawnManager : IPlayersSpawnManager
{
	private ITickableManager _tickManager;
	private ConnectedUserMonoView.Factory _connectedUserFactory;
	private OwnerUserMonoView.Factory _ownerUserFactory;
	public event Action<int> PlayerCreated;
	public event Action UserCreated;
	private Queue<UserInfo> _spawnedPlayers = new Queue<UserInfo>();
	private UserInfo? _userInfo;


	private PlayersSpawnManager
	(
		ITickableManager tickManager,
		ConnectedUserMonoView.Factory connectedUserFactory,
		OwnerUserMonoView.Factory ownerUserFactory
	)
	{
		_tickManager = tickManager;
		_ownerUserFactory = ownerUserFactory;
		_connectedUserFactory = connectedUserFactory;
		_tickManager.Register(SpawnPlayer);
		_tickManager.Register(SpawnUser);

	}

	~PlayersSpawnManager()
	{
		_tickManager.Unregister(SpawnPlayer);
		_tickManager.Unregister(SpawnUser);
	}

	public void AddPlayersToSpawnQueue(UserInfo[] usersInfo)
	{
		foreach (var userInfo in usersInfo)
		{
			AddPlayerToSpawnQueue(userInfo);
		}
	}

	public void AddPlayerToSpawnQueue(UserInfo usersInfo)
	{
		_spawnedPlayers.Enqueue(usersInfo);
	}

	public void InitiateUserSpawning(UserInfo userInfo)
	{
		_userInfo = userInfo;
	}

	private void SpawnPlayer()
	{
		if (!_spawnedPlayers.Any()) return;
		var data = _spawnedPlayers.Dequeue();
		var playerView = _connectedUserFactory.Create();
		playerView.Init(data);
	}

	private void SpawnUser()
	{
		if (_userInfo == null) return;
		var user = _ownerUserFactory.Create();
		user.Init((UserInfo)_userInfo);
		_userInfo = null;
	}
}
