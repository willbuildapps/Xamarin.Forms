using Xamarin.Platform.Handlers;
using RegistrarHandlers = Xamarin.Platform.Registrar;

namespace Sample
{
	public class Platform
	{
		static bool HasInit;

		public static void Init()
		{
			if (HasInit)
				return;

			HasInit = true;

			RegistrarHandlers.Handlers.Register<ActivityIndicator, ActivityIndicatorHandler>();
			RegistrarHandlers.Handlers.Register<Button, ButtonHandler>();
		}
	}
}