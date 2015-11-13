using System;
using System.Windows;
using SpaceInvaders.Ships.EventArgs;

namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : ShipBase
	{
		public const int Width = 10;
		public const int Height = 10;
		private static readonly Size InvaderSize = new Size(Width, Height);
		public Invader(Point location, InvaderType invaderType) : base(location, InvaderSize)
		{
			InvaderType = invaderType;
			Location = location;
		}

		private InvaderType InvaderType { get; set; }

		public override void Move(Direction direction)
		{
			throw new NotImplementedException();
		}
	}
}