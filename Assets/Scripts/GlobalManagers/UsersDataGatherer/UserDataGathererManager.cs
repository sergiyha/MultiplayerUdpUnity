
using Datagrams.CustomTypes;
using tools;
using Zenject;

public class UserDataGatherManager : IUserDataGatherManager
{
	private ITickableManager _tickManager;
	private IUserManager _userManager;
	private UdpDataSender _udpDataSender;
	private UserMonoView _observedUserView;

	[Inject]
	public UserDataGatherManager(
		ITickableManager tickManager,
		IUserManager userManager)
	{
		_tickManager = tickManager;
		_userManager = userManager;
		_tickManager.Register(Gather);
	}

	public void GatherOn(OwnerUserMonoView userView)
	{
		_udpDataSender = new UdpDataSender();
		_udpDataSender.InitSender(_userManager.GetUserUdpClient(), GetSendingData);
	}

	private byte[] GetSendingData()
	{
		return BinarySerializer.Serialize(new ConcreteUserInformation()
		{
			UserId = _observedUserView.UserData.UserIdentifier.ToString(),
			UserInformation = _observedUserView.UserData
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
