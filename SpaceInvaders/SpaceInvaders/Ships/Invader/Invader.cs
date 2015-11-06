namespace SpaceInvaders.Ships.Invader
{
	internal class Invader : IInvader
	{
		public Invader(InvaderType invaderType)
		{
			InvaderType = invaderType;
		}

		public InvaderType InvaderType { get;private set; }
	}
}
