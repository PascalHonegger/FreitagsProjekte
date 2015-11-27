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
	internal class SpaceInvadersViewModel
	{
		private readonly Dictionary<Invader, FrameworkElement> _invaders =
			new Dictionary<Invader, FrameworkElement>();

		private readonly ObservableCollection<object> _lives = new ObservableCollection<object>();

		private readonly Dictionary<FrameworkElement, DateTime> _shotInvader =
			new Dictionary<FrameworkElement, DateTime>();

		private readonly Dictionary<Shot, FrameworkElement> _shots =
			new Dictionary<Shot, FrameworkElement>();

		private readonly ObservableCollection<FrameworkElement> _sprites = new ObservableCollection<FrameworkElement>();

		private readonly DispatcherTimer _timer = new DispatcherTimer();
		private bool _lastPaused = true;
		private FrameworkElement _playerControl = null;
		private bool _playerFlashing = false;

		public INotifyCollectionChanged Sprites => _sprites;

		public bool GameOver => Model.GameOver;

		public INotifyCollectionChanged Lives => _lives;

		public bool Paused { get; set; }

		public static double Scale { get; private set; }

		public SpaceInvadersModel Model { get; } = new SpaceInvadersModel();
	}
}