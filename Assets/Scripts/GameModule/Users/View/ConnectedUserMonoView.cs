using Datagrams.Datagram;
using UnityEngine;
using Zenject;

public class ConnectedUserMonoView : UserMonoView, IUpdatableUserInfo<UserInfo>
{
	[Inject] private IDataUpdaterManager _updateManager;
	public override void Init(int userId)
	{
		base.Init(userId);
		_updateManager.AddUpdatable(userId, this);
	}

	public void UpdateData(UserInfo info)
	{
		UserData = info;
	}

	protected override void ApplyUserInfo()
	{
		transform.position = new Vector3(UserData.Transform.Position.x, UserData.Transform.Position.y, UserData.Transform.Position.z);
		transform.rotation = new Quaternion(UserData.Transform.Rotation.x, UserData.Transform.Rotation.y, UserData.Transform.Rotation.z, UserData.Transform.Rotation.w);
	}

	public class Factory : PlaceholderFactory<ConnectedUserMonoView>
	{
	}
}
