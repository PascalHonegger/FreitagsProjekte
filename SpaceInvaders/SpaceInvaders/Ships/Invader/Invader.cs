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
			Score = (int) invaderType;
		}

		public InvaderType InvaderType { get; private set; }
		public int Score { get; }

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}