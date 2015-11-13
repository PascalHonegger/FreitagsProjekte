﻿using System.Windows;
using System.Windows.Media.Imaging;

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
	}
}