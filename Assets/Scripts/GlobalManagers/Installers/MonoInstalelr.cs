using System;
using UnityEngine;
using Zenject;

public class MonoInstalelr : MonoInstaller
{
	[SerializeField]
	private MonoTickerManager MonoTickerManager;

	[SerializeField]
	private StartManager StartManager;

	[SerializeField]
	private GameObject ConnectedUserMonoView;

	[SerializeField]
	private GameObject OwnerUserMonoView;


	public override void InstallBindings()
	{
		Container.Bind<IGameManager>().To<GameManager>().AsSingle().NonLazy();
		Container.Bind<ITickableManager>().FromInstance(MonoTickerManager).AsSingle();
		Container.Bind<IDataUpdaterManager>().To<DataUpdaterManger>().AsSingle().NonLazy();
		Container.Bind<StartManager>().FromInstance(StartManager);

		Container.BindFactory<ConnectedUserMonoView, ConnectedUserMonoView.Factory>().FromComponentInNewPrefab(ConnectedUserMonoView);
		Container.BindFactory<OwnerUserMonoView, OwnerUserMonoView.Factory>().FromComponentInNewPrefab(OwnerUserMonoView);


	}
}
