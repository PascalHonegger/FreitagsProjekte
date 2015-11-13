using System;
using System.Collections.Generic;
using System.Linq;
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
using SpaceInvaders.Ships;

namespace SpaceInvaders.UserControlés
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
