using System;
using System.Windows;

namespace SpaceInvaders.Ships.Player
{
	internal class Player : ShipBase, IPlayer
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