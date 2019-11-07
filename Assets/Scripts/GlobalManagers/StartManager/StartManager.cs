using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartManager : MonoBehaviour
{
	[SerializeField]
	private Button _startButton;

	[Inject] private IGameManager _gameManager; 

	private void Awake()
	{
		_startButton.onClick.AddListener(OnStartButtonClicked);
	}

	private void OnStartButtonClicked()
	{
		_gameManager.StartGame();
	}
}
