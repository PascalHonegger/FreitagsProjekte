using System;
using System.Windows;

namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : ShipBase
	{
		public Invader(Point location, Size size, InvaderType invaderType) : base(location, size)
		{
			Score = (int) (InvaderType = invaderType);
			Location = location;
			Size = size;
		}

		public InvaderType InvaderType { get; private set; }
		public int Score { get; }

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}