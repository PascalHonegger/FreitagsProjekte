namespace SpaceInvaders.Ships.EventArgs
{
	class ShotMovedEventArgs : System.EventArgs
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
