using System;
using System.Windows;

namespace SpaceInvaders.Ships
{
	class Shot : IShot
	{
		private const double ShotPixelsPerSecond = 95;

		public Point Location { get; private set; }
		public static Size ShotSize = new Size(2, 10);

		public Direction Direction { get; }

		private DateTime _lastMoved;

		public Shot(Point location, Direction direction)
		{
			Location = location;
			Direction = direction;
		}

		public void Move()
		{
			var timeSinceLastMoved = DateTime.Now - _lastMoved;
			var distance = timeSinceLastMoved.Milliseconds * ShotPixelsPerSecond / 1000;
			if (Direction == Direction.Up) distance += -1;
			Location = new Point(Location.X, Location.Y + distance);
			_lastMoved = DateTime.Now;
		}
	}
}