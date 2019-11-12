using Datagrams.Datagram;
using UnityEngine;
using Zenject;

public class ConnectedUserMonoView : UserMonoView, IUpdatableUserInfo<UserInfo>
{
	[Inject] private IDataUpdaterManager _updateManager;

	public override void Init(UserInfo userInfo)
	{
		base.Init(userInfo);
		_updateManager.AddUpdatable(userInfo.UserIdentifier, this);
		_futurePosition = transform.position;
		_positionLerp = 0;
	}

	private Vector3 _currentPosition;
	private Vector3 _futurePosition;

	public void UpdateData(UserInfo info)
	{
		UserData = info;
	}

	protected override void ApplyUserInfo()
	{
		var newPosition = new Vector3(UserData.Transform.Position.x, UserData.Transform.Position.y, UserData.Transform.Position.z);
		transform.rotation = new Quaternion(UserData.Transform.Rotation.x, UserData.Transform.Rotation.y, UserData.Transform.Rotation.z, UserData.Transform.Rotation.w);
		//if (newPosition == _futurePosition) return;
		_futurePosition = newPosition;
		_currentPosition = transform.position;
		_positionLerp = 0;
		//transform.position = new Vector3(UserData.Transform.Position.x, UserData.Transform.Position.y, UserData.Transform.Position.z);
	}

	public void Update()
	{
		Interpolate();
	}

	private float _positionLerp;
	public int LerpSpeed = 10;
	private void Interpolate()
	{
		_positionLerp += Time.deltaTime * LerpSpeed;
		_positionLerp = _positionLerp > 1 ? 1 : _positionLerp;
		transform.position = Vector3.Lerp(_currentPosition, _futurePosition, _positionLerp);
	}

	//private void 
	public class Factory : PlaceholderFactory<ConnectedUserMonoView>
	{
	}
}
