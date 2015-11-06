using System;
using System.Windows;

namespace SpaceInvaders.Ships.Player
{
	internal class Player : ShipBase, IPlayer
	{
		static readonly Size PlayerSize = new Size(25, 15);
		private const double Speed = 10;
		public Player(Point location) : base(location, PlayerSize)
		{
		}

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}