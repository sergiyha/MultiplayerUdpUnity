using System.Diagnostics;
using System.IO;
using Extensions;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class TopPanelScripts
{
	[MenuItem("Tools/Grab Datagram Dll")]
	private static void GrabDatagramDll()
	{
		string originDllPath = Application.dataPath.Up() + "/Server/Datagrams/Datagrams/bin/Debug/Datagrams.dll";


		string[] destinationPath = new[]
		{
			Application.dataPath + "/libs/Datagrams.dll",
			Application.dataPath.Up() + "/Server/UDPServerTest/UDPServerTest/libs/Datagrams.dll"

		};

		foreach (var p in destinationPath)
		{
			File.Copy(originDllPath, p, true);
		}
	}
}
