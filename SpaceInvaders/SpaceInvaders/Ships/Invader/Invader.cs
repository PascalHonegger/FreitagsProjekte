using System.Windows;
using System.Windows.Media.Imaging;

namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : ShipBase
	{
		public const int Width = 10;
		public const int Height = 10;
		private static readonly Size InvaderSize = new Size(Width, Height);
		public Invader(Point location, InvaderType invaderType, BitmapSource texture) : base(location, InvaderSize, texture)
		{
			InvaderType = invaderType;
		}

		private InvaderType InvaderType { get; set; }

		public override double Speed => 10;
	}
}