using System;
using System.Windows;

namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : ShipBase
	{
		public const int Width = 10;
		public const int Height = 10;
		public Invader(Point location, Size size, InvaderType invaderType) : base(location, size)
		{
			InvaderType = invaderType;
			Location = location;
			Size = size;
		}

		public InvaderType InvaderType { get; private set; }

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}