public interface IDataUpdaterManager : IDataListener
{
	void AddUpdatable<T>(int id, IUpdatableUserInfo<T> updatable);
}