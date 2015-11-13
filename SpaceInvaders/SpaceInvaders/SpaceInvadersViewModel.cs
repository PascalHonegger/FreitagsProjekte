using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;
using SpaceInvaders.Ships;
using SpaceInvaders.Ships.EventArgs;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders
{
	internal class SpaceInvadersViewModel
	{
		private const int MaximumPlayerShots = 3;
		private const int InitialStarCount = 50;
		private const int PlayAreaWidth = 400;
		private const int PlayAreaHeight = 300;
		public static readonly Size PlayAreaSize = new Size(PlayAreaWidth, PlayAreaHeight);

		private static readonly Point PlayerStartPoint = new Point(); //TODO
		private readonly List<IShot> _invaderShots = new List<IShot>();
		private readonly List<IShot> _playerShots = new List<IShot>();
		private readonly Random _random = new Random();
		private readonly List<Point> _stars = new List<Point>();
		private Direction _invaderDirection = Direction.Left;
		private List<Invader> _invaders = new List<Invader>();
		private bool _justMovedDown = false;
		private DateTime _lastUpdated = DateTime.MinValue;
		private Player _player;
		private DateTime? _playerDied = null;

		public SpaceInvadersViewModel()
		{
			EndGame();
		}

		private Timer UpdateTimer { get; } = new Timer(1000);

		public int Score { get; private set; }
		public int Wave { get; private set; }
		public int Lives { get; private set; }
		public bool GameOver { get; private set; }
		public bool PlayerDying => _playerDied.HasValue;

		public event EventHandler<ShipChangedEventArgs> ShipChangedEventHandler;
		public event EventHandler<ShotMovedEventArgs> ShotMovedEventHandler;
		public event EventHandler<StarChangedEventArgs> StarChangedEventHandler;

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

			foreach (var point in _stars)
			{
				OnStarChangedEventHandler(new StarChangedEventArgs(point, true));
			}

			for (var i = 0; i < InitialStarCount; i++)
			{
				_stars.Add(new Point()); //TODO RANDOM KORDINATES
			}

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

		public void Twinkle()
		{
			// Add Star
			if (_random.Next(1, 100) > 50 && _stars.Count < InitialStarCount*1.5)
			{
				/*var _ran = new Random();
				var star = new Point(_ran.Next());*/
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

			if (!PlayerDying)
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
			foreach (var ship in _invaders.ToList())
			{
				foreach (var shot in _playerShots)
				{
					if (FindCollisions(ship, shot))
					{
						OnShipChangedEventHandler(new ShipChangedEventArgs(ship, true));
					}
				}

				if (FindCollisions(ship.Location, ship.Size.Width, ship.Size.Height, _player.Location, _player.Size.Width,
					_player.Size.Height))
				{
					OnShipChangedEventHandler(new ShipChangedEventArgs(ship, true));
				}
			}
		}

		private bool FindCollisions(IShip ship, IShot shot)
		{
			return FindCollisions(ship.Location, ship.Size.Width, ship.Size.Height, shot.Location, Shot.ShotSize.Width,
				Shot.ShotSize.Height);
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
			foreach (var invader in _invaders)
			{
				FireShot(invader, Direction.Down);
			}
		}

		private bool FindCollisions(Point point1, double width1, double height1, Point point2, double width2, double height2)
		{
			var rect1 = new Rect(point1.X, point1.Y, width1, height1);
			var rect2 = new Rect(point2.X, point2.Y, width2, height2);

			return rectsOverlap(rect1, rect2);
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
	}
}