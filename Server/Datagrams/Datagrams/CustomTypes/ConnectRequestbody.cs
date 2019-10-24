using System;
using Datagrams.Datagram;

namespace Datagrams.CustomTypes
{
	[Serializable]
	public class ConnectRequestbody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.Connect;
		}
	}

	[Serializable]
	public class ConnectedRequestBody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.Connected;
		}
	}

	[Serializable]
	public class DisconnectRequestBody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.Disconnect;
		}
	}

	[Serializable]
	public class DisconnectedRequestBody : RequestBodyBase
	{
		public override RequestIdentifiers GetDatagramId()
		{
			return RequestIdentifiers.Disconnected;
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
