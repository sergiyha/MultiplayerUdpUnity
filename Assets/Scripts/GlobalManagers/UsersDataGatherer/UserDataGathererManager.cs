
using Datagrams.CustomTypes;
using tools;
using Zenject;

public class UserDataGatherManager : IUserDataGatherManager
{
	private ITickableManager _tickManager;
	private IUserManager _userManager;
	private UdpDataSender _udpDataSender;
	private UserMonoView _observeredUserView;

	
	public UserDataGatherManager(
		ITickableManager tickManager,
		IUserManager userManager)
	{
		_tickManager = tickManager;
		_userManager = userManager;
		
	}

	public void GatherOn(OwnerUserMonoView userView)
	{
		_observeredUserView = userView;
		_udpDataSender = new UdpDataSender();
		_udpDataSender.InitSender(_userManager.GetUserUdpClient(), GetSendingData);
		_tickManager.Register(Gather);
	}

	private byte[] GetSendingData()
	{
		return BinarySerializer.Serialize(new ConcreteUserInformation()
		{
			UserId = _observeredUserView.UserData.UserIdentifier.ToString(),
			UserInformation = _observeredUserView.UserData
		});
	}

	private void Gather()
	{
		if (!_userManager.UserExist()) return;
		_udpDataSender.Send();
	}

	public void StopGathering()
	{
		_tickManager.Unregister(Gather);
		_udpDataSender = null;
	}
}
