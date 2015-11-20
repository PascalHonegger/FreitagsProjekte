using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceInvaders.UserControlés
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
