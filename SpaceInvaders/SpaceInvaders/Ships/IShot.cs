using System.Windows;

namespace SpaceInvaders.Ships
{
	internal interface IShot
	{
		Direction Direction { get; }
		Point Location { get; }

		void Move();
	}
}