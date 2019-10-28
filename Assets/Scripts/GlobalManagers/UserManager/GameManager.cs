using System.Net.Sockets;
using Datagrams.CustomTypes;
using Zenject;

public class GameManager : IGameManager
{
	[Inject] private IUserManager _userManager;

	public void StartGame()
	{
		ConnectUser();
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

	}
}
