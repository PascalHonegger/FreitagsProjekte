using System;
using System.Windows;
using SpaceInvaders.Ships.EventArgs;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders.Ships
{
	internal abstract class ShipBase : IShip
	{
		protected ShipBase(Point location, Size size)
		{
			Location = location;
			Size = size;
		}

		public Point Location { get; protected set; }

		public Size Size { get; }

		public Rect Area => new Rect(Location, Size);
		public abstract void Move(Direction direction);

		public void OnShipChanged(object sender, ShipChangedEventArgs e)
		{
			if (e.Killed && Equals(e.ShipUpdated, this))
			{
				if (this is Player)
				{
					
				}
			}
		}
	}
}