using Datagrams.Datagram;
using UnityEngine;

public class ConnectedUserMonoView : UserMonoView, IUpdatableUserInfo<UsersInfo>
{	
	public void UpdateData(UsersInfo info)
	{
		UserData = info;
	}

	protected override void ApplyUserInfo()
	{
		transform.position = new Vector3(UserData.Transform.Position.x, UserData.Transform.Position.y, UserData.Transform.Position.z);
		transform.rotation = new Quaternion(UserData.Transform.Rotation.x, UserData.Transform.Rotation.y, UserData.Transform.Rotation.z, UserData.Transform.Rotation.w);
	}
}
