using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using SpaceInvaders.Ships;
using SpaceInvaders.Ships.EventArgs;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders
{
	internal class SpaceInvadersModel
	{
		private const int MaximumPlayerShots = 3;
		private const int InitialStarCount = 50;
		private const int PlayAreaWidth = 400;
		private const int PlayAreaHeight = 300;
		public Size PlayAreaSize = new Size(PlayAreaWidth, PlayAreaHeight);

		private static readonly Point PlayerStartPoint = new Point(); //TODO
		private readonly List<IShot> _invaderShots = new List<IShot>();
		private readonly List<IShot> _playerShots = new List<IShot>();
		public static readonly Random Random = new Random();
		private List<Invader> _invaders = new List<Invader>();
		private bool _justMovedDown;
		private DateTime _lastUpdated = DateTime.MinValue;
		private Player _player;
		private DateTime? _playerDied = null;

		private Timer UpdateTimer { get; } = new Timer(100);

		public int Score { get; set; }
		private int Wave { get; set; }
		private int Lives { get; set; }
		public bool GameOver { get; set; }
		private bool PlayerDying => _playerDied.HasValue;

		public event EventHandler<ShipChangedEventArgs> ShipChangedEventHandler;
		public event EventHandler<ShotMovedEventArgs> ShotMovedEventHandler;

		private void EndGame()
		{
			GameOver = true;
			UpdateTimer.Stop();
		}

		private void OnShipChangedEventHandler(ShipChangedEventArgs e)
		{
			ShipChangedEventHandler?.Invoke(this, e);
		}

		private void OnShotMovedEventHandler(ShotMovedEventArgs e)
		{
			ShotMovedEventHandler?.Invoke(this, e);
		}

		public void StartGame()
		{
			GameOver = false;

			foreach (var invader in _invaders)
			{
				OnShipChangedEventHandler(new ShipChangedEventArgs(invader, true));
			}
			_invaders.Clear();

			foreach (var shot in _playerShots)
			{
				OnShotMovedEventHandler(new ShotMovedEventArgs(shot, true));
			}
			_playerShots.Clear();


			foreach (var shot in _invaderShots)
			{
				OnShotMovedEventHandler(new ShotMovedEventArgs(shot, true));
			}
			_invaderShots.Clear();

			_player = new Player(PlayerStartPoint);
			ShipChangedEventHandler += _player.OnShipChanged;
			OnShipChangedEventHandler(new ShipChangedEventArgs(_player, false));

			Wave = 0;
			Lives = 2;
			Score = 0;

			NextWave();

			UpdateTimer.Elapsed += (sender, args) => { Update(); };
			UpdateTimer.Start();
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
				var invader = new Invader(new Point(currentX, currentY), GetInvaderType(), new BitmapImage());
				ShipChangedEventHandler += invader.OnShipChanged;
				attackers.Add(invader);
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
			InvaderType type;
			switch (Wave%6)
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

		private void FireShot(IShip ship, Direction direction)
		{
			if (ship is Player && _playerShots.Count < MaximumPlayerShots || ship is Invader)
			{
				var shot = new Shot(ship.Location, direction);

				if (ship is Player)
				{
					_playerShots.Add(shot);
				}
				else
				{
					_invaderShots.Add(shot);
				}

				OnShotMovedEventHandler(new ShotMovedEventArgs(shot, false));
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

		private void Update()
		{
			if (_invaders.Count == 0)
			{
				NextWave();
			}

			if (PlayerDying)
			{
				OnShipChangedEventHandler(new ShipChangedEventArgs(_player, true));
			}
			else
			{
				foreach (var shot in _invaderShots)
				{
					shot.Move();
					OnShotMovedEventHandler(new ShotMovedEventArgs(shot, IsOutOfBounds(shot.Location)));
				}
				foreach (var shot in _playerShots)
				{
					shot.Move();
					OnShotMovedEventHandler(new ShotMovedEventArgs(shot, IsOutOfBounds(shot.Location)));
				}
			}

			CheckForInvaderCollision();

			CheckForPlayerCollision();

			MoveInvaders();

			ReturnFire();
		}

		private static bool IsOutOfBounds(Point shot)
		{
			return Math.Abs(shot.X - PlayAreaHeight) < 0.5 || Math.Abs(shot.X) < 0.5;
		}

		private void MoveInvaders()
		{
			if (_lastUpdated >= DateTime.Today.AddSeconds(10 - Wave))
			{
				return;
			}

			foreach (var invader in _invaders)
			{
				invader.Move(_justMovedDown ? Direction.Right : Direction.Left);
			}

			if (_invaders.Any(invader => IsOutOfBounds(invader.Location)))
			{
				_justMovedDown = !_justMovedDown;
				foreach (var invader in _invaders)
				{
					invader.Move(_justMovedDown ? Direction.Right : Direction.Left);
					invader.Move(Direction.Down);
				}
			}
		}

		private void CheckForInvaderCollision()
		{
			foreach (var ship in _invaders.ToList())
			{
				foreach (var shot in _playerShots)
				{
					if (FindCollisions(ship, shot))
					{
						OnShipChangedEventHandler(new ShipChangedEventArgs(ship, true));
					}
				}

				if (FindCollisions(ship, _player))
				{
					OnShipChangedEventHandler(new ShipChangedEventArgs(ship, true));
				}
			}
		}

		private bool FindCollisions(IShip ship, IShot shot)
		{
			var rect1 = ship.Area;
			var rect2 = new Rect(shot.Location.X, shot.Location.Y, Shot.ShotSize.Width, Shot.ShotSize.Height);

			return rectsOverlap(rect1, rect2);
		}

		private void CheckForPlayerCollision()
		{
			foreach (var shot in _invaderShots)
			{
				if (FindCollisions(_player, shot))
				{
					OnShipChangedEventHandler(new ShipChangedEventArgs(_player, true));
				}
			}
		}

		private void ReturnFire()
		{
			if (_invaderShots.Count > Wave + 1)
			{
				return;
			}

			if (Random.Next(10) < 10 - Wave)
			{
				return;
			}

			var invader = _invaders.OrderByDescending(i => i.Location.X).PickRandom();

			FireShot(invader, Direction.Down);
		}

		private bool FindCollisions(IShip ship1, IShip ship2)
		{
			return rectsOverlap(ship1.Area, ship2.Area);
		}

		private bool rectsOverlap(Rect rect1, Rect rect2)
		{
			rect1.Intersect(rect2);
			if (rect1.Width > 0 || rect1.Height > 0)
				return true;
			return false;
		}

		public void FireShotPlayer()
		{
			FireShot(_player, Direction.Up);
		}

		public void UpdateAllShipsAndStars()
		{
			
		}
	}
}