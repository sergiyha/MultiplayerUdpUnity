using Datagrams.Datagram;
using UnityEngine;
using Zenject;

public abstract class UserMonoView : MonoBehaviour, IUserMonoView
{
	protected abstract void ApplyUserInfo();
	public UserInfo UserData;

	protected int UserId;
	public virtual void Init(UserInfo userInfo)
	{
		UserId = userInfo.UserIdentifier;
		var userTransform = userInfo.Transform;
		this.transform.position = new Vector3(userTransform.Position.x, userTransform.Position.y, userTransform.Position.z);
		this.transform.rotation = new Quaternion(userTransform.Rotation.x, userTransform.Rotation.y, userTransform.Rotation.z, userTransform.Rotation.w);
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
