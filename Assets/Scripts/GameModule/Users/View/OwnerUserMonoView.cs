using Datagrams.Datagram;
using Zenject;

public class OwnerUserMonoView : UserMonoView
{

	protected override void ApplyUserInfo()
	{
		UserData = new UserInfo()
		{
			UserIdentifier = UserId,
			Transform = new TransformDatagram()
			{
				Message = "user simple transform",
				Position = new Datagrams.CustomTypes.Vector3
				(
					this.transform.position.x,
					this.transform.position.y,
					this.transform.position.z
				),
				Rotation = new Datagrams.CustomTypes.Vector4
				(
					this.transform.rotation.x,
					this.transform.rotation.y,
					this.transform.rotation.z,
					this.transform.rotation.w
				),
			}
		};
	}

	public class Factory : PlaceholderFactory<OwnerUserMonoView>
	{
	}
}
