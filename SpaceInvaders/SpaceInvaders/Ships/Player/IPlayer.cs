namespace SpaceInvaders.Ships.Player
{
	internal interface IPlayer : IShip
	{
		void Move(Direction direction);
	}
}