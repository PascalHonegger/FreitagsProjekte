using System.Windows;
using System.Windows.Media.Imaging;

namespace SpaceInvaders.Ships.Invader
{
	internal class Player : ShipBase
	{
		private static readonly Size PlayerSize = new Size(25, 15);
		//TODO Import Image
		private static readonly BitmapSource PlayerTexutre = new BitmapImage();
		public Player(Point location) : base(location, PlayerSize, PlayerTexutre)
		{
		}

		public override double Speed => 10;
	}
}