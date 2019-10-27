using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITickableManager
{
	void Register(Action action);
	void Unregister(Action action);
}
