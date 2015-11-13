using System;
using System.Windows;
using System.Windows.Media.Imaging;
using SpaceInvaders.Ships.EventArgs;

namespace SpaceInvaders.Ships.Invader
{
	internal class Player : ShipBase
	{
		static readonly Size PlayerSize = new Size(25, 15);
		private const double Speed = 10;
		//TODO Import Image
		private static readonly BitmapSource PlayerTexutre = new BitmapImage();
		public Player(Point location) : base(location, PlayerSize, PlayerTexutre)
		{
		}

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}