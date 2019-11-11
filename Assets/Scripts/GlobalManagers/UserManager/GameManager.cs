using System.Net.Sockets;
using Datagrams.CustomTypes;
using Datagrams.Datagram;
using Zenject;

public class GameManager : IGameManager
{
	[Inject] private IUserManager _userManager;
	[Inject] private ITickableManager _tickManager;
	[Inject] private IDataUpdaterManager _dataUpdateManager;
	[Inject] private IPlayersSpawnManager _playersSpawnManager;
	[Inject] private IConnectionManager _connectionManager;

	public void StartGame()
	{
		ConnectUser();
		_connectionManager.PlayerConnected += OnPlayerConnected;
		_connectionManager.PlayerDisconnected += OnPlayerDisconnected;

	}

	private void OnPlayerDisconnected(int id)
	{
		throw new System.NotImplementedException();
	}

	private void OnPlayerConnected(UserInfo userData)
	{
		_playersSpawnManager.AddPlayerToSpawnQueue(userData);
	}

	public void FinishGame()
	{
		throw new System.NotImplementedException();
	}

	private void ConnectUser()
	{
		_userManager.CreateUser();
		int userId = _userManager.GetUserModel().UserId;
		new UdpUserConnector().Connect(userId, OnUserConnectedToServer);
	}

	private void OnUserConnectedToServer(UdpClient client, UserConnectedRequestBody userConnectedPayloads)
	{
		_userManager.SetUserUdp(client);
		CreateUser(userConnectedPayloads.UserInformation);
		CreateExistedPlayers(userConnectedPayloads.ExistedPlayersInformation);
		_connectionManager.StartListening(client);
		_dataUpdateManager.StartListening(client);
	}

	private void CreateExistedPlayers(UserInfo[] usersInfo)
	{
		if (usersInfo == null) return;
		_playersSpawnManager.AddPlayersToSpawnQueue(usersInfo);
	}

	private void CreateUser(UserInfo userInfo)
	{
		_playersSpawnManager.InitiateUserSpawning(userInfo);
	}
}
