using System.Net.Sockets;

public class UserManager : IUserManager
{
	private UserModel _userModel = null;


	public void CreateUser()
	{
		_userModel = new UserModel()
		{
			UserId = GetHashCode()
		};
	}

	public void DeleteUser()
	{
		_userModel = null;
	}

	public UserModel GetUserModel()
	{
		return _userModel;
	}

	public UdpClient GetUserUdpClient()
	{
		return _userModel.UdpClient;
	}

	public void SetUserUdp(UdpClient client)
	{
		_userModel.UdpClient = client;
	}

	public bool UserExist()
	{
		return _userModel != null;
	}
}

public class UserModel
{
	public int UserId;
	public UdpClient UdpClient;
}
