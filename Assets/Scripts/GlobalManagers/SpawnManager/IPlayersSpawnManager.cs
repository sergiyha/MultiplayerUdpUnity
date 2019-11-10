using System;
using Datagrams.Datagram;

public interface IPlayersSpawnManager
{
	event Action<int> PlayerCreated;
	event Action UserCreated;

	void AddPlayersToSpawnQueue(UserInfo[] usersInfo);

	void AddPlayerToSpawnQueue(UserInfo userInfo);
	void InitiateUserSpawning(UserInfo userInfo);
}
