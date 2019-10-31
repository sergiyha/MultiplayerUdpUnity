using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		new UdpUserConnector().Connect(123456, (a, b) => { Debug.Log("Connected");});    
    }

   
}
