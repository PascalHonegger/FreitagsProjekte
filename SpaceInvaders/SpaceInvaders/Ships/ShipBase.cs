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

		public abstract double Speed { get; }

		public Point Location { get; private set; }

		public Size Size { get; }

		public Rect Area => new Rect(Location, Size);

		public void Move(Direction direction)
		{
			var oldX = Location.X;
			var oldY = Location.Y;

			double newX;
			double newY;

			if (direction == Direction.Left || direction == Direction.Right)
			{
				newX = oldX + Speed*(int) direction;
				newY = oldY;
			}
			else
			{
				newX = oldX;
				newY = oldY + Speed * (int)direction;
			}

			Location = new Point(newX, newY);
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