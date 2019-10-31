using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Datagrams.AbstractTypes;
using Datagrams.CustomTypes;
using Datagrams.Datagram;
using tools;
using UdpClient;

namespace UDPServerTest.UdpClient
{
	public class ConnectionMediator
	{
		private UserManager _userManager;

		public void InjectDependencies(UserManager userManager)
		{
			_userManager = userManager;
		}

		private UdpListener _listener;

		public void StartStreaming()
		{
			_listener = new UdpListener();
			StartListening();
			StartSending();
		}

		private void StartListening()
		{
			Task.Run(() => CheckReceivedInformation());
		}

		private void StartSending()
		{
			Task.Run(() => SendInformation());
		}


		private void SendInformation()
		{
			while (true)
			{
				try
				{
					lock (connectDisconnectLock)
					{
						var usersInformation = _userManager.GetUsersData();
						foreach (var userInfo in usersInformation)
						{
							if(usersInformation.Count == 1)continue;

							var userInfoRequestBody = new UsersInformationRequestBody()
							{
								UserId = userInfo.UsersInfo.UserIdentifier.ToString(),
								UsersInformation = new UsersInfo[usersInformation.Count - 1]
							};


							var addingCount = 0;
							foreach (var _userInfo in usersInformation)
							{
								if (_userInfo.UsersInfo.UserIdentifier == userInfo.UsersInfo.UserIdentifier) continue;
								userInfoRequestBody.UsersInformation[addingCount] = _userInfo.UsersInfo;
								addingCount++;
							}

							var bytes = BinarySerializer.Serialize(userInfoRequestBody);
							_listener.Send(bytes, userInfo.EndPoint);
						}

					}
					Thread.Sleep(16);
				}
				catch (Exception e)
				{
					Console.Write("Error till sending data: " + e + "\n");
					SendInformation();
				}
			}
		}

		private async void CheckReceivedInformation()
		{
			while (true)
			{
				try
				{
					var received = await _listener.Receive();
					var receivedDatagram = BinarySerializer.Deserialize<Datagrams.AbstractTypes.AbstractDatagram>(received.Datagram);
					ManageReceivedData(receivedDatagram, received.EndPoint);
				}
				catch (Exception e)
				{
					Console.Write("connection Mediator pars datagram error exception " + e);
					StartListening();
				}
			}
		}

		object userManagerLock = new object();
		private object connectDisconnectLock = new object();


		public void ManageReceivedData(AbstractDatagram datagram, IPEndPoint endPoint)
		{
			Task.Run(() => { FilterRequest(datagram, endPoint); });
		}

		public void FilterRequest(AbstractDatagram datagram, IPEndPoint ebdPoint)
		{
			lock (userManagerLock)
			{
				switch (datagram.GetDatagramId())
				{
					case RequestIdentifiers.UserConnect:
						{
							lock (connectDisconnectLock)
							{
								var id = ((UserConnectRequestbody) datagram).UserIdentifier;
								_listener.Send
									(BinarySerializer.Serialize(new UserConnectedRequestBody() { UserInformation = new UsersInfo() { UserIdentifier = id } }),
									ebdPoint);
								_userManager.AddUser((UserConnectRequestbody) datagram, ebdPoint);
								break;
							}
						}
					case RequestIdentifiers.PlayerDisconnected:
						{
							lock (connectDisconnectLock)
							{
								_userManager.RemoveUser(datagram as PlayerDisconnectedRequestBody);
								break;
							}
						}
					case RequestIdentifiers.UsersInfo:
						{
							_userManager.UpdateUser(datagram as ConcreteUserInformation);
							break;
						}
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}





	}
}
