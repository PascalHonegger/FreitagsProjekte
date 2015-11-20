using System.Windows.Controls;

namespace SpaceInvaders.UserControls
{
	/// <summary>
	/// Interaction logic for ShipControl.xaml
	/// </summary>
	public partial class ShipControl : UserControl
	{
		public ShipControl()
		{
			InitializeComponent();
		}

		public void InvaderShot()
		{
			invaderShotStoryboard.Begin();
		}

		public void StartFlashing()
		{
			flashStoryboard.Begin();
		}

		public void StopFlashing()
		{
			flashStoryboard.Stop();
		}
	}
}
