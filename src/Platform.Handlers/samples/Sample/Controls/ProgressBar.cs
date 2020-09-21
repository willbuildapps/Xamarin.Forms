using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class ProgressBar : View, IProgress
	{
		public double Progress { get; set; }
		public Color ProgressColor { get; set; }
	}
}