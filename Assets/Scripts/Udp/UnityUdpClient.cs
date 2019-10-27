using System;
using System.Collections;
using System.Threading.Tasks;
using Datagrams.Datagram;
using tools;
using UnityEngine;
using UnityEngine.UI;

public class UnityUdpClient : MonoBehaviour
{
	[SerializeField] private InputField field;
	[SerializeField] private Button SendButton;
	//private UdpUser _udpUser;
	void Start()
	{
		SendButton.onClick.AddListener(() =>
		{
			Debug.LogError("BUtton Click");
			ClientSend();
		});


		
	}

	IEnumerator SendFrequently()
	{
		while (true)
		{
			yield return new WaitForSeconds(seconds: (float)1 / 60);
			ClientSend();
		}
	}

	private void ClientSend()
	{
		new UdpConnector().Connect((client) => { Debug.Log("seems like connected"); });


		//var transformObject = new TestTransformRequest()
		//{
		//	//Position = new Datagrams.CustomTypes.Vector3(1,2,3),
		//	//Rotation = new Datagrams.CustomTypes.Vector4(2,4,5,6),
		//	Position = new Datagrams.CustomTypes.Vector3(transform.position.x, transform.position.y, transform.position.z),
		//	Rotation = new Datagrams.CustomTypes.Vector4(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w),
		//	Message = "this message was sent by:" + _udpUser.GetHashCode()
		//};

		//byte[] buffer = BinarySerializer.Serialize(transformObject);
		////var t = BinarySerializer.Deserialize<TestTransformRequest>(buffer);
		////Debug.LogError(t.Message + " " + t.Position.x + " " +  t.Rotation);
		//_udpUser.Send(buffer);
	}


}


