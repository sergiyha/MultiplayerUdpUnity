using System;

public interface  ITickableManager
{
	void Register(Action action);
	void Unregister(Action action);
}
