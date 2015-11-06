﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SpaceInvaders.Ships;
using SpaceInvaders.Ships.EventArgs;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders
{
	internal class SpaceInvaderViewModel
	{
		public const int MaximumPlayerShots = 3;
		public const int InitialStarCount = 50;
		public static readonly Size PlayAreaSize = new Size(400, 300);
		private readonly List<Invader> _invaders = new List<Invader>();
		private readonly List<IShot> _invaderShots = new List<IShot>();
		private readonly List<IShot> _playerShot = new List<IShot>();
		private readonly Random _random = new Random();
		private readonly List<Point> _stars = new List<Point>();
		private Direction _invaderDirection = Direction.Left;
		private bool _justMovedDown = false;
		private DateTime _lastUpdated = DateTime.MinValue;
		private Player _player;
		private DateTime? _playerDied = null;

		public SpaceInvaderViewModel()
		{
			EndGame();
		}

		public int Score { get; private set; }
		public int Wave { get; private set; }
		public int Lives { get; private set; }
		public bool GameOver { get; private set; }
		public bool PlayerDying => _playerDied.HasValue;

		public event EventHandler<ShipChangedEventArgs> ShipChangedEventHandler;
		public event EventHandler<ShotMovedEventArgs> ShotMovedEventHandler;
		public event EventHandler<StarChangedEventArgs> StarChangedEventHandler;

		public void EndGame()
		{
			GameOver = true;
		}

		protected void OnShipChangedEventHandler(ShipChangedEventArgs e)
		{
			ShipChangedEventHandler?.Invoke(this, e);
		}

		protected void OnShotMovedEventHandler(ShotMovedEventArgs e)
		{
			ShotMovedEventHandler?.Invoke(this, e);
		}

		protected void OnStarChangedEventHandler(StarChangedEventArgs e)
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


		public void NextWave()
		{
			Wave++;
			_invaders.Clear();
			// TODO Add lines of new invaders
			_invaders.Add(new Invader(new Point(),new Size(10, 10), InvaderType.Bug));
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
				MoveInvaders();
			}

			if (!PlayerDying)
			{
				foreach (var shot in _invaderShots)
				{
					shot.Move();
					// TODO Remove shots if they are out of bounds
					OnShotMovedEventHandler(new ShotMovedEventArgs(shot, false));
				}
				foreach (var shot in _playerShot)
				{
					shot.Move();
					// TODO Remove shots if they are out of bounds
					OnShotMovedEventHandler(new ShotMovedEventArgs(shot, false));
				}
			}

			ReturnFire();

			CheckForInvaderCollision();

			CheckForPlayerCollision();
		}

		private void MoveInvaders()
		{
			throw new NotImplementedException();
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
