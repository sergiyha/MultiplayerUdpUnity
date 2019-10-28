using System.Net.Sockets;

public interface IUserManager
{
	void CreateUser();

	UserModel GetUserModel();

	UdpClient GetUserTcpClient();
	void SetUserUdp(UdpClient client);
}
