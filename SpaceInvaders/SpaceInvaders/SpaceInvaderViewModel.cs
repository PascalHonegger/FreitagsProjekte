using System;
using System.Collections.Generic;
using System.Windows;
using SpaceInvaders.Ships;
using SpaceInvaders.Ships.EventArgs;
using SpaceInvaders.Ships.Invader;
using SpaceInvaders.Ships.Player;

namespace SpaceInvaders
{
	internal class SpaceInvaderViewModel
	{
		public static readonly Size PlayAreaSize = new Size(400, 300);
		public const int MaximumPlayerShots = 3;
		public const int InitialStarCount = 50;
		private readonly Random _random = new Random();
		public int Score { get; private set; }
		public int Wave { get; private set; }
		public bool GameOver { get; private set; }
		private DateTime? _playerDied = null;
		public bool PlayerDying => _playerDied.HasValue;
		private IPlayer _player;
		private readonly List<IInvader> _invaders = new List<IInvader>();
		private readonly List<IShot> _playerShot = new List<IShot>();
		private readonly List<IShot> _invaderShots = new List<IShot>();
		private readonly List<Point> _stars = new List<Point>();
		private Direction _invaderDirectio = Direction.Left;
		private bool _justMovedDown = false;
		private DateTime _lastUpdated = DateTime.MinValue;

		public SpaceInvaderViewModel()
		{
			EndGame();
		}

		public void EndGame()
		{
			GameOver = true;
		}

		public void StartGame()
		{
			GameOver = false;

		}
	}
}
