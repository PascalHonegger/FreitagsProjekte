using System;
using System.Windows;

namespace SpaceInvaders.Ships.Invader
{
	internal class Player : ShipBase
	{
		public Player(Point location, Size size) : base(location, size)
		{
		}

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}