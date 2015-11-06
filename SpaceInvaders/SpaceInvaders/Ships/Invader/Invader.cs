using System.Windows;

namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : IInvader
	{
		public Invader(InvaderType invaderType)
		{
			InvaderType = invaderType;
		}

		public InvaderType InvaderType { get;private set; }
		public Rect Area { get; }
		public Point Location { get; }
		public Size Size { get; }
		public void Move(Direction direction)
		{
			throw new System.NotImplementedException();
		}
	}
}
