using System;
using Datagrams.Datagram;

namespace Datagrams.CustomTypes
{
	[Serializable]
	public class UserConnectRequestbody : RequestBodyBase
	{
		public int UserIdentifier;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.UserConnect;
		}
	}

	/// <summary>
	/// From server only
	/// </summary>
	[Serializable]
	public class UserConnectedRequestBody : RequestBodyBase
	{
		public UserInfo UserInformation;
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
		public UserInfo UserInformation;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.PlayerConnected;
		}
	}

	[Serializable]
	public class ConcreteUserInformation : RequestBodyBase
	{
		public UserInfo UserInformation;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.ConcreteUserInformation;
		}
	}

	[Serializable]
	public class PlayerDisconnectedRequestBody : RequestBodyBase
	{
		public int UserIdentifier;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.PlayerDisconnected;
		}
	}

	[Serializable]
	public class UsersInformationRequestBody : RequestBodyBase
	{
		public UserInfo[] UsersInformation;
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.UsersInfo;
		}
	}
}
