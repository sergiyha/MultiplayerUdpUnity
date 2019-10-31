using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Datagrams.CustomTypes;
using Datagrams.Datagram;

namespace UDPServerTest.UdpClient
{
	public class UserManager
	{
		private Dictionary<int, UserInfoContainer> _usersData = new Dictionary<int, UserInfoContainer>();


		public void AddUser(UserConnectRequestbody payload, IPEndPoint endPoint)
		{
			if (_usersData.ContainsKey(payload.UserIdentifier))
			{
				Console.Write("User was already added id:" + payload.UserIdentifier);
			}
			else
			{
				_usersData.Add(payload.UserIdentifier, new UserInfoContainer {/*there must be random*/ EndPoint = endPoint,UsersInfo = new UsersInfo()
				{
					UserIdentifier = payload.UserIdentifier

				}});
			}
		}

		public void UpdateUser(ConcreteUserInformation payload)
		{
			if (_usersData.ContainsKey(payload.UserInformation.UserIdentifier))
			{
				_usersData[payload.UserInformation.UserIdentifier].UsersInfo = payload.UserInformation;
			}
			else
			{
				Console.Write("User trying to update data but wasn't connected" +
							  payload.UserInformation.UserIdentifier);
			}
		}

		public void RemoveUser(PlayerDisconnectedRequestBody payload)
		{
			if (_usersData.ContainsKey(payload.UserIdentifier))
			{
				_usersData.Remove(payload.UserIdentifier);
			}
			else
			{
				Console.Write("User cannot be disconnected because wasn't connected" +
							  payload.UserIdentifier);
			}
		}

		public List<UserInfoContainer> GetUsersData()
		{
			return _usersData.Values.ToList();
		}
	}

	public class UserInfoContainer
	{
		public IPEndPoint EndPoint;
		public UsersInfo UsersInfo;
	}


}
