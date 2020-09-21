using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface IProgress : IView
	{
		double Progress { get; }
		Color ProgressColor { get; }

		Task<bool> ProgressTo(double value, uint length, Easing easing);
	}
}