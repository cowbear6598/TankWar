using Core.User.Domain.Adapters;

namespace Core.User.Domain
{
	public class User : IUser
	{
		public string Name { get; private set; }

		public void SetName(string name) => Name = name;
	}
}