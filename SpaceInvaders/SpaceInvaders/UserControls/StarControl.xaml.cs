using System.Windows.Controls;
using System.Windows.Media;

namespace SpaceInvaders.UserControls
{
	/// <summary>
	/// Interaction logic for StarControl.xaml
	/// </summary>
	public partial class StarControl : UserControl
	{
		public StarControl()
		{
			InitializeComponent();
		}

		public void SetFill(Brush color)
		{
			Star.Fill = color;
		}
	}
}
