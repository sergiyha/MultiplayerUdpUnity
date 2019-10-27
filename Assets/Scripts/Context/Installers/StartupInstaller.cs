using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartupInstaller : MonoInstaller
{
	public MonoTickerManager _monoTikableManager;
	public override void InstallBindings()
	{
		Container.Bind<ITickableManager>().FromInstance(_monoTikableManager);
	}
}
