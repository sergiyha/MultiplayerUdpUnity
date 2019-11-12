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
							if (usersInformation.Count == 1) continue;

							var userInfoRequestBody = new UsersInformationRequestBody()
							{
								UserId = userInfo.UsersInfo.UserIdentifier.ToString(),
								UsersInformation = new UserInfo[usersInformation.Count - 1]
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
					Thread.Sleep(33);
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
					var receivedDatagram = BinarySerializer.Deserialize<AbstractDatagram>(received.Datagram);
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

		public void FilterRequest(AbstractDatagram datagram, IPEndPoint endPoint)
		{
			lock (userManagerLock)
			{
				switch (datagram.GetDatagramId())
				{
					case RequestIdentifiers.UserConnect:
						{
							lock (connectDisconnectLock)
							{
								var id = ((UserConnectRequestbody)datagram).UserIdentifier;
								var initialUserData = GetInitialUserData(id);
								_listener.Send(BinarySerializer.Serialize(initialUserData), endPoint);
								Console.Write("User Connected id: " + id + "\n");
								PlayerConnectedRequest(initialUserData.UserInformation);

								_userManager.AddUser((UserConnectRequestbody)datagram, endPoint);
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
					case RequestIdentifiers.ConcreteUserInformation:
						{
							_userManager.UpdateUser(datagram as ConcreteUserInformation);
							break;
						}
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private void PlayerConnectedRequest(UserInfo userInfo)
		{
			var usersInformation = _userManager.GetUsersData();
			foreach (var userData in usersInformation)
			{
				var byteData = BinarySerializer.Serialize(new PlayerConnectedRequestBody()
				{
					UserInformation = userInfo
				});

				_listener.Send(byteData, userData.EndPoint);
			}
			Console.Write(usersInformation.Count + " users were aware of it\n");
		}

		private UserConnectedRequestBody GetInitialUserData(int userId)
		{
			var random = new Random();
			return new UserConnectedRequestBody()
			{
				ExistedPlayersInformation = _userManager.GetExistedUsersInfo(),
				UserInformation = new UserInfo()
				{
					UserIdentifier = userId,
					Transform = new TransformDatagram()
					{
						Position = new Vector3
						{
							x = random.Next(1, 10),
							y = random.Next(1, 10),
							z = random.Next(1, 10)
						},
						Rotation = new Vector4
						{
							w = 1,
							x = 0,
							y = 0,
							z = 0
						}
					}
				}
			};
		}
	}
}
