using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using SpaceInvaders.Ships;
using SpaceInvaders.Ships.EventArgs;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders
{
	internal class SpaceInvaderViewModel
	{
		private const int MaximumPlayerShots = 3;
		private const int InitialStarCount = 50;
		private const int PlayAreaWidth = 400;
		private const int PlayAreaHeight = 300;
		public static readonly Size PlayAreaSize = new Size(PlayAreaWidth, PlayAreaHeight);
		private readonly List<IShot> _invaderShots = new List<IShot>();
		private readonly List<IShot> _playerShot = new List<IShot>();
		private readonly Random _random = new Random();
		private readonly List<Point> _stars = new List<Point>();
		private Direction _invaderDirection = Direction.Left;
		private List<Invader> _invaders = new List<Invader>();
		private bool _justMovedDown = false;
		private DateTime _lastUpdated = DateTime.MinValue;
		private Player _player;
		private DateTime? _playerDied = null;

		public SpaceInvaderViewModel()
		{
			EndGame();
			UpdateTimer = new Timer(1000);
			UpdateTimer.Elapsed += (sender, args) => { Update();};
			UpdateTimer.Start();
		}

		private Timer UpdateTimer { get; }

		public int Score { get; private set; }
		public int Wave { get; private set; }
		public int Lives { get; private set; }
		public bool GameOver { get; private set; }
		public bool PlayerDying => _playerDied.HasValue;

		private int Level { get; } = 1;

		public event EventHandler<ShipChangedEventArgs> ShipChangedEventHandler;
		public event EventHandler<ShotMovedEventArgs> ShotMovedEventHandler;
		public event EventHandler<StarChangedEventArgs> StarChangedEventHandler;

		private void EndGame()
		{
			GameOver = true;
		}

		private void OnShipChangedEventHandler(ShipChangedEventArgs e)
		{
			ShipChangedEventHandler?.Invoke(this, e);
		}

		private void OnShotMovedEventHandler(ShotMovedEventArgs e)
		{
			ShotMovedEventHandler?.Invoke(this, e);
		}

		private void OnStarChangedEventHandler(StarChangedEventArgs e)
		{
			StarChangedEventHandler?.Invoke(this, e);
		}

		public void StartGame()
		{
			GameOver = false;

			foreach (var invader in _invaders)
			{
				OnShipChangedEventHandler(new ShipChangedEventArgs(invader, true));
			}
			_invaders.Clear();

			foreach (var shot in _playerShot)
			{
				OnShotMovedEventHandler(new ShotMovedEventArgs(shot, true));
			}
			_playerShot.Clear();


			foreach (var shot in _invaderShots)
			{
				OnShotMovedEventHandler(new ShotMovedEventArgs(shot, true));
			}
			_invaderShots.Clear();

			foreach (var point in _stars)
			{
				OnStarChangedEventHandler(new StarChangedEventArgs(point, true));
			}

			for (var i = 0; i < InitialStarCount; i++)
			{
				_stars.Add(new Point()); //TODO RANDOM KORDINATES
			}

			_player = new Player(new Point());
			OnShipChangedEventHandler(new ShipChangedEventArgs(_player, false));
			Wave = 0;
			Lives = 2;

			NextWave();
		}

		private void NextWave()
		{
			Wave++;
			_invaders = CreateNewAttackWave().ToList();
		}

		private IEnumerable<Invader> CreateNewAttackWave()
		{
			IList<Invader> attackers = new List<Invader>();

			var currentX = Invader.Height*1.4;
			var currentY = Invader.Width*1.4;
			for (var i = 0; i < 16; i++)
			{
				attackers.Add(new Invader(new Point(currentX, currentY), new Size(Invader.Width, Invader.Height), GetInvaderType()));
				currentX += Invader.Width*2.4;
				if (IsOutOfBounds(new Point(currentX*2.4, currentY)))
				{
					currentX = Invader.Height*1.4;
					currentY += Invader.Height + Invader.Height*1.4;
				}
			}

			return attackers;
		}

		private InvaderType GetInvaderType()
		{
			InvaderType type = 0;
			switch (Level%6)
			{
				case 0:
					type = InvaderType.Mothership;
					break;
				case 1:
					type = InvaderType.Saucer;
					break;
				case 2:
					type = InvaderType.Star;
					break;
				case 3:
					type = InvaderType.Satellite;
					break;
				case 4:
					type = InvaderType.Bug;
					break;
				case 5:
					type = InvaderType.Spaceship;
					break;
				default:
					throw new NotImplementedException("Invadertype not implementet");
			}
			return type;
		}

		public void FireShot()
		{
			if (_playerShot.Count < MaximumPlayerShots)
			{
				var shot = new Shot(_player.Location, Direction.Up);
				_playerShot.Add(shot);
				OnShotMovedEventHandler(new ShotMovedEventArgs(shot, true));
			}
		}

		public void MovePlayer(Direction direction)
		{
			if (PlayerDying)
			{
				return;
			}
			_player.Move(direction);
			OnShipChangedEventHandler(new ShipChangedEventArgs(_player, false));
		}

		public void Twinkle()
		{
			// Add Star
			if (_random.Next(1, 100) > 50 && _stars.Count < InitialStarCount*1.5)
			{
				var star = new Point();
				_stars.Add(star);
				OnStarChangedEventHandler(new StarChangedEventArgs(star, true));
			}
			// Remove Star
			else if (_stars.Count > InitialStarCount*0.75)
			{
				var star = _stars.Last();
				_stars.Remove(star);
				OnStarChangedEventHandler(new StarChangedEventArgs(star, false));
			}
		}

		public void Update()
		{
			if (_invaders.Count == 0)
			{
				NextWave();
			}

			if (PlayerDying)
			{
				OnShipChangedEventHandler(new ShipChangedEventArgs(_player, true));
			}

			if (!PlayerDying)
			{
				foreach (var shot in _invaderShots)
				{
					shot.Move();
					OnShotMovedEventHandler(new ShotMovedEventArgs(shot, IsOutOfBounds(shot.Location)));
				}
				foreach (var shot in _playerShot)
				{
					shot.Move();
					OnShotMovedEventHandler(new ShotMovedEventArgs(shot, IsOutOfBounds(shot.Location)));
				}
			}

			ReturnFire();

			CheckForInvaderCollision();

			CheckForPlayerCollision();

			MoveInvaders();
		}

		private static bool IsOutOfBounds(Point shot)
		{
			return Math.Abs(shot.X - PlayAreaHeight) < 0.5 || Math.Abs(shot.X) < 0.5;
		}

		private void MoveInvaders()
		{
			foreach (var invader in _invaders)
			{
				invader.Move(_invaderDirection);
			}

			if (_invaders.Any(invader => IsOutOfBounds(invader.Location)))
			{
				_invaderDirection = Direction.Left;
				foreach (var invader in _invaders)
				{
					invader.Move(_invaderDirection);
					invader.Move(Direction.Down);
				}
			}
		}

		private void CheckForInvaderCollision()
		{
			throw new NotImplementedException();
		}

		private void CheckForPlayerCollision()
		{
			throw new NotImplementedException();
		}

		private void ReturnFire()
		{
			throw new NotImplementedException();
		}
	}
}