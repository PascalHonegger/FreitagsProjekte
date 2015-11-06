using System;
using System.Windows;

namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : ShipBase
	{
		public Invader(Point location, Size size, InvaderType invaderType) : base(location, size)
		{
			InvaderType = invaderType;
			Location = location;
			Size = size;
		}

		public InvaderType InvaderType { get; private set; }
		public Rect Area { get; }

		public Point Location { get; }
		public Size Size { get; }

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}