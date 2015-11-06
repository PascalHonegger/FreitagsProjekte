using System.Windows;

namespace SpaceInvaders.Ships
{
	internal abstract class ShipBase : IShip
	{
		public Point Location { get; protected set; }

		public Size Size { get; protected set; }

		public Rect Area => new Rect(Location, Size);
		public abstract void Move(Direction direction);

		public ShipBase(Point location, Size size)
		{
			Location = location;
			Size = size;
		}
	}
}