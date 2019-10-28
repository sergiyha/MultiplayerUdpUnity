using System;
using Datagrams.Datagram;

namespace Datagrams.CustomTypes
{
	[Serializable]
	public class UserConnectRequestbody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.UserConnect;
		}
	}

	[Serializable]
	public class UserConnectedRequestBody : RequestBodyBase
	{
		public UsersInfo[] UsersInformation;
		public Vector3 Position;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.UserConnected;
		}
	}

	[Serializable]
	public class UserDisconnectRequestBody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.UserDisconnect;
		}
	}

	[Serializable]
	public class UserDisconnectedRequestBody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.UserDisconnected;
		}
	}

	[Serializable]
	public class PlayerConnectedRequestBody : RequestBodyBase
	{
		public Vector3 Position;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.PlayerConnected;
		}
	}

	[Serializable]
	public class PlayerDisconnectedRequestBody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.PlayerDisconnected;
		}
	}

	[Serializable]
	public class UserInformationRequestBody : RequestBodyBase
	{
		public UsersInfo[] UsersInformation;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.UsersInfo;
		}
	}
}
