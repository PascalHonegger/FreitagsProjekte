using System;
using System.Windows;
using System.Windows.Media.Imaging;
using SpaceInvaders.Ships.EventArgs;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders.Ships
{
	internal abstract class ShipBase : IShip
	{
		protected ShipBase(Point location, Size size, BitmapSource texture)
		{
			Location = location;
			Size = size;
			Texture = texture;
		}

		public Point Location { get; }

		public Size Size { get; }

		public Rect Area => new Rect(Location, Size);

		public void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
		public BitmapSource Texture { get; }

		public void OnShipChanged(object sender, ShipChangedEventArgs e)
		{
			if (e.Killed && Equals(e.ShipUpdated, this))
			{
				if (this is Player)
				{
					//TODO Remove lives

				}
			}
		}
	}
}