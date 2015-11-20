using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Threading;
using SpaceInvaders.Ships;
using SpaceInvaders.Ships.Invader;

namespace SpaceInvaders
{
	class SpaceInvadersViewModel
	{
		private readonly ObservableCollection<FrameworkElement> _sprites = new ObservableCollection<FrameworkElement>();

		public INotifyCollectionChanged Sprites => _sprites;

		public bool GameOver => _model.GameOver;

		private readonly ObservableCollection<object> _lives = new ObservableCollection<object>();

		public INotifyCollectionChanged Lives => _lives;

		public bool Paused { get; set; }
		private bool _lastPaused = true;

		public static double Scale { get; private set; }

		public int Score { get; private set; }

		public Size PlayAreaSize
		{
			set
			{
				Scale = value.Width/405;
				_model.UpdateAllShipsAndStars();
				//TODO RecreateScanLines();
			}
		}
		private readonly SpaceInvadersModel _model = new SpaceInvadersModel();

		private readonly DispatcherTimer _timer = new DispatcherTimer();
		private FrameworkElement _playerControl = null;
		private bool _playerFlashing = false;
		private readonly Dictionary<Invader, FrameworkElement> _invaders = 
										new Dictionary<Invader, FrameworkElement>(); 
		private readonly  Dictionary<FrameworkElement, DateTime> _shotInvader =
										new Dictionary<FrameworkElement, DateTime>();
		private readonly Dictionary<Shot, FrameworkElement> _shots =
										new Dictionary<Shot, FrameworkElement>();
		private readonly  Dictionary<Point,FrameworkElement> _stars =
										new Dictionary<Point, FrameworkElement>();
		private readonly List<FrameworkElement> _scanLines =
										new List<FrameworkElement>();
	}
}
