public interface IDataUpdaterManager
{
	void AddUpdatable<T>(int id, IUpdatableUserInfo<T> updatable);
}