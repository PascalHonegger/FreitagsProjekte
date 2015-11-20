using System.Windows;
using System.Windows.Controls;
using SpaceInvaders.Ships;

namespace SpaceInvaders.UserControls
{
	/// <summary>
	/// Interaction logic for ShotControl.xaml
	/// </summary>
	public partial class ShotControl : UserControl
	{
		public ShotControl()
		{
			InitializeComponent();
			DataContext = new Shot(new Point(), Direction.Down);
		}
	}
}
