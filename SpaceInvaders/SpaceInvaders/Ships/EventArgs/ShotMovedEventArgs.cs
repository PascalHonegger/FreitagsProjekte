namespace SpaceInvaders.Ships.EventArgs
{
	class ShotMovedEventArgs : System.EventArgs
	{
		public IShot Shot { get; private set; }
		public bool Disappeared { get; private set; }
		public ShotMovedEventArgs(IShot shot, bool disappeared)
		{
			Shot = shot;
			Disappeared = disappeared;
		}
	}
}
