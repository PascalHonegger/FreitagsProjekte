using System;
using System.Windows;
using SpaceInvaders.Ships.EventArgs;

namespace SpaceInvaders.Ships.Invader
{
	internal class Player : ShipBase
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