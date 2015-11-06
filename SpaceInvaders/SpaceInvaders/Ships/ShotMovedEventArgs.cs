using System;

namespace SpaceInvaders.Ships
{
	class ShotMovedEventArgs : EventArgs
	{
		public IShip ShipUpdated { get; private set; }
		public bool Killed { get; private set; }
		public ShotMovedEventArgs(IShip shipUpdated, bool killed)
		{
			ShipUpdated = shipUpdated;
			Killed = killed;
		}
	}
}
