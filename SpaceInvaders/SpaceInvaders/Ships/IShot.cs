using System.Windows;

namespace SpaceInvaders.Ships
{
	interface IShot
	{
		Direction Direction { get; }
		Point Location { get; }

		void Move();
	}
}