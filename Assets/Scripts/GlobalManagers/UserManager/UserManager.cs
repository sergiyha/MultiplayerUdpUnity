using System.Net.Sockets;

public class UserManager : IUserManager
{
	private UserModel _userModel;


	public void CreateUser()
	{
		_userModel = new UserModel()
		{
			UserId = GetHashCode()
		};
	}

	public UserModel GetUserModel()
	{
		return _userModel;
	}

	public UdpClient GetUserTcpClient()
	{
		return _userModel.UdpClient;
	}

	public void SetUserUdp(UdpClient client)
	{
		_userModel.UdpClient = client;
	}
}

public class UserModel
{
	public int UserId;
	public UdpClient UdpClient;
}
