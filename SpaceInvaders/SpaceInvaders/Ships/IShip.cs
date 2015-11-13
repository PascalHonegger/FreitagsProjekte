using System.Windows;
using System.Windows.Media.Imaging;

namespace SpaceInvaders.Ships
{
	internal interface IShip
	{
		Rect Area { get; }
		Point Location { get; }
		Size Size { get; }

		void Move(Direction direction);
		BitmapSource Texture { get;}
	}
}