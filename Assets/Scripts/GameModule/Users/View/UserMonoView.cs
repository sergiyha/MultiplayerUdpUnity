using Datagrams.Datagram;
using UnityEngine;
using Zenject;

public abstract class UserMonoView : MonoBehaviour, IUserMonoView
{
	protected abstract void ApplyUserInfo();
	protected UserInfo UserData;

	protected int UserId;
	public virtual void Init(int userId)
	{
		UserId = userId;
	}

	private ITickableManager _tickableManager;

	[Inject]
	private void Inject(ITickableManager tickableManager)
	{
		_tickableManager = tickableManager;
		PostInject();
	}

	private void PostInject()
	{
		_tickableManager.Register(ApplyUserInfo);
	}

	
}
