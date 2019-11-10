using System.Net.Sockets;

public interface IUserManager
{
	void CreateUser();
	void DeleteUser();

	UserModel GetUserModel();

	UdpClient GetUserUdpClient();
	void SetUserUdp(UdpClient client);

	bool UserExist();

}
