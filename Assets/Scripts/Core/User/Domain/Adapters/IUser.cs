namespace Core.User.Domain.Adapters
{
	public interface IUser
	{
		string Name { get; }

		void SetName(string name);
	}
}