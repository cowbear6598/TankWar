using Core.Controller.Domain;
using VContainer;

namespace Core.Controller.Infrastructure
{
	public class ControllerService : IControllerService
	{
		[Inject] private readonly PCInput _input;
	}
}