using System;
using System.Windows;
using System.Windows.Media.Imaging;
using SpaceInvaders.Ships.EventArgs;

namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : ShipBase
	{
		public const int Width = 10;
		public const int Height = 10;
		public Invader(Point location, Size size, InvaderType invaderType, BitmapSource texture) : base(location, size, texture)
		{
			InvaderType = invaderType;
		}

		private InvaderType InvaderType { get; set; }

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}