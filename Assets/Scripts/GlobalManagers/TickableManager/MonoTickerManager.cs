using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoTickerManager : MonoBehaviour, ITickableManager
{
	private List<Action> _actions = new List<Action>();
	private float _timeToTick = 1 / 30f;

	private void Awake()
	{
		StartCoroutine(StartTicking());
	}

	public void Register(Action action)
	{
		if (_actions.Contains(action))
		{
			Debug.LogError("Action Duplicated");
			return;
		}

		_actions.Add(action);
	}

	public void Unregister(Action action)
	{
		if (_actions.Contains(action))
		{
			_actions.Remove(action);
		}
	}


	private IEnumerator StartTicking()
	{
		while (true)
		{

			for (int i = 0; i < _actions.Count; i++)
			{
				_actions[i]?.Invoke();
			}
			yield return new WaitForSeconds(_timeToTick);
		}
	}
}
